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


            dataSC.Add(DataSTypes.RunUpdate, 
                (Action)new InputS().Run
                + new RayS().Run
                + new SelectorS().Run);

            dataSC.Add(DataSTypes.RunFixedUpdate,
                (Action)new SoundS().Run);

            dataSC.Add(DataSTypes.RunAfterUpdate,
                (Action)new VisibElseS().Run
                + new AbilSyncMS().Run
                + new ClearAvailCellsS().Run
                + new GetAttackPawnCellsS().Run
                + new GetSetUnitCellsS().Run);

            new DataSC(list);
        }
    }
}