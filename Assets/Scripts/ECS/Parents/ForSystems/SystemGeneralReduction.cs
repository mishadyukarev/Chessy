using static MainGame;

public abstract class SystemGeneralReduction
{
    protected EntitiesGeneralManager _eGM;

    protected StartValuesGameConfig StartValuesGameConfig = InstanceGame.StartValuesGameConfig;

    internal SystemGeneralReduction(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }
}
