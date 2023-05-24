new Entry().Bind(Entry.TextProperty, static (ViewModel vm) => vm.RegistrationCode, static (ViewModel vm, string text) => vm.RegistrationCode = text)



Default property
The Bind method can be called without specifying the property to set the binding up for, this will utilize the defaults provided by the library with the full list at the GitHub repository.
https://github.com/CommunityToolkit/Maui.Markup/blob/523ff96160889f0806f7686e25c5d651fa7d8b7e/src/CommunityToolkit.Maui.Markup/DefaultBindableProperties.cs



new Label().FormattedText(new[] 
{
    new Span { Text = "Here is a link to the docs: " },
    new Span { Text = "https://learn.microsoft.com/", TextDecorations = TextDecorations.Underline, TextColor = Colors.Blue }
});
