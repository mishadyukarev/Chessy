using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Common.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class EntitiesCommonManager : EntitiesManager
    {
        private EcsEntity _canvasEnt;
        internal ref CanvasCommComponent CanvasEnt_CanvasCommCom => ref _canvasEnt.Get<CanvasCommComponent>();


        private EcsEntity _commonParentGOZoneEnt;
        internal ref ParentGOZoneComponent CommParentGOZoneEnt_ParentGOZoneCom => ref _commonParentGOZoneEnt.Get<ParentGOZoneComponent>();


        private EcsEntity _toggleSceneParentGOZoneEnt;
        internal ref ParentGOZoneComponent ToggleSceneParentGOZoneEnt_ParentGOZoneCom => ref _toggleSceneParentGOZoneEnt.Get<ParentGOZoneComponent>();


        private EcsEntity _saverEnt;
        internal ref SaverCommonComponent SaverEnt_SaverCommCom => ref _saverEnt.Get<SaverCommonComponent>();


        private EcsEntity _resourcesEnt;
        internal ref ResourcesCommComponent ResourcesEnt_ResourcesCommonCom => ref _resourcesEnt.Get<ResourcesCommComponent>();


        private EcsEntity _cameraEnt;
        internal ref CameraCommComponent CameraEnt_CameraCommonCom => ref _cameraEnt.Get<CameraCommComponent>();


        private EcsEntity _soundEnt;
        internal ref AudioSourceCommComponent SoundEnt_AudioSourceCommCom => ref _soundEnt.Get<AudioSourceCommComponent>();
        /*Need Replace*/
        internal ref SliderCommComponent SoundEnt_SliderCommCom => ref _soundEnt.Get<SliderCommComponent>();


        private EcsEntity _unityEventEnt;
        internal ref UnityEventCommComponent UnityEventEnt_CanvasCommCom => ref _unityEventEnt.Get<UnityEventCommComponent>();


        internal EntitiesCommonManager(EcsWorld commonWorld)
        {
            _commonParentGOZoneEnt = commonWorld.NewEntity();
            var commonZoneGO = new GameObject(NameConst.COMMON_ZONE);
            CommParentGOZoneEnt_ParentGOZoneCom.SetParent(commonZoneGO);


            _toggleSceneParentGOZoneEnt = commonWorld.NewEntity();
            _toggleSceneParentGOZoneEnt.Replace(new ParentGOZoneComponent());


            CommParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(Instance.transform);


            _saverEnt = commonWorld.NewEntity();
            SaverEnt_SaverCommCom.SliderVolume = 0.15f;


            _resourcesEnt = commonWorld.NewEntity();
            ResourcesEnt_ResourcesCommonCom.FillFromResources();


            _cameraEnt = commonWorld.NewEntity();
            var camera = UnityEngine.Object.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera, Instance.transform.position, Instance.transform.rotation);
            camera.transform.position += new Vector3(7, 5.5f, -2);
            camera.name = "Camera";
            CommParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(camera.transform);
            CameraEnt_CameraCommonCom.SetCamera(camera);


            _soundEnt = commonWorld.NewEntity();
            var audioSource = new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();///////////
            CommParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(audioSource.transform);
            SoundEnt_AudioSourceCommCom.SetAudioSource(audioSource);
            SoundEnt_AudioSourceCommCom.SetClip(ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip);
            SoundEnt_AudioSourceCommCom.Volume = SaverEnt_SaverCommCom.SliderVolume;
            SoundEnt_AudioSourceCommCom.Loop = true;
            SoundEnt_AudioSourceCommCom.Play();


            _canvasEnt = commonWorld.NewEntity();
            var canvas = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Canvas);
            canvas.name = "Canvas";
            CanvasEnt_CanvasCommCom.SetCanvas(canvas);


            _unityEventEnt = commonWorld.NewEntity();
            var goES = new GameObject("EventSystem");
            UnityEventEnt_CanvasCommCom.EventSystem = goES.AddComponent<EventSystem>();
            UnityEventEnt_CanvasCommCom.StandaloneInputModule = goES.AddComponent<StandaloneInputModule>();
            CommParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(goES.transform);
        }

        internal void OwnUpdate(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    SaverEnt_SaverCommCom.SliderVolume = SoundEnt_SliderCommCom.Value;
                    SoundEnt_AudioSourceCommCom.Volume = SaverEnt_SaverCommCom.SliderVolume;
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    break;
            }
        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    ToggleSceneParentGOZoneEnt_ParentGOZoneCom.DestroyCurrentGOZone();
                    ToggleSceneParentGOZoneEnt_ParentGOZoneCom.SetParent(new GameObject(NameConst.IN_MENU_GAME_ZONE));

                    CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Game);

                    CanvasEnt_CanvasCommCom.SetZoneUI(SceneTypes.Menu, ResourcesEnt_ResourcesCommonCom);

                    var slider = CanvasEnt_CanvasCommCom.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");
                    SoundEnt_SliderCommCom.SetSlider(slider);
                    SoundEnt_SliderCommCom.Value = SaverEnt_SaverCommCom.SliderVolume;
                    break;


                case SceneTypes.Game:
                    ToggleSceneParentGOZoneEnt_ParentGOZoneCom.DestroyCurrentGOZone();
                    ToggleSceneParentGOZoneEnt_ParentGOZoneCom.SetParent(new GameObject(NameConst.GAME));

                    CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Menu);

                    CanvasEnt_CanvasCommCom.SetZoneUI(SceneTypes.Game, ResourcesEnt_ResourcesCommonCom);

                    CameraEnt_CameraCommonCom.Camera.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                    break;

                default:
                    break;
            }
        }
    }
}