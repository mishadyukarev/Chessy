using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

internal sealed class Main : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool _isOfflineMode;
    [SerializeField] private TestTypes _testType;
    [SerializeField] private SceneTypes _sceneType;

    private static Main _instance;
    private Builder _builder;
    private Names _names;
    private PhotonManager _photonManager;
    private ECSManager _eCSmanager;
    private GameObject _parentGOs;

    #endregion


    #region Properties

    internal static Main Instance => _instance;

    internal bool IsOfflineMode => _isOfflineMode;
    internal TestTypes TestType => _testType;
    internal SceneTypes SceneType => _sceneType;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;

    internal Names Names => _names;
    internal ECSManager ECSmanager => _eCSmanager;
    internal Builder Builder => _builder;
    internal GameObject ParentGOs => _parentGOs;
    internal PhotonManager PhotonGameManager => _photonManager;
    internal ref CanvasCommComponent CanvasManager => ref _eCSmanager.EntitiesCommonManager.CanvasEnt_CanvasCommCom;

    #endregion


    private void Start()
    {
        PhotonNetwork.PhotonServerSettings.StartInOfflineMode = _isOfflineMode;

        _instance = this;
        _builder = new Builder();
        _names = new Names();

        _eCSmanager = new ECSManager();
        _photonManager = new PhotonManager(_eCSmanager);

        ToggleScene(_sceneType);
    }

    private void Update()
    {
        switch (_sceneType)
        {
            case SceneTypes.Menu:
                if (_eCSmanager.EntitiesCommonManager.SoundEnt_SliderCommCom.Value != _eCSmanager.EntitiesCommonManager.SaverEnt_SaverCommonCom.SliderVolume)
                {
                    _eCSmanager.EntitiesCommonManager.SaverEnt_SaverCommonCom.SliderVolume = _eCSmanager.EntitiesCommonManager.SoundEnt_SliderCommCom.Value;
                    _eCSmanager.EntitiesCommonManager.SoundEnt_AudioSourceCommCom.Volume = _eCSmanager.EntitiesCommonManager.SaverEnt_SaverCommonCom.SliderVolume;
                }
                break;

            case SceneTypes.Game:
                _eCSmanager.OwnUpdate();
                _photonManager.SceneManager.OwnUpdate();
                break;

            default:
                break;
        }
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        _sceneType = sceneType;

        switch (_sceneType)
        {
            case SceneTypes.Menu:
                Destroy(_parentGOs);
                _parentGOs = new GameObject(_names.IN_MENU_GAME_ZONE);

                _eCSmanager.ToggleScene(_sceneType);
                _photonManager.ToggleScene(_sceneType);
                break;


            case SceneTypes.Game:
                Destroy(_parentGOs);
                _parentGOs = new GameObject(_names.GAME);

                _eCSmanager.ToggleScene(_sceneType);
                _photonManager.ToggleScene(_sceneType);
                break;

            default:
                break;
        }
    }
}