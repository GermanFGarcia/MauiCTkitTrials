using CommunityToolkit.Maui.Markup;

namespace Uniq;

public class BindingsView : ContentPage
{
    public BindingsView(BindingsNexus nexus)
    {
        BindingContext = this.nexus = nexus;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                new Label().Text("String fix"),

                new Label().Margins(0,20,0,0).Text("String, from nexus, direct call => do not work"),
                new Label().Bind(Label.TextProperty, nexus.LocalString),

                new Label().Margins(0,10,0,0).Text("String, from nexus, name path"),
                new Label().Bind(Label.TextProperty, nameof(BindingsNexus.LocalString)),

                new Label().Margins(0,10,0,0).Text("String, from nexus, lambda expression"),
                new Label().Bind(Label.TextProperty, static (BindingsNexus n) => n.LocalString),


                new Label().Margins(0,20,0,0).Text("String, from model, name path"),
                new Label().Bind(Label.TextProperty, nameof(BindingsNexus.RemoteString)),

                new Label().Margins(0,10,0,0).Text("String, from mode, lambda expression"),
                new Label().Bind(Label.TextProperty, static (BindingsNexus n) => n.RemoteString),


                new Label().Margins(0,20,0,0).Text("String, from both, name path"),
                new Label().Bind(Label.TextProperty, nameof(BindingsNexus.BothString)),

                new Label().Margins(0,10,0,0).Text("String, from both, lambda expression"),
                new Label().Bind(Label.TextProperty, static (BindingsNexus n) => n.BothString),


                new Label().Margins(0,20,0,0).Text("Element, from both, name path => do not work"),
                new Label().Bind(Label.TextProperty, nameof(BindingsNexus.Element.StringValue)),

                new Label().Margins(0,10,0,0).Text("Element, from both, lambda expression"),
                new Label().Bind(Label.TextProperty, static (BindingsNexus n) => n.Element.StringValue),

                
                new Label().Margins(0,20,0,0).Text("Collection, from both, deferred, lambda expression"),
                new CollectionView()
                    .ItemTemplate(new ElementTemplate())
                    .Bind(CollectionView.ItemsSourceProperty, static (BindingsNexus n) => n.ElementCollection),


                new Label().Margins(0,20,0,0).Text("Nested collection, from both, deferred, lambda expression"),
                new Label().Bind(Label.TextProperty, static (BindingsNexus n) => n.Nest.Name),
                new CollectionView()
                    .ItemTemplate(new ElementTemplate())
                    .Bind(CollectionView.ItemsSourceProperty, static (BindingsNexus n) => n.Nest.ElementCollection),
            }
        };
    }

    private BindingsNexus nexus;

    internal class ElementTemplate : DataTemplate
    {
        public ElementTemplate() : base(() =>
        {
            return new Label().Bind(Label.TextProperty, static (ElementNexus n) => n.StringValue);
        }) { }
    }
}
