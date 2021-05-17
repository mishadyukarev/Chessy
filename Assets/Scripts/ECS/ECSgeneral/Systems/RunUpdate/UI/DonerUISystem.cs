using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class DonerUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC = default;
    private Button _doneButton;
    private RawImage _donerRawImage = default;

    private bool _isDone => _eGM.DonerComponent.IsCurrentDone;

    internal DonerUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _doneButton = MainGame.InstanceGame.GameObjectPool.DoneButton;
        _doneButton.onClick.AddListener(delegate { Done(); });

        _donerRawImage = MainGame.InstanceGame.GameObjectPool.DonerRawImage;
    }

    public void Run()
    {
        if (_isDone)
        {
            _donerRawImage.gameObject.SetActive(true);
            _doneButton.image.color = Color.red;
        }
        else
        {
            _donerRawImage.gameObject.SetActive(false);
            _doneButton.image.color = Color.white;
        }
    }

    private void Done() => _photonPunRPC.DoneToMaster(!_isDone);
}
