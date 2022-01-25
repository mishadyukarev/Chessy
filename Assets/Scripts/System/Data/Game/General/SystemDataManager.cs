using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemDataManager
    {
        static Dictionary<DataSTypes, Action> _actions;


        public SystemDataManager(in bool def)
        {
            _actions = new Dictionary<DataSTypes, Action>();

            _actions.Add(DataSTypes.RunUpdate,
                (Action)
                new InputS().Run
                + new RayS().Run
                + new SelectorS().Run);


            _actions.Add(DataSTypes.RunFixedUpdate,
                (Action)default);


            _actions.Add(DataSTypes.RunAfterSyncRPC,
                (Action)
                new VisibElseS().Run
                + new UniqueAbilitySyncS().Run
                + new GetCellsForSetUnitS().Run
                + new GetCellsForShiftUnitS().Run
                + new GetCellsForArsonArcherS().Run

                + new GetAttackPawnKingCellsS().Run
                + new GetCellsForAttackArcherS().Run);
        }


        public static void Run(in DataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}