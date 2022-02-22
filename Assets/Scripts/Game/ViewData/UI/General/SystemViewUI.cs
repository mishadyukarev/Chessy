using System;

namespace Game.Game
{
    public readonly struct SystemViewUI
    {
        public SystemViewUI(ref ActionC update, ref ActionC fixedUpdate, in Resources res, in EntitiesModel ents, in EntitiesViewUI entsUI, out Action updateUI)
        {
            update.Action +=
                 (Action)
                new MistakeUIS(ents, entsUI).Run
                + new MotionCenterUIS(ents, entsUI).Run;

            updateUI = (Action)

                ///Right
                new RightZoneUIS(ents, entsUI).Run
                + new StatsUIS(ents, entsUI).Run
                + new ProtectUIS(ents, entsUI).Run
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
                + new WindUIS(ents, entsUI).Run
                + new UpSunsUIS(ents, entsUI).Run

                ///Center
                + new SelectorUIS(ents, entsUI).Run
                + new TheEndGameUIS(ents, entsUI).Run
                + new ReadyZoneUIS(ents, entsUI).Run
                + new KingZoneUIS(ents, entsUI).Run
                + new FriendZoneUISys(ents, entsUI).Run
                + new PickUpgUIS(ents, entsUI).Run
                + new HeroesSyncUIS(ents, entsUI).Run

                ///Left
                + new LeftZonesUIS(ents, entsUI).Run
                + new EnvUIS(ents, entsUI).Run;



            fixedUpdate.Action += updateUI;
        }
    }
}