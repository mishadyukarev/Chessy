using Chessy.Model.Component;
using System;

namespace Chessy.Model.Entity
{
    public sealed class CommonGameE
    {
        public readonly DataFromViewC DataFromViewC;
        public readonly FromResourcesC Resources;
        public ShopC ShopC;
        public AdC AdC;
        public UpdateAllViewC UpdateAllViewC;
        public SettingsC SettingsC;
        public BookC BookC;
        public CommonInfoAboutGameC CommonInfoAboutGameC;
        public MistakeC MistakeC;
        public ZonesInfoC ZoneInfoC;
        public WhereTeleportC WhereTeleportC;
        public MotionC MotionC;
        public CellsC CellsC;
        public SelectedUnitC SelectedUnitC;
        public InputC InputC;
        internal RpcPoolC RpcC;

        internal CommonGameE(in DataFromViewC dataFromViewC, in TestModeTypes testModeT, in DateTime startGame)
        {
            DataFromViewC = dataFromViewC;
            Resources = new FromResourcesC(default);
            CommonInfoAboutGameC = new CommonInfoAboutGameC(testModeT, startGame);
        }
    }
}