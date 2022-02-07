using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitVE : EntityAbstract
    {
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        internal CellUnitVE(in SpriteRenderer sr, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SpriteRendererVC(sr));
        }
    }
}