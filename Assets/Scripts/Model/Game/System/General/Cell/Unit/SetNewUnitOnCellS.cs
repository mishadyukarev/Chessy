using Chessy.Common.Extension;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed partial class SystemsModelGame //: SystemModel
    {
        //internal SetNewUnitOnCellS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void SetNewUnitOnCellS(in UnitTypes unitT, in PlayerTypes playerT, in byte cell)
        {
            UnitSs.UnitSimpleS(cell).SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            UnitSs.SetStats(cell, HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            UnitSs.SetExtraToolWeapon(cell, ToolWeaponTypes.None, LevelTypes.None, 0);
            UnitSs.SetEffects(cell, 0, 0, 0, false);



            if (_eMG.UnitTC(cell).Is(UnitTypes.Pawn))
            {
                _eMG.PlayerInfoE(playerT).PawnInfoC.SetPawn();
            }



            if (unitT == UnitTypes.Pawn)
            {
                _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity--;

                UnitSs.SetMainToolWeapon(cell, ToolWeaponTypes.Axe, LevelTypes.First);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) _eMG.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    _eMG.PlayerInfoE(playerT).KingInfoE.CellKing = cell;
                    _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = false;
                }

                UnitSs.SetMainToolWeapon(cell, ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}