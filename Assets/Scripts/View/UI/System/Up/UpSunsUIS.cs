using Chessy.Model.Model.Entity;
using System;

namespace Chessy.Model
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


            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.LookInfoAboutSun)
            {
                var isFirstPlayer = _e.CurPlayerIT == PlayerTypes.First;

                switch (_e.WeatherE.SunC.SunSideT)
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