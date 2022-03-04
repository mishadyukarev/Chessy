using UnityEngine;

namespace Chessy.Game
{
    public struct CellUnitToolWeaponVE
    {
        public SpriteRendererVC SR;

        internal CellUnitToolWeaponVE(in SpriteRenderer sr)
        {
            SR = new SpriteRendererVC(sr);
        }

        public void Enable(in bool isVisForNext)
        {
            SR.Enable();

            if (isVisForNext) SR.Color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
            else SR.Color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);
        }
        public void Disable() => SR.Disable();
    }
}