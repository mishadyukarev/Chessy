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

    internal DonerUISystem(ECSmanager eCSmanager, PhotonGameManager photonManager)
    {
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _photonPunRPC = photonManager.PhotonPunRPC;

        _doneButton = MainGame.InstanceGame.StartSpawnGameManager.DoneButton;
        _doneButton.onClick.AddListener(delegate { Done(); });

        _donerRawImage = MainGame.InstanceGame.StartSpawnGameManager.DonerRawImage;
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
