﻿namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Clear();

                var unit_from = UnitEs(idx_0).TypeE.UnitTC;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (unit_from.Is(UnitTypes.Archer))
                    {
                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!EffectEs(idx_1).FireE.HaveFireC.Have)
                            {
                                if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                {
                                    CellsForArsonArcherEs.Idxs<IdxsC>(idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}