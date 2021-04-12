
public abstract class ReductionSystem
{
    protected NameValueManager _nameValueManager = default;

    protected ReductionSystem(SupportManager supportManager)
    {
        _nameValueManager = supportManager.NameValueManager;
    }
}
