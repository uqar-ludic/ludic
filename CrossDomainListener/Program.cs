﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrossDomainListener
{
    public delegate void TraceWriterHandler(string message);

    internal class SynchronizedTraceListener : TraceListener
    {
        private TraceWriterHandler messageHandler;

        public SynchronizedTraceListener(TraceWriterHandler writeHandler)
        {
            messageHandler = writeHandler;
        }

        public override void Write(string message)
        {
            messageHandler(message);
        }

        public override void WriteLine(string message)
        {
            messageHandler(message + System.Environment.NewLine);
        }
    }

    [Serializable]
    public sealed class CrossDomainTracer : MarshalByRefObject
    {
        private CrossDomainTracer remoteTracer;
        private SynchronizedTraceListener remoteListener;

        public CrossDomainTracer()
        {
        }

        public CrossDomainTracer(AppDomain farDomain)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.remoteTracer = farDomain.CreateInstanceFrom(Assembly.GetExecutingAssembly().Location, typeof(CrossDomainTracer).FullName).Unwrap() as CrossDomainTracer;
            AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            if (remoteTracer != null)
            {
                remoteTracer.StartListening(this);
            }
        }

        public void StartListening(CrossDomainTracer farTracer)
        {
            this.remoteTracer = farTracer;
            this.remoteListener = new SynchronizedTraceListener(new TraceWriterHandler(Write));
            Trace.Listeners.Add(this.remoteListener);
        }

        public void Write(string message)
        {
            this.remoteTracer.RemoteWrite("AppDomain(" + AppDomain.CurrentDomain.Id.ToString() + ") " + message);
        }

        public void RemoteWrite(string message)
        {
            Trace.Write(message);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                Assembly assembly = System.Reflection.Assembly.Load(args.Name);
                if (assembly != null)
                {
                    return assembly;
                }
            }
            catch { }

            // Try to load by assembly fullname (path to file)
            string[] Parts = args.Name.Split(',');
            string File = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Parts[0].Trim() + ".dll";

            return System.Reflection.Assembly.LoadFrom(File);
        }

        public static class CrossDomainTrace
        {
            public static void StartListening(AppDomain remoteDomain)
            {
                new CrossDomainTracer(remoteDomain);
            }
        }
    }
}
