using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class ReadySystem : RPCGeneralReduction, IEcsRunSystem
{
    private SystemsGeneralManager _systemsGeneralManager;

    private RectTransform _parentReadyZone;
    private Button _readyButton;

    private bool _isCurrentReady => _eGM.ReadyEntIsActivatedDictCom.IsActivatedDictionary[InstanceGame.IsMasterClient];



    internal ReadySystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _parentReadyZone = InstanceGame.GameObjectPool.ParentReadyZone;
        _readyButton = InstanceGame.GameObjectPool.ReadyButton;
        _readyButton.onClick.AddListener(delegate { Ready(); });
    }

    public void Run()
    {
        if (_eGM.ReadyEntStartGameCom.IsStartedGame)
        {
            _parentReadyZone.gameObject.SetActive(false);
            _systemsGeneralManager.TryActiveRunSystem(false, this.ToString(), _systemsGeneralManager.RunUpdateSystems);
        }

        if (_isCurrentReady) _readyButton.image.color = Color.red;
        else _readyButton.image.color = Color.white;
    }

    private void Ready()
    {
        if (_isCurrentReady) _photonPunRPC.ReadyToMaster(!_isCurrentReady);
        else _photonPunRPC.ReadyToMaster(!_isCurrentReady);
    }
}
