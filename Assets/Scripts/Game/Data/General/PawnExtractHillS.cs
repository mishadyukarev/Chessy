﻿namespace Game.Game
{
    sealed class PawnExtractHillS : SystemAbstract, IEcsRunSystem
    {
        internal PawnExtractHillS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.PawnExtractHillE(idx_0).Resources = 0;

                if (E.UnitTC(idx_0).Is(UnitTypes.Pawn) && E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Pick))
                {
                    if (E.HillC(idx_0).HaveAnyResources)
                    {
                        var extract = Environment_Values.PAWN_PICK_EXTRACT_HILL;


                        if (E.HillC(idx_0).Resources < extract) extract = E.HillC(idx_0).Resources;


                        E.PawnExtractHillE(idx_0).Resources = extract;

                    }
                }
            }
        }
    }
}