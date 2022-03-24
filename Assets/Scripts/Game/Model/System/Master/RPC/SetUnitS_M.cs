using Chessy.Common.Entity;
using Chessy.Common.Extension;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public readonly struct SetUnitS_M
    {
        readonly SetNewUnitS _setNewUnitS;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesModelCommon _eMCommon;

        public SetUnitS_M(in SetNewUnitS setNewUnitS, in EntitiesModelGame eMGame, in EntitiesModelCommon eMCommon)
        {
            _setNewUnitS = setNewUnitS;
            _eMGame = eMGame;
            _eMCommon = eMCommon;
        }

        public void Set(in byte idx_0, in UnitTypes unitT, in Player sender)
        {
            var whoseMove = _eMGame.WhoseMove.Player;

            if (_eMGame.CellEs(idx_0).CellE.IsStartedCell(whoseMove) && !_eMGame.UnitTC(idx_0).HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (_eMGame.LessonTC.LessonT == LessonTypes.SettingKing)
                    {
                        _eMGame.LessonTC.SetNextLesson();
                    }
                }
                else if (unitT == UnitTypes.Pawn)
                {
                    if (_eMGame.LessonTC.LessonT == LessonTypes.SettingPawn)
                    {
                        _eMGame.LessonTC.SetNextLesson();
                        _eMGame.ResourcesC(whoseMove, ResourceTypes.Wood).Resources += EconomyValues.ForBuyToolWeapon(ToolWeaponTypes.Staff, LevelTypes.First, ResourceTypes.Wood);
                    }
                }
                else if (unitT.IsGod())
                {
                    if (_eMGame.LessonTC.LessonT == LessonTypes.SettingGod)
                    {
                        _eMGame.LessonTC.SetNextLesson();
                    }
                }


                _setNewUnitS.SetNewUnitHere(_eMGame.UnitEs(idx_0), unitT, whoseMove, _eMGame.PlayerInfoE(whoseMove), _eMGame);


                _eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}