using Chessy.Game.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class CellEs
    {
        readonly CellPlayerPoolEs[] _forPlayerEs = new CellPlayerPoolEs[(byte)PlayerTypes.End];
        readonly HealthC[] _trailHealthCs = new HealthC[(byte)DirectTypes.End - 1];

        public readonly bool IsActiveParentSelf;

        public CellE CellE;
        public AroundCellsE AroundCellsEs;
        public UnitEs UnitEs;
        public CellBuildingEs BuildEs;
        public CellEnvironmentEs EnvironmentEs;
        public CellEffectE EffectEs;
        public CellRiverE RiverEs;

        public ref HealthC TrailHealthC(in DirectTypes dir) => ref _trailHealthCs[(byte)dir - 1];
        public ref CellPlayerPoolEs Player(in PlayerTypes player) => ref _forPlayerEs[(byte)player];


        public ref UnitMainE UnitMainE => ref UnitEs.MainE;
        public ref UnitTC UnitTC => ref UnitMainE.UnitTC;
        public ref PlayerTC UnitPlayerTC => ref UnitMainE.PlayerTC;

        public ref StatsE UnitStatsE => ref UnitEs.StatsE;

        public ref MainToolWeaponE UnitMainTWE => ref UnitEs.MainToolWeaponE;
        public ref ExtraToolWeaponE UnitExtraTWE => ref UnitEs.ExtraToolWeaponE;


        internal CellEs(in bool[] isActiveParents, in int idCell, byte[] xy, in byte idx, in EntitiesModelGame eMGame)
        {
            IsActiveParentSelf = isActiveParents[idx];

            AroundCellsEs = new AroundCellsE(idx, isActiveParents, xy, eMGame);

            CellE = new CellE(idx, xy, idCell);

            UnitEs = new UnitEs(default);

            BuildEs = new CellBuildingEs(default);
            RiverEs = new CellRiverE(new bool[(byte)DirectTypes.End - 1]);
        }
    }
}