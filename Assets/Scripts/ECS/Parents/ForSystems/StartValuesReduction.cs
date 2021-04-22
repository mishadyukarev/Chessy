
public abstract class StartValuesReduction
{
    protected StartValuesGameConfig _startValues = default;

    protected StartValuesReduction(SupportManager supportManager)
    {
        _startValues = supportManager.StartValuesConfig;
    }
}
