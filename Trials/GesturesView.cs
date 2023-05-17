using CommunityToolkit.Maui.Markup;

namespace Uniq;

public class GesturesView : ContentPage
{
    public GesturesView(GesturesNexus nexus)
    {
        BindingContext = this.nexus = nexus;


        Content = new VerticalStackLayout
        {
            new Label().Margins(0, 20, 0, 0).Text("Gestures"),
            new CollectionView()
                .ItemTemplate(new ElementTemplate())
                .Bind(CollectionView.ItemsSourceProperty, static (GesturesNexus n) => n.ElementCollection)
        };
    }

    private GesturesNexus nexus;

    internal class ElementTemplate : DataTemplate
    {
        public ElementTemplate() : base(() =>
        {
            var label = new Label()
                .BackgroundColor(Colors.Violet)
                .Bind(Label.TextProperty, static (ElementNexus n) => n.StringValue);

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureEventHandler;

            label.GestureRecognizers.Add(tapGestureRecognizer);

            return label;
        })
        { }

        private static void TapGestureEventHandler<TappedEventArgs>(object source, TappedEventArgs args) 
        {
            // Handle the tap
        }


    }
}