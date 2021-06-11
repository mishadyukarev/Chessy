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
        _startValuesGameConfig = Instance.ECSmanagerGame.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;
    }

    public virtual void Init()
    {
        _eGM = Instance.ECSmanagerGame.EntitiesGeneralManager;
        _sGM = Instance.ECSmanagerGame.SystemsGeneralManager;
        _cM = Instance.ECSmanagerGame.CellManager;
        _eM = Instance.ECSmanagerGame.EconomyManager;
    }

    public virtual void Run()
    {

    }
}
