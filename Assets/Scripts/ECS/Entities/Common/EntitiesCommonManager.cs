using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class EntitiesCommonManager : EntitiesManager
    {
        private Vector3 _posForCamera = new Vector3(7, 4.8f, -2);


        private EcsEntity _canvasEnt;
        internal ref CanvasComponent CanvasEnt_CanvasCommCom => ref _canvasEnt.Get<CanvasComponent>();


        private EcsEntity _commonParentGOZoneEnt;
        internal ref ParentComponent CommParentGOZoneEnt_ParentCom => ref _commonParentGOZoneEnt.Get<ParentComponent>();


        private EcsEntity _toggleSceneParentGOZoneEnt;
        internal ref ParentComponent ToggleSceneParentGOZoneEnt_ParentCom => ref _toggleSceneParentGOZoneEnt.Get<ParentComponent>();


        private EcsEntity _saverEnt;
        internal ref SaverComponent SaverEnt_SaverCommCom => ref _saverEnt.Get<SaverComponent>();
        internal ref StepModeTypeComponent SaverEnt_StepModeTypeCom => ref _saverEnt.Get<StepModeTypeComponent>();


        private EcsEntity _resourcesEnt;
        internal ref ResourcesComponent ResourcesEnt_ResourcesCommonCom => ref _resourcesEnt.Get<ResourcesComponent>();


        private EcsEntity _cameraEnt;
        internal ref CameraComponent CameraEnt_CameraCom => ref _cameraEnt.Get<CameraComponent>();


        private EcsEntity _soundEnt;
        internal ref AudioSourceComponent StandartMusicEnt_AudioSourceCom => ref _soundEnt.Get<AudioSourceComponent>();


        private EcsEntity _unityEventEnt;
        internal ref UnityEventComponent UnityEventEnt_CanvasCommCom => ref _unityEventEnt.Get<UnityEventComponent>();


        internal EntitiesCommonManager(EcsWorld commonWorld)
        {
            _commonParentGOZoneEnt = commonWorld.NewEntity()
                .Replace(new ParentComponent(new GameObject(NameConst.COMMON_ZONE)));
            CommParentGOZoneEnt_ParentCom.ParentGO.transform.SetParent(CommParentGOZoneEnt_ParentCom.ParentGO.transform);


            _toggleSceneParentGOZoneEnt = commonWorld.NewEntity()
                .Replace(new ParentComponent());

            Instance.transform.SetParent(CommParentGOZoneEnt_ParentCom.ParentGO.transform);

            _saverEnt = commonWorld.NewEntity()
                .Replace(new SaverComponent(0.15f))
                .Replace(new StepModeTypeComponent());


            _resourcesEnt = commonWorld.NewEntity()
                .Replace(new ResourcesComponent(true));



            var camera = UnityEngine.Object.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera, Instance.transform.position, Instance.transform.rotation);
            camera.transform.position += _posForCamera;
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;
            camera.transform.SetParent(CommParentGOZoneEnt_ParentCom.ParentGO.transform);
            _cameraEnt = commonWorld.NewEntity()
                .Replace(new CameraComponent(camera));


            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();///////////
            audioSource.transform.SetParent(CommParentGOZoneEnt_ParentCom.ParentGO.transform);

            _soundEnt = commonWorld.NewEntity()
                .Replace(new AudioSourceComponent(audioSource));
            audioSource.clip = ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip;
            audioSource.volume = SaverEnt_SaverCommCom.SliderVolume;
            audioSource.loop = true;
            audioSource.Play();



            var canvas = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Canvas);
            canvas.name = "Canvas";
            _canvasEnt = commonWorld.NewEntity()
                .Replace(new CanvasComponent(canvas));


            var goES = new GameObject("EventSystem");
            _unityEventEnt = commonWorld.NewEntity()
                .Replace(new UnityEventComponent(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>()));
            goES.transform.SetParent(CommParentGOZoneEnt_ParentCom.ParentGO.transform);
        }

        internal void OwnUpdate(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    StandartMusicEnt_AudioSourceCom.AudioSource.volume = SaverEnt_SaverCommCom.SliderVolume;

                    SaverEnt_StepModeTypeCom.StepModeType = Instance.EntMenuM.StepModUIEnt_DropDownTMPCom.StepModValue;
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

                    CanvasUIWorker.DestroyZoneUI(SceneTypes.Game);

                    CanvasUIWorker.SetZoneUI(SceneTypes.Menu, ResourcesEnt_ResourcesCommonCom);

                    var slider = CanvasUIWorker.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");
                    break;


                case SceneTypes.Game:
                    GameObject.Destroy(ToggleSceneParentGOZoneEnt_ParentCom.ParentGO);
                    ToggleSceneParentGOZoneEnt_ParentCom.ParentGO = new GameObject(NameConst.GAME);

                    CanvasUIWorker.DestroyZoneUI(SceneTypes.Menu);

                    CanvasUIWorker.SetZoneUI(SceneTypes.Game, ResourcesEnt_ResourcesCommonCom);

                    if (Instance.IsMasterClient)
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