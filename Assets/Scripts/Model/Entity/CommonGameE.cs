﻿using Chessy.Model.Component;
namespace Chessy.Model.Entity
{
    public struct CommonGameE
    {
        public readonly DataFromViewC DataFromViewC;
        public readonly Resources Resources;
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

        internal CommonGameE(in DataFromViewC dataFromViewC) : this()
        {
            DataFromViewC = dataFromViewC;
            Resources = new Resources(default);
        }
    }
}