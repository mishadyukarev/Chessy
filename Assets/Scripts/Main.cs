using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public sealed class Main : MonoBehaviour
{

    #region Variables

    static private Main _instance;

    private Camera _camera;
    private ECSmanager _eCSmanager;
    private PhotonManager _photonManager;
    private SupportManager _supportManager;
    private GameObject _parentScriptsGO;

    #endregion


    #region StartVariables

    [Range(0, 100)] [SerializeField] private int _percentTree = 30;
    [Range(0, 100)] [SerializeField] private int _percentHill = 10;
    [Range(0, 100)] [SerializeField] private int _percentMountain = 2;

    [SerializeField] private int _cellCountX = 15;
    [SerializeField] private int _cellCountY = 12;

    #endregion


    #region Properties

    static public Main Instance => _instance;

    public bool IsMasterClient => PhotonNetwork.IsMasterClient;
    public Player MasterClient => PhotonNetwork.MasterClient;
    public Player LocalPlayer => PhotonNetwork.LocalPlayer;

    #endregion



    private void Start()
    {
        _instance = this;//Master

        _camera = Camera.main;
        if (!IsMasterClient) _camera.transform.Rotate(0, 0, 180);

        _supportManager = new SupportManager(_percentTree, _percentHill, _percentMountain, _cellCountX, _cellCountY);

        _supportManager.BuilderManager.CreateGameObject(out _parentScriptsGO, "Scripts");
        gameObject.transform.SetParent(_parentScriptsGO.transform);

        _photonManager = new PhotonManager(_supportManager, _parentScriptsGO.transform);
        _eCSmanager = new ECSmanager(_supportManager, _photonManager);

        _photonManager.InitAfterECS(_eCSmanager);
    }


    private void Update()
    {
        _eCSmanager.Run();
    }

    private void OnDestroy()
    {
        _eCSmanager.OnDestroy();
    }
}