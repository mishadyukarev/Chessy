
public abstract class StartValuesReduction
{
    protected StartValuesGameConfig _startValuesGameConfig = default;

    internal StartValuesReduction(SupportGameManager supportManager)
    {
        _startValuesGameConfig = supportManager.StartValuesGameConfig;
    }
}
