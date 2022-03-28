using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class SetNewUnitOnCellS : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMGame;

        internal SetNewUnitOnCellS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            _sMGame.CellSs(cell).SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            _sMGame.CellSs(cell).SetStatsS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            _sMGame.CellSs(cell).SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0);
            _sMGame.CellSs(cell).SetEffectsS.Set(0, 0, 0, false);

            e.PlayerInfoE(playerT).LevelE(e.UnitLevelTC(cell).Level).Add(unitT, 1);


            if (unitT == UnitTypes.Pawn)
            {
                e.PlayerInfoE(playerT).PeopleInCity--;

                _sMGame.CellSs(cell).SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);
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

                _sMGame.CellSs(cell).SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}