using UnityEngine;

namespace Chessy.Game
{
    static class SyncFrozenArrawVS
    {
        static readonly Vector3 _mainLocalScale = new Vector3(1, 1, 1);
        static readonly Vector3 _leftLocalScale = new Vector3(-1, 1, 1);

        public static void Sync(in byte idx_0, in EntitiesView eV, in EntitiesModel e)
        {
            eV.UnitEffectVEs(idx_0).FrozenArrawVE.Disable();

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitEffectFrozenArrawC(idx_0).HaveEffect)
                {
                    eV.UnitEffectVEs(idx_0).FrozenArrawVE.Enable();

                    if (e.SelectedIdxC.Idx == idx_0)
                    {
                        eV.UnitEffectVEs(idx_0).Parent.LocalScale = _leftLocalScale;
                    }
                    else
                    {
                        eV.UnitEffectVEs(idx_0).Parent.LocalScale = _mainLocalScale;
                    }
                }
            }
        }
    }
}