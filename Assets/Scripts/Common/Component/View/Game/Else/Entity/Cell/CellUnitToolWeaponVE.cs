using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitToolWeaponVE : EntityAbstract
    {
        ref SpriteRendererVC SRCRef => ref Ent.Get<SpriteRendererVC>();

        internal CellUnitToolWeaponVE(in SpriteRenderer sr, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SpriteRendererVC(sr));
        }

        public void Enable(in bool isVisForNext)
        {
            SRCRef.Enable();

            if (isVisForNext) SRCRef.Color = new Color(SRCRef.Color.r, SRCRef.Color.g, SRCRef.Color.b, 1);
            else SRCRef.Color = new Color(SRCRef.Color.r, SRCRef.Color.g, SRCRef.Color.b, 0.6f);
        }
        public void Disable() => SRCRef.Disable();
    }
}