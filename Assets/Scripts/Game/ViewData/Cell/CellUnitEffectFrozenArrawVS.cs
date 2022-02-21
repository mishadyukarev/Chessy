using UnityEngine;

namespace Game.Game
{
    sealed class CellUnitEffectFrozenArrawVS : SystemViewAbstract, IEcsRunSystem
    {
        readonly Vector3 _mainLocalScale = new Vector3(1, 1, 1);
        readonly Vector3 _leftLocalScale = new Vector3(-1, 1, 1);

        internal CellUnitEffectFrozenArrawVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                VEs.UnitEffectVEs(idx_0).FrozenArrawVE.SR.Disable();

                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (E.UnitEffectFrozenArrawC(idx_0).HaveEffect)
                    {
                        VEs.UnitEffectVEs(idx_0).FrozenArrawVE.SR.Enable();

                        if (E.SelectedIdxC.Idx == idx_0)
                        {
                            VEs.UnitEffectVEs(idx_0).FrozenArrawVE.Parent.LocalScale = _leftLocalScale;
                        }
                        else
                        {
                            VEs.UnitEffectVEs(idx_0).FrozenArrawVE.Parent.LocalScale = _mainLocalScale;
                        }
                    }
                }
            }
        }
    }
}