using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

internal class WarningUISystem : IEcsRunSystem
{
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;
    private EcsComponentRef<SelectorUnitComponent> _selectorUnitComponent = default;

    internal WarningUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager)
    {
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _selectorUnitComponent = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
    }

    public void Run()
    {
        if (_donerComponentRef.Unref().IsMistaked)
        {
            _selectorUnitComponent.Unref().Button0.image.color = Color.green;
        }
    }
}
