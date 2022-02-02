using UnityEngine;

namespace Game.Game
{
    sealed class CellUnitEffectFrozenArrawVS : SystemViewAbstract, IEcsRunSystem
    {
        readonly Vector3 _mainLocalScale = new Vector3(1, 1, 1);
        readonly Vector3 _leftLocalScale = new Vector3(-1, 1, 1);

        internal CellUnitEffectFrozenArrawVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                UnitEffectVEs(idx_0).FrozenArrawVE.SR.Disable();

                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                {
                    if (UnitEffectEs(idx_0).FrozenArrowE.IsFrozenArraw.Is)
                    {
                        UnitEffectVEs(idx_0).FrozenArrawVE.SR.Enable();

                        if (Es.SelectedIdxE.IdxC.Idx == idx_0)
                        {
                            UnitEffectVEs(idx_0).FrozenArrawVE.Parent.LocalScale = _leftLocalScale;
                        }
                        else
                        {
                            UnitEffectVEs(idx_0).FrozenArrawVE.Parent.LocalScale = _mainLocalScale;
                        }
                    }
                }
            }
        }
    }
}