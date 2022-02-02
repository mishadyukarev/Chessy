using UnityEngine;

namespace Game.Game
{
    sealed class FliperAndRotatorUnitVS : SystemViewAbstract, IEcsRunSystem
    {
        internal FliperAndRotatorUnitVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;
                var corner_0 = UnitEs(idx_0).MainE.IsCorned;

                ref var main_0 = ref CellVEs(idx_0).UnitVEs.UnitMainSR;
                ref var extra_0 = ref CellVEs(idx_0).UnitVEs.UnitExtraSR;




                main_0.LocalEulerAngles = new Vector3(0, 0, 0);
                main_0.FlipX = false;
                extra_0.FlipX = false;

                if (Es.SelectedIdxE.IdxC.Is(idx_0))
                {
                    if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                    {
                        if (ownUnit_0.Is(Es.WhoseMove.CurPlayerI))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.Is)
                                {
                                    main_0.LocalEulerAngles = new Vector3(0, 0, -90);
                                    main_0.FlipX = false;
                                }
                                else
                                {
                                    main_0.LocalEulerAngles = new Vector3(0, 0, 0);
                                    main_0.FlipX = true;
                                }
                            }
                            else
                            {
                                main_0.FlipX = true;
                                extra_0.FlipX = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
