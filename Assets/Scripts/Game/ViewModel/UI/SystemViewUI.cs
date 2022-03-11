﻿using Chessy.Common;
using System;

namespace Chessy.Game.View.UI.System
{
    public readonly struct SystemViewUI
    {
        public SystemViewUI(ref ActionC update, in Resources res, in EntitiesViewUI entsUI, in EntitiesModel ents)
        {
            update.Action += (Action)

            ///Right
            new RightZoneUIS(entsUI, ents).Run
            + new StatsUIS(entsUI, ents).Run
            + new RightUnitProtectUIS(entsUI, ents).Run
            + new RelaxUIS(entsUI, ents).Run
            + new ShieldUIS(entsUI, ents).Run
            + new RightEffectsUIS(res, entsUI, ents).Run;
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                update.Action += new UniqueButtonUIS(buttonT, entsUI.RightEs.Unique(buttonT), res, ents).Run;
            }


            ///Down
            update.Action += (Action)
            new DonerUIS(entsUI.DownEs.DonerE, ents).Run
            + new DownPawnUIS(entsUI.DownEs.PawnE, ents).Run
            + new DownToolWeaponUIS(entsUI.DownEs.ToolWeaponE, ents).Run
            + new DownHeroUIS(entsUI.DownEs.HeroE, ents).Run


            ///Up
            + new EconomyUpUIS(entsUI, ents).Run
            + new UpWindUIS(entsUI, ents).Run
            + new UpSunsUIS(entsUI, ents).Run


            ///Center
            + new CenterSelectorUIS(entsUI, ents).Run
            + new CenterEndGameUIS(entsUI, ents).Run
            + new CenterReadyUIS(entsUI, ents).Run
            + new CenterKingUIS(entsUI, ents).Run
            + new CenterFriendUIS(entsUI, ents).Run
            + new CenterHeroesUIS(entsUI, ents).Run
            + new CenterMistakeUIS(entsUI, ents).Run
            + new CenterMotionUIS(entsUI, ents).Run
            + new CenterBuildingZonesUIS(entsUI, ents).Run


            ///Left
            + new LeftZonesUIS(entsUI, ents).Run
            + new EnvUIS(entsUI, ents).Run
            + new LeftCityUIS(entsUI, ents).Run;
        }
    }
}