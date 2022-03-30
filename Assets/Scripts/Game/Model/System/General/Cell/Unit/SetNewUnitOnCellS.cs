using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class SetNewUnitOnCellS : SystemModelGameAbs
    {
        internal SetNewUnitOnCellS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            s.SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false, cell);
            s.SetStatsS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX, cell);
            s.SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0, cell);
            s.SetEffectsS.Set(0, 0, 0, false, cell);

            e.PlayerInfoE(playerT).LevelE(e.UnitLevelTC(cell).Level).Add(unitT, 1);


            if (unitT == UnitTypes.Pawn)
            {
                e.PlayerInfoE(playerT).PeopleInCity--;

                s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) e.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    e.PlayerInfoE(playerT).HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    e.PlayerInfoE(playerT).KingCell = cell;
                    e.PlayerInfoE(playerT).HaveKingInInventor = false;
                }

                s.SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None, cell);
            }
        }
    }
}