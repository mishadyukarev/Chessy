using Assets.Scripts.Abstractions;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entity.Data.Common.Else.Container
{
    internal struct DataCommContainerElseZone
    {
        private static EcsEntity _commonParentGOZoneEnt;

        internal static GameObject ParentGO
        {
            get => _commonParentGOZoneEnt.Get<ParentComponent>().ParentGO;
            set => _commonParentGOZoneEnt.Get<ParentComponent>().ParentGO = value;
        }

        internal DataCommContainerElseZone(EcsWorld commWorld)
        {
            _commonParentGOZoneEnt = commWorld.NewEntity()
                .Replace(new ParentComponent(new GameObject(NameConst.COMMON_ZONE)));
        }
    }
}
