using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class ReadyUISystem : IEcsRunSystem
{
    private EcsComponentRef<ReadyComponent> _readyComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;
    private SystemsGeneralManager _systemsGeneralManager;

    private RectTransform _parentReadyZone;
    private Button _readyButton;

    private bool _isReady;



    internal ReadyUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _readyComponentRef = eCSmanager.EntitiesGeneralManager.ReadyComponentRef;

        _parentReadyZone = startSpawnGameManager.ParentReadyZone;
        _readyButton = startSpawnGameManager.ReadyButton;
        _readyButton.onClick.AddListener(delegate { Ready(); });
    }

    public void Run()
    {
        _isReady = _readyComponentRef.Unref().IsReady;

        if (InstanceGame.IsStartedGame)
        {
            _parentReadyZone.gameObject.SetActive(false);
            _systemsGeneralManager.ActiveRunSystem(false, SystemGeneralTypes.Update, this.ToString());
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
