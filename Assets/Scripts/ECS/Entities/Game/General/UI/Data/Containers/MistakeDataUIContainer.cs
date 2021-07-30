using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Entities.Game.General.UI.Containers
{
    internal sealed class MistakeDataUIContainer
    {
        private EcsEntity _mistakeInfoDataUIEnt;
        internal ref UnityEventBaseComponent MistakeInfoDataUIEnt_UnityEventCom => ref _mistakeInfoDataUIEnt.Get<UnityEventBaseComponent>();


        internal MistakeDataUIContainer(EcsWorld gameWorld)
        {
            _mistakeInfoDataUIEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));
        }
    }
}
