using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SystemDataManager
    {
        public SystemDataManager()
        {
            var list = new List<object>();
            var dataSC = new Dictionary<DataSTypes, Action>();
            list.Add(dataSC);


            var action =
                (Action)new InputS().Run
                + new RayS().Run
                + new SelectorS().Run;
            dataSC.Add(DataSTypes.RunUpdate, action);

            dataSC.Add(DataSTypes.RunFixedUpdate, new SoundS().Run);


            action =
                (Action)new VisibElseS().Run
                + new AbilSyncMS().Run
                + new ClearAvailCellsS().Run
                + new GetAttackPawnCellsS().Run
                + new GetSetUnitCellsS().Run;

            dataSC.Add(DataSTypes.RunAfterUpdate, action);

            new DataSC(list);
        }
    }
}