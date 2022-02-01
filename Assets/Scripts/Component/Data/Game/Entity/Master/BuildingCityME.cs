using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class BuildingCityME : EntityAbstract
    {
        public ref IdxC WhereBuildCity => ref Ent.Get<IdxC>();

        public BuildingCityME(in EcsWorld world) : base(world)
        {

        }
    }
}