using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class ReadyUISystem : StartValuesReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC = default;

    private RectTransform _parentReadyZone;
    private Button _readyButton;

    internal ReadyUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager) : base(supportGameManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;

        _parentReadyZone = startSpawnGameManager.ParentReadyZone;
        _readyButton = startSpawnGameManager.ReadyButton;
        _readyButton.onClick.AddListener(delegate { Ready(true); });
    }

    public void Run()
    {
        if (InstanceGame.IsStartedGame)
        {
            _parentReadyZone.gameObject.SetActive(false);
        }
    }

    private void Ready(bool isReady) => _photonPunRPC.Ready(isReady);
}
