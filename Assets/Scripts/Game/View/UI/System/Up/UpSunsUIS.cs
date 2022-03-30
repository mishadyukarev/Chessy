﻿using System;

namespace Chessy.Game
{
    sealed class UpSunsUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal UpSunsUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        public void Run()
        {
            switch (e.WeatherE.SunC.SunSide)
            {
                case SunSideTypes.Dawn:
                    eUI.UpEs.SunsE.RightSun.SetActive(false);
                    eUI.UpEs.SunsE.LeftSun.SetActive(true);
                    break;

                case SunSideTypes.Center:
                    eUI.UpEs.SunsE.RightSun.SetActive(false);
                    eUI.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    eUI.UpEs.SunsE.RightSun.SetActive(true);
                    eUI.UpEs.SunsE.LeftSun.SetActive(false);
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