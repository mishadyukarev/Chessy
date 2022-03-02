using System;

namespace Chessy.Game
{
    public readonly struct SystemViewUI
    {
        public SystemViewUI(ref ActionC update, ref ActionC fixedUpdate, in Resources res,  in EntitiesViewUI entsUI, in EntitiesModel ents, out Action updateUI)
        {
            updateUI = (Action)

                ///Right
                new RightZoneUIS(entsUI, ents).Run
                + new StatsUIS(entsUI, ents).Run
                + new RightUnitProtectUIS(entsUI, ents).Run
                + new RelaxUIS(entsUI, ents).Run
                + new UniqueButtonUIS(res, entsUI, ents).Run
                + new ShieldUIS(entsUI, ents).Run
                + new RightEffectsUIS(res, entsUI, ents).Run


                ///Down
                + new DonerUIS(entsUI.DownEs.DonerE, ents).Run
                + new DownPawnUIS(entsUI.DownEs.PawnE, ents).Run
                + new DownToolWeaponUIS(entsUI.DownEs.ToolWeaponE, ents).Run
                + new DownScoutUIS(entsUI.DownEs.ScoutE, ents).Run
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
                + new CenterPickFractionUIS(entsUI, ents).Run
                + new CenterHeroesUIS(entsUI, ents).Run
                + new CenterMistakeUIS(entsUI, ents).Run
                + new CenterMotionUIS(entsUI, ents).Run
                + new CenterBuildingZonesUIS(entsUI, ents).Run

                ///Left
                + new LeftZonesUIS(entsUI, ents).Run
                + new EnvUIS(entsUI, ents).Run;



            fixedUpdate.Action += updateUI;
        }
    }
}