using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SystemViewDataManager
    {
        public SystemViewDataManager()
        {
            new CenterEventUIS();
            new LeftCityEventUISys();
            new LeftEnvEventUISys();
            new DownEventUIS();
            new RightUnitEventUISys();
            new UpEventUIS();


            var actions = new Dictionary<ViewDataSTypes, Action>();

            actions.Add(ViewDataSTypes.RunUpdate, new SoundClickCellS().Run);


            actions.Add(ViewDataSTypes.RunFixedUpdate,
                (Action)new SyncCellUnitViewSys().Run
                + new UnitStatCellSyncS().Run
                + new BuildCellSyncVS().Run
                + new EnvCellSyncVS().Run
                + new FireCellSyncVS().Run
                + new CloudCellSyncVS().Run
                + new RiverCellSyncVS().Run
                + new CellBarsEnvS().Run
                + new SyncCellTrailSys().Run
                + new CellStunViewS().Run
                + new SyncSelUnitCellVS().Run
                + new SupportSyncVS().Run
                + new FliperAndRotatorUnitSystem().Run


                + new BuildZoneUISys().Run
                + new EnvUIS().Run

                ///right
                + new RightZoneUISys().Run
                + new StatsUISystem().Run
                + new ProtectUISys().Run
                + new RelaxUISys().Run
                + new UniqButSyncUISys().Run
                + new FirstButtonBuildUISys().Run
                + new SecButtonBuildUISys().Run
                + new ThirdButtonBuildUISys().Run
                + new ShieldUISys().Run
                + new EffectsUISys().Run

                ///down
                + new DonerUISystem().Run
                + new GetterUnitsUISystem().Run
                + new GiveTakeUISystem().Run
                + new ScoutSyncUIS().Run
                + new HeroSyncUIS().Run

                ///up
                + new EconomyUpUIS().Run
                + new WindUISys().Run

                ///center
                + new SelectorUISys().Run
                + new TheEndGameUISystem().Run
                + new MotionCenterUISystem().Run
                + new ReadyZoneUISystem().Run
                + new MistakeUISys().Run
                + new KingZoneUISys().Run
                + new FriendZoneUISys().Run
                + new ActiveHitUISys().Run
                + new PickUpgUIS().Run
                + new HeroesSyncUISys().Run

                + new RotateAllVS().Run);

            new ViewDataSC(actions);
        }
    }
}