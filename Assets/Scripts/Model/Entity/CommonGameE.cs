using Chessy.Model.Component;
using System;
using System.Collections.Generic;

namespace Chessy.Model.Entity
{
    public sealed class CommonGameE
    {
        internal readonly RpcPoolC RpcC;
        public readonly CommonInfoAboutGameC CommonInfoAboutGameC;
        public readonly DataFromViewC DataFromViewC;

        public readonly FromResourcesC Resources = new();
        public readonly ShopC ShopC = new();
        public readonly AdC AdC = new(DateTime.Now);
        public readonly UpdateAllViewC UpdateAllViewC = new();
        public readonly SettingsC SettingsC = new();
        public readonly BookC BookC = new();
        public readonly MistakeC MistakeC = new(new float[(byte)ResourceTypes.End]);
        public readonly ZonesInfoC ZoneInfoC = new();
        public readonly WhereTeleportC WhereTeleportC = new();
        public readonly IndexedCellsC CellsC = new();
        public readonly SelectedUnitC SelectedUnitC = new();
        public readonly InputC InputC = new();
        public readonly SelectedBuildingsInTownC SelectedBuildingsC = new();
        public readonly SelectedToolWeaponC SelectedToolWeaponC = new();
        public readonly IndexesByXyC IndexesByXyC;

        internal CommonGameE(in DataFromViewC dataFromViewC, in TestModeTypes testModeT, in DateTime startGame, in List<object> actions, in string name, in byte[,] idxs)
        {
            RpcC = new RpcPoolC(actions, name);
            DataFromViewC = dataFromViewC;
            CommonInfoAboutGameC = new CommonInfoAboutGameC(testModeT, startGame);
            IndexesByXyC = new IndexesByXyC(idxs);
        }

        internal void Dispose()
        {
            ZoneInfoC.Dispose();
            CommonInfoAboutGameC.Dispose();
            MistakeC.Dispose();
        }
    }
}