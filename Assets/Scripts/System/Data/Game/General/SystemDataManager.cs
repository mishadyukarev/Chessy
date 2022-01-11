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

            _actions.Add(DataSTypes.RunAfterUpdate,
                (Action)new VisibElseS().Run
                + new AbilSyncS().Run
                + new ClearAvailCellsS().Run
                + new GetAttackPawnCellsS().Run
                + new GetSetUnitCellsS().Run);
        }


        public static void Run(in DataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type]?.Invoke();
        }
    }
}