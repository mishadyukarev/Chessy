using Leopotam.Ecs;
using static MainGame;

internal abstract class SystemGeneralReduction : IEcsRunSystem
{
    protected EntitiesGeneralManager _eGM;
    protected SystemsGeneralManager _sGM;

    protected StartValuesGameConfig StartValuesGameConfig = Instance.StartValuesGameConfig;

    protected SystemGeneralReduction(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;
    }

    public virtual void Run()
    {
        
    }
}
