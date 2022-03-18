using Chessy.Common;
using Chessy.Game.System.Model;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI.System;
using UnityEngine;

namespace Chessy.Game.System.View.UI
{
    public readonly struct SystemViewUIUpdate
    {
        public void Run(in float timer, in SystemsViewUI systems, in EntitiesViewUI eUI, in EntitiesModel e)
        {
            ///Right
            new RightZoneUIS(eUI, e).Run();
            new StatsUIS(eUI, e).Run();
            ProtectUIS.Run(eUI.RightEs.ProtectE, e);
            systems.RelaxS.Run(eUI.RightEs.RelaxE, e);
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
            eUI.DownEs.CostE.Sync(e);


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
            MotionUIS.Sync(timer, eUI, e);
            eUI.CenterEs.MistakeE.Sync(timer, e);

            ///Left
            new LeftZonesUIS(eUI, e).Run();
            new EnvUIS(eUI, e).Run();
            new LeftCityUIS(eUI, e).Run();
        }
    }
}