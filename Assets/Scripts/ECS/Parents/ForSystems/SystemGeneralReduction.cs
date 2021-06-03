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
        _startValuesGameConfig = Instance.StartValuesGameConfig;
        _cM = Instance.CellManager;
        _eM = Instance.EconomyManager;
    }

    public virtual void Init()
    {
        _eGM = Instance.ECSmanagerGame.EntitiesGeneralManager;
        _sGM = Instance.ECSmanagerGame.SystemsGeneralManager;
    }

    public virtual void Run()
    {

    }
}
