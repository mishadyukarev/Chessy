using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class ReadyUISystem : IEcsRunSystem
{
    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;
    private EcsComponentRef<StartGameComponent> _startGameComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;
    private SystemsGeneralManager _systemsGeneralManager;

    private RectTransform _parentReadyZone;
    private Button _readyButton;

    private bool _isReady;



    internal ReadyUISystem(ECSmanager eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _readyComponentRef = eCSmanager.EntitiesGeneralManager.ReadyComponentRef;
        _startGameComponentRef = eCSmanager.EntitiesGeneralManager.StartGameComponentRef;

        _parentReadyZone = MainGame.InstanceGame.GameObjectPool.ParentReadyZone;
        _readyButton = MainGame.InstanceGame.GameObjectPool.ReadyButton;
        _readyButton.onClick.AddListener(delegate { Ready(); });
    }

    public void Run()
    {
        _isReady = _readyComponentRef.Unref().IsReady;

        if (_startGameComponentRef.Unref().IsStartedGame)
        {
            _parentReadyZone.gameObject.SetActive(false);
            _systemsGeneralManager.TryActiveRunSystem(false,  this.ToString(), _systemsGeneralManager.UpdateRunSystems);
        }

        if (_isReady) _readyButton.image.color = Color.red;
        else _readyButton.image.color = Color.white;
    }

    private void Ready()
    {
        var isReady = _readyComponentRef.Unref().IsReady;

        if (isReady) _photonPunRPC.Ready(!isReady);
        else _photonPunRPC.Ready(!isReady);
    }
}
