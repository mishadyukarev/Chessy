using static MainGame;

internal abstract class SystemGeneralReduction
{
    protected EntitiesGeneralManager _eGM;
    protected SystemsGeneralManager _sGM;

    protected StartValuesGameConfig StartValuesGameConfig = Instance.StartValuesGameConfig;

    internal SystemGeneralReduction(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;
    }
}
