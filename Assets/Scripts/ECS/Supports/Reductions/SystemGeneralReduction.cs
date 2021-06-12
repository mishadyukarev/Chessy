using Leopotam.Ecs;
using static Main;

internal abstract class SystemGeneralReduction : IEcsInitSystem, IEcsRunSystem
{
    protected EntitiesGeneralManager _eGM;
    protected SystemsGeneralManager _sGM;

    protected StartValuesGameConfig _startValuesGameConfig;
    protected CellManager _cM;
    protected EconomyManager _eM;

    protected SystemGeneralReduction()
    {
        _startValuesGameConfig = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;
    }

    public virtual void Init()
    {
        _eGM = Instance.ECSmanager.EntitiesGeneralManager;
        _sGM = Instance.ECSmanager.SystemsGeneralManager;
        _cM = Instance.ECSmanager.CellManager;
        _eM = Instance.ECSmanager.EconomyManager;
    }

    public virtual void Run()
    {

    }
}
