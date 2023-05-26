#region userClasses
public abstract class Client
{
    protected string UserName { get; set; }
    public string UserAuthString { get; set; }
    public bool HasAccess { get; set; }

    public AccessBehaviour AccessBehaviour { get; set; }

    public void UseAccessBehaviour()
    {
        AccessBehaviour.HandleAccess(this);
    }
    public abstract string BuildAuthString();

    public Client(string userName)
    {
        UserName = userName;
        BuildAuthString();
    }
}

public class User : Client
{
    public User(string userName) : base(userName)
    {
        HasAccess = false;
    }

    public override string BuildAuthString()
    {
        UserAuthString = UserName;
        return UserAuthString;
    }

}

public class Manager: Client
{
    public override string BuildAuthString()
    {
        UserAuthString = $"{UserName}MAN";
        return UserAuthString;
    }
    public Manager(string userName) : base(userName)
    {
        HasAccess = true;
    }
}

public class Admin: Client
{
    public override string BuildAuthString()
    {
        UserAuthString = $"{UserName}ADMIN";
        return UserAuthString;
    }
    public Admin(string userName) : base(userName)
    {
        HasAccess = true;
    }
}
#endregion

#region accessStrategy
public interface AccessBehaviour
{
    public bool HandleAccess(Client client);
}
public class CheckStringAccessBehaviour: AccessBehaviour
{
    public bool HandleAccess(Client client)
    {
        if (client.UserAuthString.EndsWith("ADMIN"))
        {
            client.HasAccess = true;
        }

        return true;
    }
}
public class SwitchAuthAccessBehaviour: AccessBehaviour
{
    public bool HandleAccess(Client client)
    {
        client.HasAccess = !client.HasAccess;
        return client.HasAccess;
    }
}
#endregion

#region factory
public class ClientFactory
{
    public virtual Client CreateClient(string clientType, string clientName)
    {
        Client client;

        if(clientType == "user")
        {
            client = new User(clientName);
        } else if (clientType == "manager") {
            client = new Manager(clientName);
        } else if (clientType == "administrator")
        {
            client = new Admin(clientName);
        } else
        {
            throw new Exception("Invalid client type.");
        }

        return client;
    }
}
#endregion

#region handler
public abstract class ClientHandler
{
    public ClientFactory Factory { get; set; }

    public abstract Client CreateClient(string type, string name);
}

public class RetailClientHandler: ClientHandler
{
    public override Client CreateClient(string type, string name)
    {
        Client newClient = Factory.CreateClient(type, name);

        if(newClient is User || newClient is Manager)
        {
            newClient.AccessBehaviour = new SwitchAuthAccessBehaviour();
        } else if (newClient is Admin)
        {
            newClient.AccessBehaviour = new CheckStringAccessBehaviour();
        }

        return newClient;
    }
}

public class EnterpriseClientHandler : ClientHandler
{
    public override Client CreateClient(string type, string name)
    {
        Client newClient = Factory.CreateClient(type, name);

        if (newClient is User)
        {
            newClient.AccessBehaviour = new SwitchAuthAccessBehaviour();
        }
        else if (newClient is Admin || newClient is Manager)
        {
            newClient.AccessBehaviour = new CheckStringAccessBehaviour();
        }

        return newClient;
    }
}

#endregion