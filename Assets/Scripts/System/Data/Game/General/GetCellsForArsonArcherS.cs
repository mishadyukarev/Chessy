﻿namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                E.UnitEs(idx_0).ForArson.Clear();

                if (!E.UnitEffectStunC(idx_0).IsStunned)
                {
                    if (E.UnitTC(idx_0).HaveUnit && E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            if (!E.EffectEs(idx_1).HaveFire)
                            {
                                if (E.AdultForestC(idx_1).HaveAny)
                                {
                                    E.UnitEs(idx_0).ForArson.Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}