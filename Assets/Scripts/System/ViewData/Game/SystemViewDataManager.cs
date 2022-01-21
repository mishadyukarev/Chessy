﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemViewDataManager
    {
        static Dictionary<ViewDataSystemTypes, Action> _actions;


        public SystemViewDataManager(in bool def)
        {
            _actions = new Dictionary<ViewDataSystemTypes, Action>();


            _actions.Add(ViewDataSystemTypes.RunUpdate, default);


            _actions.Add(ViewDataSystemTypes.RunFixedUpdate,
                (Action)
                new CellUnitVS().Run
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
                + new CellIceWallVS().Run

                + new RotateAllVS().Run
                + new SoundVS().Run);
        }

        public static void Run(in ViewDataSystemTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}