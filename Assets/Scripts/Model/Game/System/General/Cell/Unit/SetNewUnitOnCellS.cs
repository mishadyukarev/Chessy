using Chessy.Common.Extension;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed class SetNewUnitOnCellS : SystemModel
    {
        internal SetNewUnitOnCellS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            sMG.UnitSs.SetMain(cell, unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            sMG.UnitSs.SetStats(cell, HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            sMG.UnitSs.SetExtraToolWeapon(cell, ToolWeaponTypes.None, LevelTypes.None, 0);
            sMG.UnitSs.SetEffects(cell, 0, 0, 0, false);



            if (eMG.UnitTC(cell).Is(UnitTypes.Pawn))
            {
                eMG.PlayerInfoE(playerT).PawnInfoE.PawnsInGame++;
            }



            if (unitT == UnitTypes.Pawn)
            {
                eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People--;

                sMG.UnitSs.SetMainToolWeapon(cell, ToolWeaponTypes.Axe, LevelTypes.First);
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

                sMG.UnitSs.SetMainToolWeapon(cell, ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}