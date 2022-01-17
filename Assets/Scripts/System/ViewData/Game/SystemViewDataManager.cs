using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemViewDataManager
    {
        readonly static Dictionary<ViewDataSTypes, Action> _actions;

        static SystemViewDataManager()
        {
            _actions = new Dictionary<ViewDataSTypes, Action>();
        }
        public SystemViewDataManager(in bool def)
        {
            new CenterEventUIS();
            new LeftCityEventUISys();
            new LeftEnvEventUISys();
            new DownEventUIS();
            new RightUnitEventUIS();
            new UpEventUIS();


            _actions.Add(ViewDataSTypes.RunUpdate, default);


            _actions.Add(ViewDataSTypes.RunFixedUpdate,
                (Action)new SyncCellUnitVS().Run
                + new UnitStatCellSyncS().Run
                + new BuildCellVS().Run
                + new EnvCellVS().Run
                + new FireCellVS().Run
                + new CloudCellVS().Run
                + new RiverCellVS().Run
                + new CellBarsEnvVS().Run
                + new CellTrailVS().Run
                + new CellStunVS().Run
                + new SyncSelUnitCellVS().Run
                + new SupportCellVS().Run
                + new FliperAndRotatorUnitVS().Run


                + new BuildZoneUIS().Run
                + new EnvUIS().Run

                ///Right
                + new RightZoneUIS().Run
                + new StatsUIS().Run
                + new ProtectUIS().Run
                + new RelaxUIS().Run
                + new UniqButSyncUIS().Run
                + new FirstButtonBuildUIS().Run
                + new SecButtonBuildUISys().Run
                + new ThirdBuildButtonUIS().Run
                + new ShieldUIS().Run
                + new EffectsUISys().Run

                ///Down
                + new DonerUIS().Run
                + new GetterUnitsUIS().Run
                + new DownToolWeaponUIS().Run
                + new ScoutSyncUIS().Run
                + new DownHeroUIS().Run

                ///Up
                + new EconomyUpUIS().Run
                + new WindUIS().Run

                ///Center
                + new SelectorUIS().Run
                + new TheEndGameUIS().Run
                + new MotionCenterUIS().Run
                + new ReadyZoneUIS().Run
                + new MistakeUIS().Run
                + new KingZoneUISys().Run
                + new FriendZoneUISys().Run
                + new PickUpgUIS().Run
                + new HeroesSyncUIS().Run

                + new RotateAllVS().Run


                + new SoundVS().Run);
        }

        public static void Run(ViewDataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}