using Chessy.Game.Entity.View.Cell;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct CellVEs
    {
        readonly Dictionary<CellBarTypes, SpriteRendererVC> _bars;
        readonly Dictionary<CellBlockTypes, SpriteRendererVC> _blocks;
        readonly Dictionary<DirectTypes, SpriteRendererVC> _trails;

        public readonly Chessy.Common.Component.GameObjectVC CellParent;
        public readonly Chessy.Common.Component.GameObjectVC CellGO;
        public readonly SpriteRendererVC CellSR;

        public readonly FireVE FireVE;
        public readonly EnvironmentVEs EnvironmentVEs;
        public readonly UnitVEs UnitVEs;
        public readonly CellBuildingVEs BuildingEs;
        public readonly SpriteRendererVC CloudCellVC;
        public readonly SupportCellVE SupportCellEs;

        public SpriteRendererVC Bar(in CellBarTypes bar) => _bars[bar];
        public SpriteRendererVC Block(in CellBlockTypes block) => _blocks[block];

        public SpriteRendererVC TrailCellVC(in DirectTypes dir) => _trails[dir];


        public CellVEs(in GameObject cell, in byte idx)
        {
            CellParent = new Chessy.Common.Component.GameObjectVC(cell);

            var cellUnder = cell.transform.Find("Cell");


            CellGO = new Chessy.Common.Component.GameObjectVC(cellUnder.gameObject);
            CellSR = new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>());


            FireVE = new FireVE(cell);
            SupportCellEs = new SupportCellVE(cell.transform);


            BuildingEs = new CellBuildingVEs(cell);
            EnvironmentVEs = new EnvironmentVEs(cell);
            UnitVEs = new UnitVEs(cell.transform);


            CloudCellVC = new SpriteRendererVC(cell.transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>());


            _bars = new Dictionary<CellBarTypes, SpriteRendererVC>();

            for (var bar = CellBarTypes.Food; bar < CellBarTypes.End; bar++)
            {
                var bars = cell.transform.Find("Bars");
                var name = bar.ToString();
                var sr = bars.Find(name).GetComponent<SpriteRenderer>();

                _bars.Add(bar, new SpriteRendererVC(sr));
            }


            _blocks = new Dictionary<CellBlockTypes, SpriteRendererVC>();

            for (var block = CellBlockTypes.Condition; block < CellBlockTypes.End; block++)
            {
                var blocks = cell.transform.Find("Blocks");
                var name = block.ToString();
                var sr = blocks.Find(name).GetComponent<SpriteRenderer>();

                _blocks.Add(block, new SpriteRendererVC(sr));
            }


            _trails = new Dictionary<DirectTypes, SpriteRendererVC>();

            var parent_Trans = cell.transform.Find("TrailZone");

            for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new SpriteRendererVC(parent_Trans.Find(dir.ToString()).GetComponent<SpriteRenderer>()));
            }
        }
    }
}