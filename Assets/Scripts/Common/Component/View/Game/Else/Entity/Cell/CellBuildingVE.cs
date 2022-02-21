using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellBuildingVE : EntityAbstract
    {
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        internal CellBuildingVE(in SpriteRenderer sr, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SpriteRendererVC(sr));
        }


    }
}