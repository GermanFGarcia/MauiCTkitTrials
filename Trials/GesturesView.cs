using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;

namespace Uniq;

public class GesturesView : ContentPage
{
    private GesturesNexus nexus;

    public GesturesView(GesturesNexus nexus)
    {
        BindingContext = this.nexus = nexus;

        Content = new VerticalStackLayout
        {
            new Label().Text("Title"),
            new Button().Text("Button").Width(40).BindCommand(static (GesturesNexus n) => n.DoCommand),
            new Label().Text("Gestures Event").TapGesture(TitleTapGestureEventHandler),
            new Label().Text("Gestures command").BindTapGesture(nameof(GesturesNexus.DoCommand)),
            new Label().Assign(out Label label).Text("Gestures command param").BindTapGesture(nameof(GesturesNexus.DoStringCommand), nexus, nameof(Label.Text), label),
            new CollectionView()
                .ItemTemplate(new ElementTemplate(this))
                .Bind(CollectionView.ItemsSourceProperty, static (GesturesNexus n) => n.ElementCollection)
        };
    }

    private void TitleTapGestureEventHandler<TappedEventArgs>(object source, TappedEventArgs args)
    {
        this.ShowPopup(new MessagePopup("Page popup"));
    }
        
    internal class ElementTemplate : DataTemplate
    {
        public ElementTemplate(GesturesView container) : base(() =>
        {
            return new Label()
                .BackgroundColor(Colors.Violet)
                .Bind(Label.TextProperty, static (ElementNexus n) => n.StringValue)
                .TapGesture(ItemTapGestureEventHandler);
        })
        {            
			// not a good idea to assign a parameter to a static field,
			// the staic field will only hold the last assigned value,
			// but in this class, by construction, that value is always the same container page
            ElementTemplate.container = container;
        }

        private static GesturesView container;

        private static void ItemTapGestureEventHandler<TappedEventArgs>(object source, TappedEventArgs args)
        {
            container.ShowPopup(new MessagePopup($"Popup for item {((Label)source).Text}"));
        }
    }

    internal class MessagePopup : Popup
    {
        public MessagePopup(string message)
        {
            Content = new VerticalStackLayout
            {
                new Label().Text(message)
            };
        }
    }
}
