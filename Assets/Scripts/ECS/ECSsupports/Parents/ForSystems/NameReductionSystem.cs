
public abstract class ReductionSystem
{
    protected StartValuesConfig _startValues = default;

    protected ReductionSystem(SupportManager supportManager)
    {
        _startValues = supportManager.StartValues;
    }
}
