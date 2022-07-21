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
        public readonly MistakeC MistakeC = new();
        public readonly ZonesInfoC ZoneInfoC = new();
        public readonly WhereTeleportC WhereTeleportC = new();
        public readonly CellsC CellsC = new();
        public readonly SelectedUnitC SelectedUnitC = new();
        public readonly InputC InputC = new();
        public readonly SelectedBuildingsInTownC SelectedBuildingsC = new();
        public readonly SelectedToolWeaponC SelectedToolWeaponC = new();

        internal CommonGameE(in DataFromViewC dataFromViewC, in TestModeTypes testModeT, in DateTime startGame, in List<object> actions, in string name)
        {
            RpcC = new RpcPoolC(actions, name);
            DataFromViewC = dataFromViewC;
            CommonInfoAboutGameC = new CommonInfoAboutGameC(testModeT, startGame);
        }

        internal void Dispose()
        {
            ZoneInfoC.Dispose();
            CommonInfoAboutGameC.Dispose();
            MistakeC.Dispose();
        }
    }
}