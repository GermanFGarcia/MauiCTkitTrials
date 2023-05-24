using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;
using CommunityToolkit.Maui.Markup;

namespace Uniq;

public class TxedderView : ContentPage 
{
    private TxedderNexus nexus;

    public TxedderView(TxedderNexus nexus)
    {
        BindingContext = this.nexus = nexus;

        Style = new Style<Label>()
            .AddAppThemeBinding(Label.BackgroundColorProperty, Colors.DarkSeaGreen, Colors.Azure).CanCascade(true);
        
        Content = new DockLayout
        {
            {  new Label().BackgroundColor(Colors.OldLace)
                .Text("Text Editor")
                .SemanticHint("The title of the news article."), DockPosition.Top 
            },
            {  new Editor().Fill()
                .Bind(Editor.TextProperty, static (TxedderNexus  n) => n.Working.Content) }
        };
    }
}
