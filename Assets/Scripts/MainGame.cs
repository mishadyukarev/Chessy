using Photon.Pun;
using System.Collections;
using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    private static MainGame _instanceGame;

    private PhotonGameManager _photonManager;
    private ECSmanager _eCSmanager;
    private SupportGameManager _supportGameManager;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    internal readonly bool IS_TEST = true;

    internal StartValuesGameConfig StartValuesGameConfig => _supportGameManager.StartValuesGameConfig;
    internal StartSpawnGameManager StartSpawnGameManager => _supportGameManager.StartSpawnGameManager;
    internal CellManager CellManager => _supportGameManager.CellManager;
    internal BuilderManager BuilderManager => _supportGameManager.BuilderManager;
    internal NameManager NameManager => _supportGameManager.NameManager;

    #endregion



    private void Start()
    {
        _instanceGame = this;
        _supportGameManager = new SupportGameManager(out Transform parentTransformScrips);


        _photonManager = new PhotonGameManager(parentTransformScrips);
        _eCSmanager = new ECSmanager(_photonManager);
        _photonManager.InitAfterECS(_eCSmanager);


        gameObject.transform.SetParent(parentTransformScrips);
    }


    private void Update()
    {
        _eCSmanager.Run();
    }

    private void OnDestroy() { }
}