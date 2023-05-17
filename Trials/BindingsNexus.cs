using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Uniq;

public class BindingsNexus : ObservableObject
{
    public BindingsNexus(BindingsModel model)
    {
        this.model = model;

        WeakReferenceMessenger.Default.Register<BindingsModel.LocalStringChangedMessage>(this, ModelLocalStringChangedMessageHandler);
        WeakReferenceMessenger.Default.Register<BindingsModel.RemoteStringChangedMessage>(this, ModelRemoteStringChangedMessageHandler);
        WeakReferenceMessenger.Default.Register<BindingsModel.BothStringChangedMessage>(this, ModelBothStringChangedMessageHandler);
        WeakReferenceMessenger.Default.Register<BindingsModel.ElementChangedMessage>(this, ModelElementChangedMessageHandler);
        WeakReferenceMessenger.Default.Register<BindingsModel.ElementCollectionChangedMessage>(this, ModelElementCollectionChangedMessageHandler);
        WeakReferenceMessenger.Default.Register<BindingsModel.NestChangedMessage>(this, ModelNestChangedMessageHandler);
    }

    private BindingsModel model;

    #region Local String

    private string localString = "Default value";
    public string LocalString
    {
        get => localString;
        set => SetProperty(ref localString, value);
    }

    private void ModelLocalStringChangedMessageHandler(object receiver, BindingsModel.LocalStringChangedMessage message)
    {
        LocalString = message.Value;
    }

    #endregion

    #region Remote String

    public string RemoteString => model.RemoteString;

    private void ModelRemoteStringChangedMessageHandler(object receiver, BindingsModel.RemoteStringChangedMessage message)
    {
        OnPropertyChanged(nameof(RemoteString));
    }

    #endregion

    #region Both String

    private string bothString;
    public string BothString
    {
        get => bothString;
        set => SetProperty(ref bothString, value);
    }

    private void ModelBothStringChangedMessageHandler(object receiver, BindingsModel.BothStringChangedMessage message)
    {
        BothString = message.Value;
    }

    #endregion

    #region Element

    private ElementNexus element;
    public ElementNexus Element
    {
        get => element;
        set
        {
            if(SetProperty(ref element, value))
            {
                // need to raise changes on nested properties as well
                OnPropertyChanged(nameof(Element.StringValue));
            }
        }
    }

    private void ModelElementChangedMessageHandler(object receiver, BindingsModel.ElementChangedMessage message)
    {
        Element = new ElementNexus(message.Value);
    }

    #endregion

    #region Element Collection

    public ObservableCollection<ElementNexus> ElementCollection { get; } = new();

    private void ModelElementCollectionChangedMessageHandler(object receiver, BindingsModel.ElementCollectionChangedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ElementCollection.Add(new ElementNexus(message.Value));
        });
    }

    #endregion

    #region Nested Element Collection

    private NestNexus nest;
    public NestNexus Nest
    {
        get => nest;
        set
        {
            if (SetProperty(ref nest, value))
            {
                // need to raise changes on nested properties as well
                OnPropertyChanged(nameof(Nest.Name));
                OnPropertyChanged(nameof(Nest.ElementCollection));
            }
        }
    }

    private void ModelNestChangedMessageHandler(object receiver, BindingsModel.NestChangedMessage message)
    {
        Nest = new NestNexus(model.Nest);
    }

    #endregion
}

public class ElementNexus
{
    public ElementNexus(string stringValue)
    {
        StringValue = stringValue;
    }

    public string StringValue { get; set; }
}

public class NestNexus : ObservableObject
{
    public NestNexus(Nest nestModel)
    {
        this.nestModel = nestModel;
    }

    Nest nestModel;

    public string Name => nestModel.Name;

    private ObservableCollection<ElementNexus> elementCollection;
    public ObservableCollection<ElementNexus> ElementCollection 
    {
        get
        {
            if (elementCollection == null)
            { 
                elementCollection = nestModel.ElementCollection
                    .Select(entry => new ElementNexus(entry.StringValue))
                    .ToObservableCollection<ElementNexus>();
            }

            return elementCollection;
        }
    }    
}