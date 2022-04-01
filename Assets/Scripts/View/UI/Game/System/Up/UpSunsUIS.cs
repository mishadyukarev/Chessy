using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game
{
    sealed class UpSunsUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal UpSunsUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        public void Run()
        {
            var isFirstPlayer = e.CurPlayerITC.Is(PlayerTypes.First);

            switch (e.WeatherE.SunSideTC.SunSide)
            {
                case SunSideTypes.Dawn:
                    eUI.UpEs.SunsE.RightSun.SetActive(isFirstPlayer ? false : true);
                    eUI.UpEs.SunsE.LeftSun.SetActive(isFirstPlayer ? true : false);
                    break;

                case SunSideTypes.Center:
                    eUI.UpEs.SunsE.RightSun.SetActive(false);
                    eUI.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    eUI.UpEs.SunsE.RightSun.SetActive(isFirstPlayer ? true : false);
                    eUI.UpEs.SunsE.LeftSun.SetActive(isFirstPlayer ? false : true);
                    break;

                case SunSideTypes.Night:
                    eUI.UpEs.SunsE.RightSun.SetActive(false);
                    eUI.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                default: throw new Exception();
            }
        }
    }
}