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
        readonly SystemsModelGame _sMGame;

        internal SetUnitS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        internal void Set(in UnitTypes unitT, in Player sender, in byte cell)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.CellEs(cell).CellE.IsStartedCell(whoseMove) && !e.UnitMainE(cell).UnitTC.HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (e.LessonTC.LessonT == LessonTypes.SettingKing)
                    {
                        e.LessonTC.SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (e.LessonTC.LessonT == LessonTypes.SettingGod)
                    {
                        e.LessonTC.SetNextLesson();
                    }
                }


                _sMGame.SetNewUnitS.Set(unitT, whoseMove, cell);


                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}