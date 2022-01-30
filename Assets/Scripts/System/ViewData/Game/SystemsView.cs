using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SystemsView
    {
        static Dictionary<ViewDataSystemTypes, Action> _actions;


        public SystemsView(in Entities ents, in EntitiesView entsView)
        {
            _actions = new Dictionary<ViewDataSystemTypes, Action>();


            _actions.Add(ViewDataSystemTypes.RunUpdate, default);


            _actions.Add(ViewDataSystemTypes.RunFixedUpdate,
                (Action)
                new CellUnitVS(ents, entsView).Run
                + new UnitStatCellSyncS(ents, entsView).Run
                + new BuildCellVS(ents, entsView).Run
                + new EnvCellVS(ents, entsView).Run
                + new FireCellVS(ents, entsView).Run
                + new CloudCellVS(ents, entsView).Run
                + new RiverCellVS(ents, entsView).Run
                + new CellBarsEnvVS(ents, entsView).Run
                + new CellTrailVS(ents, entsView).Run
                + new CellStunVS(ents, entsView).Run
                + new SyncSelUnitCellVS(ents, entsView).Run
                + new SupportCellVS(ents, entsView).Run
                + new FliperAndRotatorUnitVS(ents, entsView).Run

                + new RotateAllVS(ents, entsView).Run
                + new SoundVS(ents, entsView).Run);


            new SystemViewDataUIManager(ents, entsView);
        }

        public static void Run(in ViewDataSystemTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}