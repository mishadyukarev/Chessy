using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Main;

internal sealed class EntitiesCommonManager : EntitiesManager
{
    private EcsEntity _saverEnt;
    internal ref SaverCommonComponent SaverEnt_SaverCommonCom => ref _saverEnt.Get<SaverCommonComponent>();


    private EcsEntity _resourcesEnt;
    internal ref ResourcesCommComponent ResourcesEnt_ResourcesCommonCom => ref _resourcesEnt.Get<ResourcesCommComponent>();


    private EcsEntity _cameraEnt;
    internal ref CameraCommComponent CameraEnt_CameraCommonCom => ref _cameraEnt.Get<CameraCommComponent>();


    private EcsEntity _soundEnt;
    internal ref AudioSourceCommComponent SoundEnt_AudioSourceCommCom => ref _soundEnt.Get<AudioSourceCommComponent>();
    internal ref SliderCommComponent SoundEnt_SliderCommCom => ref _soundEnt.Get<SliderCommComponent>();


    private EcsEntity _canvasEnt;
    internal ref CanvasCommComponent CanvasEnt_CanvasCommCom => ref _canvasEnt.Get<CanvasCommComponent>();


    private EcsEntity _unityEventEnt;
    internal ref UnityEventCommComponent UnityEventEnt_CanvasCommCom => ref _unityEventEnt.Get<UnityEventCommComponent>();


    internal EntitiesCommonManager(EcsWorld ecsWorld)
    {
        _saverEnt = ecsWorld.NewEntity()
            .Replace(new SaverCommonComponent());

        _resourcesEnt = ecsWorld.NewEntity()
            .Replace(new ResourcesCommComponent());
        ResourcesEnt_ResourcesCommonCom.FillFromResources();


        _cameraEnt = ecsWorld.NewEntity();
        var camera = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera);
        camera.name = "CommonCamera";
        CameraEnt_CameraCommonCom.SetCamera(camera);
        CameraEnt_CameraCommonCom.Camera.gameObject.transform.position += new Vector3(7, 5.5f, -2);


        _soundEnt = ecsWorld.NewEntity();
        SoundEnt_AudioSourceCommCom.SetAudioSource(Instance.Builder.CreateGameObject("CommonAudio", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>());
        SoundEnt_AudioSourceCommCom.SetClip(ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip);
        SoundEnt_AudioSourceCommCom.Volume = 0.2f;
        SoundEnt_AudioSourceCommCom.Loop = true;
        SoundEnt_AudioSourceCommCom.Play();


        _canvasEnt = ecsWorld.NewEntity();
        var canvas = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.Canvas);
        canvas.name = "Canvas";
        CanvasEnt_CanvasCommCom.SetCanvas(canvas);

        //CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Menu);
        //CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Game);


        _unityEventEnt = ecsWorld.NewEntity();
        var goES = new GameObject("EventSystem");
        UnityEventEnt_CanvasCommCom.EventSystem = goES.AddComponent<EventSystem>();
        UnityEventEnt_CanvasCommCom.StandaloneInputModule = goES.AddComponent<StandaloneInputModule>();
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Game);

                CanvasEnt_CanvasCommCom.SetZoneUI(SceneTypes.Menu, ResourcesEnt_ResourcesCommonCom);

                var slider = CanvasEnt_CanvasCommCom.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");
                SoundEnt_SliderCommCom.SetSlider(slider);
                break;

            case SceneTypes.Game:
                CanvasEnt_CanvasCommCom.DestroyZoneUI(SceneTypes.Menu);

                CanvasEnt_CanvasCommCom.SetZoneUI(SceneTypes.Game, ResourcesEnt_ResourcesCommonCom);

                CameraEnt_CameraCommonCom.Camera.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                break;

            default:
                break;
        }
    }
}