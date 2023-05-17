using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Security.Cryptography.X509Certificates;

namespace Uniq;

public class BindingsModel
{
    public void Start()
    {
        Task.Run(ChangeLocalString);
        Task.Run(ChangeRemoteString);
        Task.Run(ChangeBothString);
        Task.Run(ChangeElementString);
        Task.Run(ChangeElementCollection);
        Task.Run(ChangeNest);
    }

    #region Local String

    private void ChangeLocalString()
    { 
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            WeakReferenceMessenger.Default.Send(new LocalStringChangedMessage($"String from nexus: {num++}"));
        }
    }

    public sealed class LocalStringChangedMessage : ValueChangedMessage<string>
    {
        public LocalStringChangedMessage(string s) : base(s) { }
    }

    #endregion

    #region Remote String

    private void ChangeRemoteString()
    {
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            RemoteString = $"String from model: {num++}";
        }
    }

    private string remoteString;
    public string RemoteString
    {
        get => remoteString;
        set
        {
            if (remoteString == value) return;
            remoteString = value;
            WeakReferenceMessenger.Default.Send(new RemoteStringChangedMessage());
        }
    }

    public sealed class RemoteStringChangedMessage { }

    #endregion

    #region Both String

    private void ChangeBothString()
    {
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            BothString = $"String from model/nexus: {num++}";
        }
    }

    private string bothString;
    public string BothString
    {
        get => bothString;
        set 
        {
            if (bothString == value) { return; }
            bothString = value;
            WeakReferenceMessenger.Default.Send(new BothStringChangedMessage(bothString));
        }
    }

    public sealed class BothStringChangedMessage : ValueChangedMessage<string>
    {
        public BothStringChangedMessage(string s) : base(s) { }
    }

    #endregion

    #region Element

    private void ChangeElementString()
    {
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            Element = new Element($"Element from model/nexus: {num++}");
        }
    }

    private Element element;
    public Element Element
    {
        get => element;
        set
        {
            if (element == value) { return; }
            element = value;
            WeakReferenceMessenger.Default.Send(new ElementChangedMessage(element.StringValue));
        }
    }

    public sealed class ElementChangedMessage : ValueChangedMessage<string>
    {
        public ElementChangedMessage(string s) : base(s) { }
    }

    #endregion

    #region Element Collection

    private void ChangeElementCollection()
    {
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            var element = new Element($"Item: {num++}");
            ElementCollection.Add(element);
            WeakReferenceMessenger.Default.Send(new ElementCollectionChangedMessage(element.StringValue));
        }
    }

    public IList<Element> ElementCollection { get; } = new List<Element>();
    
    public sealed class ElementCollectionChangedMessage : ValueChangedMessage<string>
    {
        public ElementCollectionChangedMessage(string s) : base(s) { }
    }

    #endregion

    #region Nested Element Collection

    private void ChangeNest()
    {
        int num = 0;
        while (true)
        {
            Thread.Sleep(2000);
            Nest = new Nest(num, $"Nest: {num}");
            num++;
        }
    }
    private Nest nest;
    public Nest Nest
    {
        get => nest;
        set
        {
            if (nest == value) { return; }  
            nest = value;
            WeakReferenceMessenger.Default.Send(new NestChangedMessage());
        }
    }

    public sealed class NestChangedMessage { }

    #endregion

}

public class Element
{
    public Element(string stringValue)
    {
        StringValue = stringValue;
    }

    public string StringValue { get; set; }
}

public class Nest
{
    public Nest(int num, string name)
    {
        Num = num;
        Name = name;
    }

    public int Num { get; private set; }

    public string Name { get; private set; }
    
    public IList<Element> ElementCollection 
    {
        get
        {
            var coll = new List<Element>(); 
            Random random = new Random();
            for (int ix = 0; ix < Num; ix++)
            {
                coll.Add(new Element($"Elem: {random.Next(10)}"));
            }
            return coll;
        }
    } 
}
