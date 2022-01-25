using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct SystemViewDataUIManager
    {
        static Dictionary<UITypes, Action> _actions;

        public SystemViewDataUIManager(in bool def)
        {
            _actions = new Dictionary<UITypes, Action>();


            _actions.Add(UITypes.RunUpdate, default);


            _actions.Add(UITypes.RunFixedUpdate,
                (Action)

                ///Right
                new RightZoneUIS().Run
                + new StatsUIS().Run
                + new ProtectUIS().Run
                + new RelaxUIS().Run
                + new UniqueButtonUIS().Run
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
                + new UpSunsUIS().Run

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


                + new BuildZoneUIS().Run
                + new EnvUIS().Run);


            _actions.Add(UITypes.RunAfterRPCSync, default);
        }

        public static void Run(in UITypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
    public enum UITypes
    {
        None,

        RunUpdate,
        RunFixedUpdate,

        RunAfterRPCSync,

        End
    }
}