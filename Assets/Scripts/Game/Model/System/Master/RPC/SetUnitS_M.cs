using Chessy.Common.Entity;
using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class SetUnitS_M : SystemModelGameAbs
    {
        readonly SetNewUnitOnCellS _setNewUnitS;

        internal SetUnitS_M(in SetNewUnitOnCellS setNewUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _setNewUnitS = setNewUnitS;
        }

        internal void Set(in byte cell_0, in UnitTypes unitT, in Player sender)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            if (eMGame.CellEs(cell_0).CellE.IsStartedCell(whoseMove) && !eMGame.UnitTC(cell_0).HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (eMGame.LessonTC.LessonT == LessonTypes.SettingKing)
                    {
                        eMGame.LessonTC.SetNextLesson();
                    }
                }
                else if (unitT == UnitTypes.Pawn)
                {
                    if (eMGame.LessonTC.LessonT == LessonTypes.SettingPawn)
                    {
                        eMGame.LessonTC.SetNextLesson();
                        eMGame.ResourcesC(whoseMove, ResourceTypes.Wood).Resources += EconomyValues.ForBuyToolWeapon(ToolWeaponTypes.Staff, LevelTypes.First, ResourceTypes.Wood);
                    }
                }
                else if (unitT.IsGod())
                {
                    if (eMGame.LessonTC.LessonT == LessonTypes.SettingGod)
                    {
                        eMGame.LessonTC.SetNextLesson();
                    }
                }


                _setNewUnitS.Set(unitT, whoseMove, cell_0);


                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}