using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct Systems
    {
        readonly Dictionary<DataSTypes, Action> _actions;

        public readonly SystemsMaster SystemsMaster;
        public readonly SystemsOther SystemsOther;

        public Systems(in Entities ents, in SystemsView systemsView)
        {
            _actions = new Dictionary<DataSTypes, Action>();

            _actions.Add(DataSTypes.RunUpdate,
                (Action)
                new InputS(ents).Run
                + new RayS(ents).Run
                + new SelectorS(ents, systemsView).Run);


            _actions.Add(DataSTypes.RunFixedUpdate,
                (Action)default);


            _actions.Add(DataSTypes.RunAfterSyncRPC,
                (Action)
                new VisibElseS(ents).Run
                + new AbilitySyncS(ents).Run
                + new GetCellsForSetUnitS(ents).Run
                + new GetCellsForShiftUnitS(ents).Run
                + new GetCellsForArsonArcherS(ents).Run

                + new GetAttackMeleeCellsS(ents).Run
                + new GetCellsForAttackArcherS(ents).Run);



            SystemsMaster = new SystemsMaster(ents);
            SystemsOther = new SystemsOther(ents);
        }
        public void Run(in DataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}