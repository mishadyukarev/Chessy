
public abstract class StartValuesReduction
{
    protected StartValuesConfig _startValues = default;

    protected StartValuesReduction(SupportManager supportManager)
    {
        _startValues = supportManager.StartValuesConfig;
    }
}
