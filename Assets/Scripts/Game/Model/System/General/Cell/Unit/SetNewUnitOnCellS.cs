using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class SetNewUnitOnCellS : SystemModelGameAbs
    {
        readonly UnitMainE _unitMaineE;
        readonly CellSs _unitSs;

        internal SetNewUnitOnCellS(in UnitMainE unitMaineE, in CellSs unitSs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _unitMaineE = unitMaineE;
            _unitSs = unitSs;
        }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT)
        {
            _unitSs.SetMainS.Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            _unitSs.SetStatsS.Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            _unitSs.SetExtraTWS.Set(ToolWeaponTypes.None, LevelTypes.None, 0);
            _unitSs.SetEffectsS.Set(0, 0, 0, false);

            eMGame.PlayerInfoE(playerT).LevelE(_unitMaineE.LevelTC.Level).Add(unitT, 1);


            if (unitT == UnitTypes.Pawn)
            {
                eMGame.PlayerInfoE(playerT).PeopleInCity--;

                _unitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);
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

                _unitSs.SetMainTWS.Set(ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}