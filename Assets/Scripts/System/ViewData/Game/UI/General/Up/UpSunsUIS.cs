using System;

namespace Game.Game
{
    sealed class UpSunsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal UpSunsUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            switch (Es.SunSidesE.SunSideTC.SunSide)
            {
                case SunSideTypes.Dawn:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(true);
                    break;

                case SunSideTypes.Center:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    UpSunsUIEs.ImageC(true).SetActive(true);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                case SunSideTypes.Night:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                default: throw new Exception();
            }
        }
    }
}