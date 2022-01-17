using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemDataManager
    {
        readonly static Dictionary<DataSTypes, Action> _actions;

        static SystemDataManager()
        {
            _actions = new Dictionary<DataSTypes, Action>();
        }
        public SystemDataManager(in bool def)
        {
            _actions.Add(DataSTypes.RunUpdate,
                (Action)new InputS().Run
                + new RayS().Run
                + new SelectorS().Run);

            _actions.Add(DataSTypes.RunFixedUpdate,
                (Action)default);

            _actions.Add(DataSTypes.RunAfterSyncRPC,
                (Action)new VisibElseS().Run
                + new AbilSyncS().Run
                + new GetAttackPawnKingCellsS().Run
                + new GetAttackKingCellsS().Run
                + new GetCellsForSetUnitS().Run
                + new GetCellsForShiftUnitS().Run
                + new GetCellsForArsonArcherS().Run);
        }


        public static void Run(in DataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}