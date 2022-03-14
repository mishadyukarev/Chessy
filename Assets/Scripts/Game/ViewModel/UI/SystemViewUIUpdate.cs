using Chessy.Common;
using Chessy.Game.View.UI.System;
using UnityEngine;

namespace Chessy.Game.System.View.UI
{
    public static class SystemViewUIUpdate
    {
        static float _timer;

        public static void Run(in float timer, in EntitiesViewUI eUI, in EntitiesModel e)
        {
            ///Center
            CenterMotionUIS.Run(timer, eUI, e);
            CenterMistakeUIS.Run(timer, eUI, e);



            //if (e.NeedUpdateUI)
            //{
            //    if (_timer > 0.07f)
            //    {
                    ///Right
                    new RightZoneUIS(eUI, e).Run();
                    new StatsUIS(eUI, e).Run();
                    new RightUnitProtectUIS(eUI, e).Run();
                    new RelaxUIS(eUI.RightEs.RelaxE, e).Run();
                    new ShieldUIS(eUI, e).Run();
                    new RightEffectsUIS(e.Resources, eUI, e).Run();
                    for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        new UniqueButtonUIS(buttonT, eUI.RightEs.Unique(buttonT), e.Resources, e).Run();
                    }


                    ///Down
                    new DonerUIS(eUI.DownEs.DonerE, e).Run();
                    new DownPawnUIS(eUI.DownEs.PawnE, e).Run();
                    new DownToolWeaponUIS(eUI.DownEs.ToolWeaponE, e).Run();
                    new DownHeroUIS(eUI.DownEs.HeroE, e).Run();


                    ///Up
                    new EconomyUpUIS(eUI, e).Run();
                    new UpWindUIS(eUI, e).Run();
                    new UpSunsUIS(eUI, e).Run();


                    ///Center
                    new CenterSelectorUIS(eUI, e).Run();
                    new CenterEndGameUIS(eUI, e).Run();
                    new CenterReadyUIS(eUI, e).Run();
                    new CenterKingUIS(eUI, e).Run();
                    new CenterFriendUIS(eUI, e).Run();
                    new CenterHeroesUIS(eUI, e).Run();
                    new CenterBuildingZonesUIS(eUI, e).Run();


                    ///Left
                    new LeftZonesUIS(eUI, e).Run();
                    new EnvUIS(eUI, e).Run();
                    new LeftCityUIS(eUI, e).Run();

                //    e.NeedUpdateUI = false;
                //    _timer = 0;
                //}

                //_timer += Time.deltaTime;
            //}
        }
    }
}