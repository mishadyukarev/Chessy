using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

namespace Chessy.Model
{
    sealed class DownPawnUIS : SystemUIAbstract
    {
        readonly DownPawnUIE _pawnE;

        internal DownPawnUIS(in DownPawnUIE pawnE, in EntitiesModel ents) : base(ents)
        {
            _pawnE = pawnE;
        }

        internal override void Sync()
        {
            if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= LessonTypes.SettingPawn)
            {
                _pawnE.ParenGOC.TrySetActive(true);

                var curPlayerI = _aboutGameC.CurrentPlayerIType;

                _pawnE.AmountTextC.TextUI.text = _e.PawnPeopleInfoC(curPlayerI).AmountInGame.ToString() + "/" + _e.PawnPeopleInfoC(curPlayerI).MaxAvailablePawns(_e.PlayerInfoC(curPlayerI).AmountBuiltHousesP);
                _pawnE.MaxPawnsTextC.TextUI.text = _e.PawnPeopleInfoC(curPlayerI).PeopleInCity.ToString();
            }
            else
            {
                _pawnE.ParenGOC.TrySetActive(false);
            }
        }
    }
}