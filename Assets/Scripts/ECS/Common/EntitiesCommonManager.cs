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
        CameraEnt_CameraCommonCom.SetCamera(GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.PrefabConfig.Camera));
        CameraEnt_CameraCommonCom.Camera.gameObject.transform.position += new Vector3(7, 5.5f, -2);


        _soundEnt = ecsWorld.NewEntity();
        SoundEnt_AudioSourceCommCom.SetAudioSource(Instance.Builder.CreateGameObject("MusicAudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>());
        SoundEnt_AudioSourceCommCom.SetClip(ResourcesEnt_ResourcesCommonCom.SoundConfig.MusicAudioClip);
        SoundEnt_AudioSourceCommCom.Volume = 0.2f;
        SoundEnt_AudioSourceCommCom.Loop = true;
        SoundEnt_AudioSourceCommCom.Play();


        _canvasEnt = ecsWorld.NewEntity();
        CanvasEnt_CanvasCommCom.Canvas = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.Canvas);
        GameObject.Destroy(CanvasEnt_CanvasCommCom.Canvas.transform.Find("InMenuZone").gameObject);
        GameObject.Destroy(CanvasEnt_CanvasCommCom.Canvas.transform.Find("InGameZone").gameObject);


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
                GameObject.Destroy(CanvasEnt_CanvasCommCom.InGameZoneGO);

                CanvasEnt_CanvasCommCom.InMenuZoneCanvasGO = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.InMenuZoneGO, CanvasEnt_CanvasCommCom.Canvas.transform);
                CanvasEnt_CanvasCommCom.InMenuZoneCanvasGO.name = Instance.Names.InMenuZone;

                SoundEnt_SliderCommCom.SetSlider(Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("Slider").GetComponent<Slider>());
                break;

            case SceneTypes.Game:
                GameObject.Destroy(CanvasEnt_CanvasCommCom.InMenuZoneCanvasGO);

                CanvasEnt_CanvasCommCom.InGameZoneGO = GameObject.Instantiate(ResourcesEnt_ResourcesCommonCom.InGameZoneGO, CanvasEnt_CanvasCommCom.Canvas.transform);
                CanvasEnt_CanvasCommCom.InGameZoneGO.name = Instance.Names.IN_GAME_CANVAS_Zone;

                CameraEnt_CameraCommonCom.Camera.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                break;

            default:
                break;
        }
    }
}