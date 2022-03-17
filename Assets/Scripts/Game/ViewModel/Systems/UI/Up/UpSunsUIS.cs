using System;

namespace Chessy.Game
{
    sealed class UpSunsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal UpSunsUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            switch (E.WeatherE.SunC.SunSide)
            {
                case SunSideTypes.Dawn:
                    UIE.UpEs.SunsE.RightSun.SetActive(false);
                    UIE.UpEs.SunsE.LeftSun.SetActive(true);
                    break;

                case SunSideTypes.Center:
                    UIE.UpEs.SunsE.RightSun.SetActive(false);
                    UIE.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    UIE.UpEs.SunsE.RightSun.SetActive(true);
                    UIE.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Night:
                    UIE.UpEs.SunsE.RightSun.SetActive(false);
                    UIE.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                default: throw new Exception();
            }
        }
    }
}