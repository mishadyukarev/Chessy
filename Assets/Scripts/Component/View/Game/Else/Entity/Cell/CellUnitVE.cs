using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitVE : EntityAbstract
    {
        ref SpriteRendererVC SRCRef => ref Ent.Get<SpriteRendererVC>();

        internal CellUnitVE(in SpriteRenderer sr, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SpriteRendererVC(sr));
        }

        public void Enable()
        {
            SRCRef.Enable();
        }
        public void Disable()
        {
            SRCRef.Disable();
        }
    }
}