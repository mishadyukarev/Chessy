using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;

namespace Game
{
    public sealed class Main : MonoBehaviour
    {
        private EcsWorld _menuW;
        private EcsWorld _gameW;


        private void Start()
        {
            var comW = new EcsWorld();
            var comSysts = new EcsSystems(comW);
            new Common.FillEntitiesSys(comSysts, ToggleScene, gameObject);

            ToggleScene(SceneTypes.Menu);
        }

        private void Update()
        {
            Common.DataSC.RunUpdate();

            switch (CurSceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    Menu.DataSC.RunUpdate();
                    break;

                case SceneTypes.Game:
                    Game.DataSC.RunUpdate();
                    DataMastSC.RunUpdate();
                    DataViewSC.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }

        private void ToggleScene(SceneTypes scene)
        {
            CurSceneC.Scene = scene;
            switch (scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameW != default)
                    {
                        _gameW.Destroy();
                    }

                    _menuW = new EcsWorld();
                    new Menu.FillEntitieSys(_menuW);
                    break;

                case SceneTypes.Game:
                    if (_menuW != default)
                    {
                        _menuW.Destroy();
                    }

                    _gameW = new EcsWorld();
                    var gameSysts = new EcsSystems(_gameW);

                    gameSysts
                        .Add(new ViewECreate())
                        .Add(new ViewUIECreate())
                        .Add(new DataECreate())
                        .Add(new FillCells());


                    #region Creating

                    ToggleZoneVC.ReplaceZone(SceneTypes.Game);

                    var genZone = new GameObject("GeneralZone");
                    ToggleZoneVC.Attach(genZone.transform);


                    SoundComC.SavedVolume = SoundComC.Volume;


                    var backGroundGO = GameObject.Instantiate(PrefabResComC.BackGroundCollider2D,
                        MainGoVC.Main_GO.transform.position + new Vector3(7, 5.5f, 2), MainGoVC.Main_GO.transform.rotation);

                    var aSParent = new GameObject("AudioSource");
                    aSParent.transform.SetParent(genZone.transform);





                    new ClipResourcesVC(true);
                    new SoundEffectC(aSParent);
                    new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);
                    new GenerZoneVC(genZone);
                    new CameraVC(Camera.main, new Vector3(7.4f, 4.8f, -2));

                    var rpc = new GameObject("RpcView");
                    rpc.AddComponent<RpcSys>();
                    GenerZoneVC.Attach(rpc.transform);
                    new RpcVC(rpc);

                    GenerZoneVC.Attach(backGroundGO.transform);

                    #endregion



                    new DataSCreate(gameSysts);
                    new DataMasSCreate(gameSysts);
                    new ViewDataSCreate(gameSysts);


                    gameSysts.Add(RpcVC.RpcView_GO.GetComponent<RpcSys>());

                    gameSysts.Init();

                    DataViewSC.RotateAll?.Invoke();

                    

                    break;

                default: throw new Exception();
            }
        }
    }
}