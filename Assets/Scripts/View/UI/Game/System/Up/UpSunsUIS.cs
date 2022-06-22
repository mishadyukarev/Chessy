using Chessy.Game.Model.Entity;
using Chessy.Game.System;
using System;

namespace Chessy.Game
{
    sealed class UpSunsUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;
        bool _needActiveLeft;
        bool _needActiveRight;

        internal UpSunsUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
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

                switch (_e.WeatherE.SunSideT)
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