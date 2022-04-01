using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class SetNewUnitOnCellS : SystemModelGameAbs
    {
        internal SetNewUnitOnCellS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            sMG.UnitSs.SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false, cell);
            sMG.UnitSs.SetStatsS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX, cell);
            sMG.UnitSs.SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0, cell);
            sMG.UnitSs.SetEffectsS.Set(0, 0, 0, false, cell);

            eMG.PlayerInfoE(playerT).LevelE(eMG.UnitLevelTC(cell).LevelT).Add(unitT, 1);


            if (unitT == UnitTypes.Pawn)
            {
                eMG.PlayerInfoE(playerT).PeopleInCity--;

                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) eMG.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    eMG.PlayerInfoE(playerT).HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    eMG.PlayerInfoE(playerT).KingCell = cell;
                    eMG.PlayerInfoE(playerT).HaveKingInInventor = false;
                }

                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None, cell);
            }
        }
    }
}