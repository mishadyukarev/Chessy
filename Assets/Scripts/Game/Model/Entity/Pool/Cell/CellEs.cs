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

        public readonly CellE CellE;
        public readonly AroundCellsE AroundCellsEs;
        public readonly UnitEs UnitEs = new UnitEs();
        public readonly CellBuildingEs BuildEs = new CellBuildingEs();
        public readonly CellEnvironmentEs EnvironmentEs = new CellEnvironmentEs();
        public readonly CellEffectE EffectEs = new CellEffectE();
        public readonly CellRiverE RiverEs = new CellRiverE();

        public ref HealthC TrailHealthC(in DirectTypes dir) => ref _trailHealthCs[(byte)dir - 1];
        public ref CellPlayerPoolEs Player(in PlayerTypes player) => ref _forPlayerEs[(byte)player];


        public UnitMainE UnitMainE => UnitEs.MainE;
        public ref UnitTC UnitTC => ref UnitMainE.UnitTC;
        public ref PlayerTC UnitPlayerTC => ref UnitMainE.PlayerTC;

        public StatsE UnitStatsE => UnitEs.StatsE;

        public MainToolWeaponE UnitMainTWE => UnitEs.MainToolWeaponE;
        public ExtraToolWeaponE UnitExtraTWE => UnitEs.ExtraToolWeaponE;


        internal CellEs(in bool[] isActiveParents, in int idCell, byte[] xy, in byte idx, in EntitiesModelGame eMGame)
        {
            IsActiveParentSelf = isActiveParents[idx];

            AroundCellsEs = new AroundCellsE(idx, isActiveParents, xy, eMGame);

            CellE = new CellE(idx, xy, idCell);
        }
    }
}