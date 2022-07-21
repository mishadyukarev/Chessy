using Chessy.Model.Entity;
using System;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class UpSunsUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;
        bool _needActiveLeft;
        bool _needActiveRight;

        internal UpSunsUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            _needActiveLeft = false;
            _needActiveRight = false;


            if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= Enum.LessonTypes.LookInfoAboutSun)
            {
                var isFirstPlayer = _aboutGameC.CurrentPlayerIType == PlayerTypes.First;

                switch (_sunC.SunSideType)
                {
                    case SunSideTypes.Dawn:
                        _needActiveLeft = isFirstPlayer;
                        _needActiveRight = !isFirstPlayer;
                        break;

                    case SunSideTypes.Center:
                        break;

                    case SunSideTypes.Sunset:
                        _needActiveLeft = !isFirstPlayer;
                        _needActiveRight = isFirstPlayer;
                        //eUI.UpEs.SunsE.RightSun.SetActive(isFirstPlayer ? true : false);
                        //eUI.UpEs.SunsE.LeftSun.SetActive(isFirstPlayer ? false : true);
                        break;

                    case SunSideTypes.Night:
                        break;

                    default: throw new Exception();
                }
            }

            eUI.UpEs.SunsE.LeftSun.SetActive(_needActiveLeft);
            eUI.UpEs.SunsE.RightSun.SetActive(_needActiveRight);
        }
    }
}