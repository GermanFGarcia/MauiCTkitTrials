using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    #region Commands

    private readonly ICommand doCommand;
    public ICommand DoCommand => doCommand ?? new RelayCommand(Do);
    private void Do()
    { }

    private readonly ICommand doStringCommand;
    public ICommand DoStringCommand => doStringCommand ?? new RelayCommand<string>(DoString);

    private void DoString(string s)
    { }


    #endregion
}