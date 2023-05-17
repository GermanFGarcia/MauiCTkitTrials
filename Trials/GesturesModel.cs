namespace Uniq;

public class GesturesModel
{
    public IList<Element> ElementCollection { get; } = new List<Element>
    {
        new Element("E1"),
        new Element("E2"),
        new Element("E3"),
        new Element("E4"),
    };
}