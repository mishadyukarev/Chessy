﻿using System;
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


            _actions.Add(ViewDataSTypes.RunUpdate, new SoundClickCellS().Run);


            _actions.Add(ViewDataSTypes.RunFixedUpdate,
                (Action)new SyncCellUnitVS().Run
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


                + new BuildZoneUIS().Run
                + new EnvUIS().Run

                ///right
                + new RightZoneUIS().Run
                + new StatsUIS().Run
                + new ProtectUIS().Run
                + new RelaxUIS().Run
                + new UniqButSyncUISys().Run
                + new FirstButtonBuildUIS().Run
                + new SecButtonBuildUISys().Run
                + new ThirdButtonBuildUISys().Run
                + new ShieldUIS().Run
                + new EffectsUISys().Run

                ///down
                + new DonerUIS().Run
                + new GetterUnitsUISystem().Run
                + new DownToolWeaponUIS().Run
                + new ScoutSyncUIS().Run
                + new DownHeroUIS().Run

                ///up
                + new EconomyUpUIS().Run
                + new WindUISys().Run

                ///center
                + new SelectorUIS().Run
                + new TheEndGameUISystem().Run
                + new MotionCenterUIS().Run
                + new ReadyZoneUISystem().Run
                + new MistakeUISys().Run
                + new KingZoneUISys().Run
                + new FriendZoneUISys().Run
                + new ActiveHitUISys().Run
                + new PickUpgUIS().Run
                + new HeroesSyncUISys().Run

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