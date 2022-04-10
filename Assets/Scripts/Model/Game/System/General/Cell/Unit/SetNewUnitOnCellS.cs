using Chessy.Common.Entity;
using Chessy.Common.Extension;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed class SetNewUnitOnCellS : SystemModel
    {
        internal SetNewUnitOnCellS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            sMG.UnitSs.SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false, cell);
            sMG.UnitSs.SetStatsS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX, cell);
            sMG.UnitSs.SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0, cell);
            sMG.UnitSs.SetEffectsS.Set(0, 0, 0, false, cell);



            if (eMG.UnitTC(cell).Is(UnitTypes.Pawn))
            {
                eMG.PlayerInfoE(playerT).PawnInfoE.PawnsInGame++;
            }
           


            if (unitT == UnitTypes.Pawn)
            {
                eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People--;

                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) eMG.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    eMG.PlayerInfoE(playerT).KingInfoE.CellKing = cell;
                    eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = false;
                }

                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None, cell);
            }
        }
    }
}