using System;

namespace Chessy.Game
{
    sealed class UpSunsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal UpSunsUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            switch (E.SunSideTC.SunSide)
            {
                case SunSideTypes.Dawn:
                    UIEs.UpEs.SunsE.RightSun.SetActive(false);
                    UIEs.UpEs.SunsE.LeftSun.SetActive(true);
                    break;

                case SunSideTypes.Center:
                    UIEs.UpEs.SunsE.RightSun.SetActive(false);
                    UIEs.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    UIEs.UpEs.SunsE.RightSun.SetActive(true);
                    UIEs.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                case SunSideTypes.Night:
                    UIEs.UpEs.SunsE.RightSun.SetActive(false);
                    UIEs.UpEs.SunsE.LeftSun.SetActive(false);
                    break;

                default: throw new Exception();
            }
        }
    }
}