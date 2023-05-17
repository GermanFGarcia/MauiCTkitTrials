using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;

namespace Uniq;

public class GesturesView : ContentPage
{
    public GesturesView(GesturesNexus nexus)
    {
        BindingContext = this.nexus = nexus;

        Content = new HorizontalStackLayout
        {
            new Label().Margins(0, 20, 0, 0).Text("Gestures"),
            new CollectionView()
                .ItemTemplate(new ElementTemplate(this))
                .Bind(CollectionView.ItemsSourceProperty, static (GesturesNexus n) => n.ElementCollection)
        };

    }

    private GesturesNexus nexus;

    internal class ElementTemplate : DataTemplate
    {
        public ElementTemplate(GesturesView container) : base(() =>
        {
            var label = new Label()
                .BackgroundColor(Colors.Violet)
                .Bind(Label.TextProperty, static (ElementNexus n) => n.StringValue);

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureEventHandler;
            label.GestureRecognizers.Add(tapGestureRecognizer);

            return label;
        })
        {
            this.container = container;
        }

        private GesturesView container;

        private static void TapGestureEventHandler<TappedEventArgs>(object source, TappedEventArgs args)
        {
            var l = (Label)source;
            var p = (Page)(l.Parent);
            ((Page)((Label)source).Parent).ShowPopup(new ElementPopup());
        }
    }

    internal class ElementPopup : Popup
    {
        public ElementPopup()
        {
            Content = new VerticalStackLayout
            {
                new Label().Text("This is a popup")
            };
        }
    }
}