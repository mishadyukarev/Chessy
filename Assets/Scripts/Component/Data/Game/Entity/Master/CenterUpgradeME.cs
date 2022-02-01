using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CenterUpgradeME : EntityAbstract
    {
        public ref BuildingTC BuildingForUpgrade => ref Ent.Get<BuildingTC>();

        public CenterUpgradeME(in EcsWorld world) : base(world)
        {
        }
    }
}