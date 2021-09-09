using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager, IDisposable
    {
        private PhotonSceneMenuSystem _photonSceneMenuSystem;

        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld)
        {
            _photonSceneMenuSystem = Main.Instance.gameObject.AddComponent<PhotonSceneMenuSystem>();

            InitOnlySystems
                .Add(new MainMenuSystem())
                .Add(_photonSceneMenuSystem);

            allMenuSystems
                .Add(InitOnlySystems)
                .Add(RunOnlySystems)
                .Add(InitRunSystems);
        }

        public void Dispose()
        {
            GameObject.Destroy(_photonSceneMenuSystem);
        }
    }
}
