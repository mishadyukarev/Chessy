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

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell_0)
        {
            eMGame.UnitTC(cell_0).Unit = unitT;
            eMGame.UnitPlayerTC(cell_0).Player = playerT;
            eMGame.UnitLevelTC(cell_0).Level = LevelTypes.First;
            eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
            eMGame.UnitIsRightArcherC(cell_0).IsRight = false;

            _sMGame.UnitSystems.SetStatsUnitS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX, cell_0);
            _sMGame.UnitSystems.SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0, cell_0);

            eMGame.UnitEffectStunC(cell_0).Stun = 0;
            eMGame.UnitEffectShield(cell_0).Protection = 0;
            eMGame.UnitEffectFrozenArrawC(cell_0).Shoots = 0;

            eMGame.PlayerInfoE(playerT).LevelE(eMGame.UnitLevelTC(cell_0).Level).Add(unitT, 1);


            if (unitT == UnitTypes.Pawn)
            {
                eMGame.PlayerInfoE(playerT).PeopleInCity--;

                _sMGame.UnitSystems.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) eMGame.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    eMGame.PlayerInfoE(playerT).HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    eMGame.PlayerInfoE(playerT).HaveKingInInventor = false;
                }

                _sMGame.UnitSystems.SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None, cell_0);
            }
        }
    }
}