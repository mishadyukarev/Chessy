using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class ViewDataSCreate
    {
        public ViewDataSCreate(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;

            var list = new List<object>();
            var actions = new Dictionary<ViewDataSTypes, Action>();
            list.Add(actions);






            var eventExecuters = new EcsSystems(gameWorld)
                .Add(new CenterEventUIS())
                .Add(new LeftCityEventUISys())
                .Add(new LeftEnvEventUISys())
                .Add(new DownEventUIS())
                .Add(new RightUnitEventUISys())
                .Add(new UpEventUIS());

            gameSysts.Add(eventExecuters);









            //actions.Add(ViewDataSTypes.RotateAll, rotateAll.Run);
            //gameSysts.Add(rotateAll);



            var runUpdate = new EcsSystems(gameWorld)
                .Add(new SoundClickCellS());

            actions.Add(ViewDataSTypes.RunUpdate, runUpdate.Run);
            gameSysts.Add(runUpdate);



            var syncCell = new EcsSystems(gameWorld)
                .Add(new SyncCellUnitViewSys())
                .Add(new UnitStatCellSyncS())
                .Add(new BuildCellSyncVS())
                .Add(new EnvCellSyncVS())
                .Add(new FireCellSyncVS())
                .Add(new CloudCellSyncVS())
                .Add(new RiverCellSyncVS())
                .Add(new CellBarsEnvSystem())
                .Add(new SyncCellTrailSys())
                .Add(new CellStunViewS())
                .Add(new SyncSelUnitCellVS())
                .Add(new SupportSyncVS())
                .Add(new FliperAndRotatorUnitSystem());


            var syncCanvas = new EcsSystems(gameWorld)
                ///left
                .Add(new BuildZoneUISys())
                .Add(new EnvUIS())

                ///right
                .Add(new RightZoneUISys())
                .Add(new StatsUISystem())
                .Add(new ProtectUISys())
                .Add(new RelaxUISys())
                .Add(new UniqButSyncUISys())
                .Add(new FirstButtonBuildUISys())
                .Add(new SecButtonBuildUISys())
                .Add(new ThirdButtonBuildUISys())
                .Add(new ShieldUISys())
                .Add(new EffectsUISys())

                ///down
                .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new GiveTakeUISystem())
                .Add(new ScoutSyncUIS())
                .Add(new HeroSyncUIS())

                ///up
                .Add(new EconomyUpUISys())
                .Add(new WindUISys())

                ///center
                .Add(new SelectorUISys())
                .Add(new TheEndGameUISystem())
                .Add(new MotionCenterUISystem())
                .Add(new ReadyZoneUISystem())
                .Add(new MistakeUISys())
                .Add(new KingZoneUISys())
                .Add(new FriendZoneUISys())
                .Add(new ActiveHitUISys())
                .Add(new PickUpgUIS())
                .Add(new HeroesSyncUISys());

            var rotateAll = new EcsSystems(gameWorld)
                .Add(new RotateAllVS());

            var runFixedUpd = new EcsSystems(gameWorld)
                .Add(syncCell)
                .Add(syncCanvas)
                .Add(rotateAll);

            actions.Add(ViewDataSTypes.RunFixedUpdate, runFixedUpd.Run);
            gameSysts.Add(runFixedUpd);



            new ViewDataSC(list);
        }
    }
}