using CommunityToolkit.Mvvm.ComponentModel;

namespace Uniq;

public class TxedderNexus : ObservableObject
{
    public TxedderNexus(TxedderModel model)
    {
        this.model = model;
        Working = new WorkingNexus(model);
    }

    private TxedderModel model;

    public WorkingNexus Working { get; set; }
}

public class WorkingNexus
{
    private TxedderModel model;

    public WorkingNexus(TxedderModel model)
    {
        this.model = model;
    }

    public string Content => model.Working.Content;
}
