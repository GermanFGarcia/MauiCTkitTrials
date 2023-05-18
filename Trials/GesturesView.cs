using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;

namespace Uniq;

public class GesturesView : ContentPage
{
    private GesturesNexus nexus;

    public GesturesView(GesturesNexus nexus)
    {
        BindingContext = this.nexus = nexus;

        Content = new HorizontalStackLayout
        {
            new Label().Margins(0, 20, 0, 0).Text("Gestures").TapGesture(TitleTapGestureEventHandler),
            new CollectionView()
                .ItemTemplate(new ElementTemplate(this))
                .Bind(CollectionView.ItemsSourceProperty, static (GesturesNexus n) => n.ElementCollection)
        };
    }

    private void TitleTapGestureEventHandler()
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

public static class GesturesExtensions
{
    public static TGestureElement TapGesture<TGestureElement>(
        this TGestureElement gestureElement,
        EventHandler<TappedEventArgs>? onTapped = null,
        int? numberOfTapsRequired = null
    ) where TGestureElement : IGestureRecognizers
    {
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        if (numberOfTapsRequired.HasValue)
        {
            tapGestureRecognizer.NumberOfTapsRequired = numberOfTapsRequired.Value;
        }
        tapGestureRecognizer.Tapped += onTapped;

        gestureElement.GestureRecognizers.Add(tapGestureRecognizer);
        return gestureElement;
    }
}