using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

internal class DonerUISystem : IEcsRunSystem
{
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;
    private PhotonPunRPC _photonPunRPC = default;
    private Button _doneButton;
    private RawImage _donerRawImage = default;

    private bool _isDone => _donerComponentRef.Unref().IsDone;

    internal DonerUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager)
    {
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _photonPunRPC = photonManager.PhotonPunRPC;

        _doneButton = startSpawnGameManager.DoneButton;
        _doneButton.onClick.AddListener(delegate { Done(); });

        _donerRawImage = startSpawnGameManager.DonerRawImage;
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

    private void Done() => _photonPunRPC.Done(!_isDone);
}
