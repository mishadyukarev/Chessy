﻿using Chessy.Model.Component;
namespace Chessy.Model.Entity
{
    public struct CellEs
    {
        public readonly CellE CellE;
        public AroundCellsE AroundCellsEs;
        public UnitE UnitE;
        public BuildingE BuildingE;
        public EnvironmentE EnvironmentE;
        public EffectE EffectE;
        public RiverE RiverE;
        public TrailE TrailE;

        internal CellEs(in DataFromViewC dataFromViewC, in int idCell, in byte idx, in EntitiesModel eMGame, params byte[] xy) : this()
        {
            CellE = new CellE(dataFromViewC, idx, idCell, xy);
            AroundCellsEs = new AroundCellsE(idx, dataFromViewC, eMGame, xy);
            UnitE = new UnitE(default);
            BuildingE = new BuildingE(default);
            RiverE = new RiverE(new bool[(byte)DirectTypes.End]);
            TrailE = new TrailE(default);
        }

        internal void Dispose()
        {
            EnvironmentE.Dispose();
            TrailE.Dispose();
            UnitE.Dispose();
            BuildingE.Dispose();
        }
    }
}