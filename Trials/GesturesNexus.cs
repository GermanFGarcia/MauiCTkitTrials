using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Uniq;

public class GesturesNexus : ObservableObject
{
    public GesturesNexus(GesturesModel model)
    {
        this.model = model;
    }

    private GesturesModel model;

    private ObservableCollection<ElementNexus> elementCollection;
    public ObservableCollection<ElementNexus> ElementCollection
    {
        get
        {
            if (elementCollection == null)
            {
                elementCollection = model.ElementCollection
                    .Select(entry => new ElementNexus(entry.StringValue))
                    .ToObservableCollection<ElementNexus>();
            }

            return elementCollection;
        }
    }
}