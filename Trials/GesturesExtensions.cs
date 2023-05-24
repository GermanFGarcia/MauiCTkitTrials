using CommunityToolkit.Maui.Markup;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Uniq;

public static class GesturesExtensions
{
    /// <summary>
    /// Connects tap gestures to an event handler defined in the view.
    /// This event handler receives the source element by parameter.
    /// </summary>
    /// <typeparam name="TGestureElement"></typeparam>
    /// <param name="gestureElement"></param>
    /// <param name="onTapped"></param>
    /// <param name="numberOfTapsRequired"></param>
    /// <returns></returns>
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
