using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Entity.Data.Common.Else.Container;
using Assets.Scripts.ECS.Menu.Entities;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class EntDataCommonElseManager
    {
        private Vector3 _posForCamera = new Vector3(7, 4.8f, -2);



        private EcsEntity _toggleSceneParentGOZoneEnt;
        internal ref ParentComponent ToggleSceneParentGOZoneEnt_ParentCom => ref _toggleSceneParentGOZoneEnt.Get<ParentComponent>();



        private EcsEntity _resourcesEnt;
        internal ref ResourcesComponent ResourcesEnt_ResourcesCommonCom => ref _resourcesEnt.Get<ResourcesComponent>();


        private EcsEntity _cameraEnt;
        internal ref CameraComponent CameraEnt_CameraCom => ref _cameraEnt.Get<CameraComponent>();



        private EcsEntity _unityEventEnt;
        internal ref UnityEventBaseComponent UnityEventEnt_CanvasCommCom => ref _unityEventEnt.Get<UnityEventBaseComponent>();


        internal EntDataCommonElseManager(EcsWorld commonWorld)
        {
            new DataCommContainerElseZone(commonWorld);


            DataCommContainerElseZone.ParentGO.transform.SetParent(DataCommContainerElseZone.ParentGO.transform);


            _toggleSceneParentGOZoneEnt = commonWorld.NewEntity()
                .Replace(new ParentComponent());

            Instance.transform.SetParent(DataCommContainerElseZone.ParentGO.transform);


            new DataCommContainerElseSaver(commonWorld);



            _resourcesEnt = commonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));



            var camera = UnityEngine.Object.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera, Instance.transform.position, Instance.transform.rotation);
            camera.transform.position += _posForCamera;
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;
            camera.transform.SetParent(DataCommContainerElseZone.ParentGO.transform);
            _cameraEnt = commonWorld.NewEntity()
                .Replace(new CameraComponent(camera));





            var goES = new GameObject("EventSystem");
            _unityEventEnt = commonWorld.NewEntity()
                .Replace(new UnityEventBaseComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()));
            goES.transform.SetParent(DataCommContainerElseZone.ParentGO.transform);
        }

        internal void OwnUpdate(SceneTypes sceneType, EntViewMenuElseManager entMenuManager)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    DataCommContainerElseSaver.StepModeType = entMenuManager.StepModUIEnt_DropDownTMPCom.StepModValue;
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    GameObject.Destroy(ToggleSceneParentGOZoneEnt_ParentCom.ParentGO);
                    ToggleSceneParentGOZoneEnt_ParentCom.ParentGO = new GameObject(NameConst.IN_MENU_GAME_ZONE);

                    ViewCommonContainerUICanvas.DestroyZoneUI(SceneTypes.Game);

                    ViewCommonContainerUICanvas.SetZoneUI(SceneTypes.Menu, ResourcesEnt_ResourcesCommonCom);

                    var slider = ViewCommonContainerUICanvas.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");
                    break;


                case SceneTypes.Game:
                    GameObject.Destroy(ToggleSceneParentGOZoneEnt_ParentCom.ParentGO);
                    ToggleSceneParentGOZoneEnt_ParentCom.ParentGO = new GameObject(NameConst.GAME);

                    ViewCommonContainerUICanvas.DestroyZoneUI(SceneTypes.Menu);

                    ViewCommonContainerUICanvas.SetZoneUI(SceneTypes.Game, ResourcesEnt_ResourcesCommonCom);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        CameraEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 0, 0);
                        CameraEnt_CameraCom.Camera.transform.position = Instance.transform.position + _posForCamera;
                    }
                    else
                    {
                        CameraEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 180, 0);
                        CameraEnt_CameraCom.Camera.transform.position = Instance.transform.position + _posForCamera + new Vector3(0, 0.5f, 0);
                    }
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}