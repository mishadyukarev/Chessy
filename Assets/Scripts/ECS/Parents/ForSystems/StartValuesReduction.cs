
public abstract class StartValuesReduction
{
    protected StartValuesGameConfig _startValues = default;

    internal StartValuesReduction(SupportGameManager supportManager)
    {
        _startValues = supportManager.StartValuesConfig;
    }
}
