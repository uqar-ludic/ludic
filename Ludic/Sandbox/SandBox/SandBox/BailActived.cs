using System;
using System.Runtime.Remoting.Lifetime;
using System.Security.Principal;

public class ClientActivatedType : MarshalByRefObject
{
    // http://msdn.microsoft.com/fr-fr/library/6tkeax11(v=vs.85).aspx, gestion du bail 

    // Voir comment gérer la mémoire et la CPU 

   
    public override Object InitializeLifetimeService()
    {

        ILease lease = (ILease)base.InitializeLifetimeService();

        // Normally, the initial lease time would be much longer.
        // It is shortened here for demonstration purposes.
        if (lease.CurrentState == LeaseState.Initial)
        {
            lease.InitialLeaseTime = TimeSpan.FromSeconds(3);
            lease.SponsorshipTimeout = TimeSpan.FromSeconds(7);
            lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
        }
        return lease;
    }

    public string RemoteMethod()
    {

        // Announces to the server that the method has been called.
        Console.WriteLine("ClientActivatedType.RemoteMethod called.");

        // Reports the client identity name.
        return "RemoteMethod called. " + WindowsIdentity.GetCurrent().Name;

    }
}