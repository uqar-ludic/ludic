using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Build.Logging;
using BuildEngine;
using Microsoft.Build.BuildEngine;
using System.Collections.Generic;

namespace Redirect_projet_libre
{
    public class SolutionBuilder
    {
        #region Variables

        BasicFileLogger b;

        string sln_path_;
        string output_;

        int warning_;
        int error_;

        List<string> compilResult = new List<string>();
        public List<object> objectToSend = new List<object>();

        #endregion

        public SolutionBuilder() { }

        [STAThread]

        static void Main(string[] args)
        {
            SolutionBuilder builder = new SolutionBuilder();
            builder.compile(@"C:\Users\Antoine\Documents\Visual Studio 2012\Projects\Test_fail\Test_fail.sln",
            @"C:\Users\Antoine\Documents\Visual Studio 2012\Projects\Test_fail\Test_fail\obj\Debug\Test_fail.csproj.FileListAbsolute.txt");
            //builder.compile(@"C:\Users\Antoine\Documents\Visual Studio 2012\Projects\Test_success\Test_success.sln",
            //@"C:\Users\Antoine\Documents\Visual Studio 2012\Projects\Test_success\Test_success\obj\Debug\Test_success.csproj.FileListAbsolute.txt");

            builder.parseOutputLines();

            builder.putInList();
            builder.display();
            Console.ReadKey();
        }

        void compile(string solution_name, string logfile)
        {
            sln_path_ = solution_name;
            b = new BasicFileLogger();
            b.Parameters = logfile;
            b.register();
            Engine.GlobalEngine.BuildEnabled = true;
            Project p = new Project(Engine.GlobalEngine);
            p.BuildEnabled = true;
            p.Load(solution_name);
            p.Build();

            output_ = b.getLogoutput();

            output_ += "\n\nWarnings: " + b.Warningcount;
            output_ += "\nErrors: " + b.Errorcount + "\n";

            warning_ = b.Warningcount;
            error_ = b.Errorcount;

            b.Shutdown();
        }

        void parseOutputLines()
        {
            string csproj_name = Path.GetDirectoryName(sln_path_);
            csproj_name += "\\" + Path.GetFileNameWithoutExtension(sln_path_)
                + "\\" + Path.GetFileNameWithoutExtension(sln_path_);
            csproj_name += ".csproj";

            output_ = getBetween("Compiling Project: " + csproj_name, "Compilation finished for " + csproj_name);
        }

        string getBetween(string strStart, string strEnd)
        {
            int Start, End;

            if (output_.Contains(strStart) && output_.Contains(strEnd))
            {
                Start = output_.IndexOf(strStart, 0) + strStart.Length;
                End = output_.IndexOf(strEnd, Start);
                return output_.Substring(Start, End - Start);
            }
            else
                return output_;
        }

        string getBetween(string line, string strStart, string strEnd)
        {
            int Start, End;

            if (line.Contains(strStart) && line.Contains(strEnd))
            {
                Start = line.IndexOf(strStart, 0) + strStart.Length;
                End = line.IndexOf(strEnd, Start);
                return line.Substring(Start, End - Start);
            }
            else
                return line;
        }

        string getBetween(string line, int Start, string strEnd)
        {
            int End;

            if (line.Contains(strEnd))
            {
                End = line.IndexOf(strEnd, Start);
                return line.Substring(Start, End - Start);
            }
            else
                return line;
        }

        void putInList()
        {
            bool error = false;
            bool warning = false;

            string[] stringSeparators = new string[] {"\n", "\r"};
            string[] result;
            result = output_.Split(stringSeparators, StringSplitOptions.None);
            foreach (string s in result)
            {
                if (!compilResult.Contains(s))
                {
                    compilResult.Add(s);
                    if (s.Contains("Message: : Warning"))
                    {
                        warning = true;
                        createObject("warning", s);
                    }
                    else if (s.Contains("ERROR"))
                    {
                        error = true;
                        createObject("error", s);
                    }
                    else if (s.Contains("Message"))
                    {
                        if (error == true)
                            updateObject("error", s);
                        else if (warning == true)
                            updateObject("warning", s);
                        error = false;
                        warning = false;
                    }
                }
            }
        }

        void createObject(string type, string line)
        {
            if (type.Contains("error"))
            {
                line = getBetween(line, "ERROR ", "\n\t");
                string file = getBetween(line, "ERROR ", "(");
                string number = getBetween(line, "(", ")");
                TraceCompil obj = new TraceCompil(messageType_.ERROR, number, "", file);
                objectToSend.Add(obj);
            }
            else if (type.Contains("warning"))
            {
                line = getBetween(line, "Warning ", "\n\t");
                string file = getBetween(line, "Warning ", "(");
                string number = getBetween(line, "(", ")");
                TraceCompil obj = new TraceCompil(messageType_.WARNING, number, "", file);
                objectToSend.Add(obj);
            }
        }

        void updateObject(string type, string line)
        {
            TraceCompil obj = (TraceCompil)objectToSend[objectToSend.Count - 1];
            obj.message_ = line;
        }

        void display()
        {
            foreach (TraceCompil tmp in objectToSend)
            {
                Console.WriteLine("===========================\n");
                Console.WriteLine("Filename: " + tmp.fileName_);
                Console.WriteLine("Line number: " + tmp.lineNumber_);
                Console.WriteLine("Message: " + tmp.message_);
                Console.WriteLine("Type: " + tmp.type_);
                Console.WriteLine("===========================\n");
            }
        }
    }
}