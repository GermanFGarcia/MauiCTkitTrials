namespace Uniq;

public class TxedderModel
{
    public TxedderModel()
    {
        Working = new WorkingModel();    
    }

    public WorkingModel Working { get; set; }
}

public class WorkingModel
{
    public WorkingModel()
    {
        Content = "default content to edit";
    }

    public string Content { get; set; }
}