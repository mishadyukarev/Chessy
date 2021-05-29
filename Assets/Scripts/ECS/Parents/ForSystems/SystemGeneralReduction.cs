using Leopotam.Ecs;
using Photon.Realtime;
using UnityEngine;
using static MainGame;

internal abstract class SystemGeneralReduction : IEcsRunSystem
{
    protected EntitiesGeneralManager _eGM;
    protected SystemsGeneralManager _sGM;

    protected StartValuesGameConfig _startValuesGameConfig;
    protected CellWorker _cellWorker;

    protected SystemGeneralReduction(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
        _sGM = eCSmanager.SystemsGeneralManager;
        _startValuesGameConfig = Instance.StartValuesGameConfig;
        _cellWorker = Instance.CellWorker;
    }

    public virtual void Run()
    {

    }
}
