using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct Systems
    {
        readonly Dictionary<SystemDataTypes, Action> _actions;

        public readonly SystemsMaster SystemsMaster;
        public readonly SystemsOther SystemsOther;

        public Systems(in Entities ents, in SystemsView systemsView)
        {
            _actions = new Dictionary<SystemDataTypes, Action>();

            _actions.Add(SystemDataTypes.RunUpdate,
                (Action)
                new InputS(ents).Run
                + new RayS(ents).Run
                + new SelectorS(ents, systemsView).Run);


            _actions.Add(SystemDataTypes.RunFixedUpdate,
                (Action)default);


            _actions.Add(SystemDataTypes.RunAfterSyncRPC,
                (Action)
                new GetCurentPlayerS(ents).Run

                + new SetIdxsBuildingsS(ents).Run
                + new VisibElseS(ents).Run
                + new AbilitySyncS(ents).Run
                + new GetDamageUnitsS(ents).Run
                + new GetCellsForSetUnitS(ents).Run
                + new GetCellsForShiftUnitS(ents).Run
                + new GetCellsForArsonArcherS(ents).Run

                + new GetUnitTypeS(ents).Run

                + new GetAttackMeleeCellsS(ents).Run
                + new GetCellsForAttackArcherS(ents).Run);



            SystemsMaster = new SystemsMaster(ents);
            SystemsOther = new SystemsOther(ents);
        }
        public void Run(in SystemDataTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}