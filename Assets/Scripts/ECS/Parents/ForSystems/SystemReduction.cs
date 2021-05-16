using static MainGame;

public abstract class SystemReduction
{
    protected StartValuesGameConfig _startValuesGameConfig = InstanceGame.StartValuesGameConfig;
    internal EntitiesGeneralManager _entitiesGeneralManager;

    internal SystemReduction(ECSmanager eCSmanager)
    {
        _entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;
    }
}
