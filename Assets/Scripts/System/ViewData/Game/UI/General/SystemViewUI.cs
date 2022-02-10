﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemViewUI
    {
        readonly Dictionary<UITypes, Action> _actions;

        public SystemViewUI(in Entities ents, in EntitiesView entsView)
        {
            _actions = new Dictionary<UITypes, Action>();


            _actions.Add(UITypes.RunUpdate, default);


            _actions.Add(UITypes.RunFixedUpdate, 
                (Action)

                ///Right
                new RightZoneUIS(ents, entsView).Run
                + new StatsUIS(ents, entsView).Run
                + new ProtectUIS(ents, entsView).Run
                + new RelaxUIS(ents, entsView).Run
                + new UniqueButtonUIS(ents, entsView).Run
                + new FirstButtonBuildUIS(ents, entsView).Run
                + new SecButtonBuildUISys(ents, entsView).Run
                + new ShieldUIS(ents, entsView).Run
                + new RightEffectsUIS(ents, entsView).Run


                ///Down
                + new DonerUIS(ents, entsView).Run
                + new GetterUnitsUIS(ents, entsView).Run
                + new DownToolWeaponUIS(ents, entsView).Run
                + new ScoutSyncUIS(ents, entsView).Run
                + new DownHeroUIS(ents, entsView).Run

                ///Up
                + new EconomyUpUIS(ents, entsView).Run
                + new WindUIS(ents, entsView).Run
                + new UpSunsUIS(ents, entsView).Run

                ///Center
                + new SelectorUIS(ents, entsView).Run
                + new TheEndGameUIS(ents, entsView).Run
                + new MotionCenterUIS(ents, entsView).Run
                + new ReadyZoneUIS(ents, entsView).Run
                + new MistakeUIS().Run
                + new KingZoneUISys(ents, entsView).Run
                + new FriendZoneUISys(ents, entsView).Run
                + new PickUpgUIS(ents, entsView).Run
                + new HeroesSyncUIS(ents, entsView).Run

                ///Left
                + new LeftZonesUIS(ents, entsView.UIEs).Run
                + new EnvUIS(ents, entsView.UIEs).Run);
        }

        public void Run(in UITypes type)
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

        End
    }
}