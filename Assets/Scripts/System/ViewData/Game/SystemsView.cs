using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemsView
    {
        readonly Dictionary<SystemViewDataTypes, Action> _actions;



        public SystemsView(in Entities ents, in EntitiesView entsView)
        {
            _actions = new Dictionary<SystemViewDataTypes, Action>();


            _actions.Add(SystemViewDataTypes.RunUpdate,
                (Action)
                new CellUnitVS(ents, entsView).Run
                + new CellUnitSelectedVS(ents, entsView).Run
                + new CellSupportVS(ents, entsView).Run);

            _actions.Add(SystemViewDataTypes.RunFixedUpdate,
                (Action)
                new UnitStatCellSyncS(ents, entsView).Run
                + new BuildCellVS(ents, entsView).Run
                + new EnvCellVS(ents, entsView).Run
                + new CellFireVS(ents, entsView).Run
                + new CellCloudVS(ents, entsView).Run
                + new RiverCellVS(ents, entsView).Run
                + new CellBarsEnvVS(ents, entsView).Run
                + new CellTrailVS(ents, entsView).Run
                + new CellUnitEffectFrozenArrawVS(ents, entsView).Run
                

                + new CellUnitEffectStunVS(ents, entsView).Run
                + new CellUnitEffectShieldVS(ents, entsView).Run

                + new RotateAllVS(ents, entsView).Run
                + new SoundVS(ents, entsView).Run);
        }

        public void Run(in SystemViewDataTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }

    public enum SystemViewDataTypes
    {
        None,

        RunUpdate,
        RunFixedUpdate,

        End,
    }
}