using System;

namespace Chessy.Game
{
    public readonly struct SystemViewUI
    {
        public SystemViewUI(ref ActionC update, ref ActionC fixedUpdate, in Resources res, in EntitiesModel ents, in EntitiesViewUI entsUI, out Action updateUI)
        {
            updateUI = (Action)

                ///Right
                new RightZoneUIS(ents, entsUI).Run
                + new StatsUIS(ents, entsUI).Run
                + new RightUnitProtectUIS(ents, entsUI).Run
                + new RelaxUIS(ents, entsUI).Run
                + new UniqueButtonUIS(res, ents, entsUI).Run
                + new ShieldUIS(ents, entsUI).Run
                + new RightEffectsUIS(res, ents, entsUI).Run


                ///Down
                + new DonerUIS(ents, entsUI).Run
                + new DownPawnUIS(ents, entsUI).Run
                + new DownToolWeaponUIS(ents, entsUI).Run
                + new DownScoutUIS(ents, entsUI).Run
                + new DownHeroUIS(ents, entsUI).Run

                ///Up
                + new EconomyUpUIS(ents, entsUI).Run
                + new UpWindUIS(ents, entsUI).Run
                + new UpSunsUIS(ents, entsUI).Run

                ///Center
                + new CenterSelectorUIS(ents, entsUI).Run
                + new CenterEndGameUIS(ents, entsUI).Run
                + new CenterReadyUIS(ents, entsUI).Run
                + new CenterKingUIS(ents, entsUI).Run
                + new CenterFriendUIS(ents, entsUI).Run
                + new CenterPickFractionUIS(ents, entsUI).Run
                + new CenterHeroesUIS(ents, entsUI).Run
                + new CenterMistakeUIS(ents, entsUI).Run
                + new CenterMotionUIS(ents, entsUI).Run

                ///Left
                + new LeftZonesUIS(ents, entsUI).Run
                + new EnvUIS(ents, entsUI).Run;



            fixedUpdate.Action += updateUI;
        }
    }
}