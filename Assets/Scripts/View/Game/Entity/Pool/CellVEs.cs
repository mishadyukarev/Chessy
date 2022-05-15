using Chessy.Common.Component;
using Chessy.Game.Entity.View.Cell;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chessy.Game
{
    public struct CellVEs
    {
        readonly Dictionary<CellBarTypes, SpriteRendererVC> _bars;

        readonly Dictionary<DirectTypes, SpriteRendererVC> _trails;

        public readonly GameObjectVC CellParent;
        public readonly GameObjectVC CellGO;
        public readonly SpriteRendererVC CellSR;
        public readonly TMPC IdxAndXyInfoTMPC;

        public readonly FireVE FireVE;
        public readonly EnvironmentVEs EnvironmentVEs;
        public readonly UnitVEs UnitEs;
        public readonly CellBuildingVEs BuildingEs;
        public readonly SpriteRendererVC CloudCellSRC;
        public readonly SupportCellVE SupportCellEs;
        public readonly RiverVE RiverE;

        public SpriteRendererVC Bar(in CellBarTypes bar) => _bars[bar];
        public SpriteRendererVC TrailCellVC(in DirectTypes dir) => _trails[dir];


        public CellVEs(in GameObject cell)
        {
            CellParent = new GameObjectVC(cell);

            var cellUnder = cell.transform.Find("Cell");


            CellGO = new GameObjectVC(cellUnder.gameObject);
            CellSR = new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>());
            IdxAndXyInfoTMPC = new TMPC(cell.transform.Find("IdxAndXyInfo_TMP+").GetComponent<TextMeshPro>());


            FireVE = new FireVE(cell);
            SupportCellEs = new SupportCellVE(cell.transform);


            BuildingEs = new CellBuildingVEs(cell);
            EnvironmentVEs = new EnvironmentVEs(cell);
            UnitEs = new UnitVEs(cell.transform);


            CloudCellSRC = new SpriteRendererVC(cell.transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>());


            _bars = new Dictionary<CellBarTypes, SpriteRendererVC>();

            for (var bar = CellBarTypes.Food; bar < CellBarTypes.End; bar++)
            {
                var bars = cell.transform.Find("Bars");
                var name = bar.ToString();
                var sr = bars.Find(name).GetComponent<SpriteRenderer>();

                _bars.Add(bar, new SpriteRendererVC(sr));
            }





            _trails = new Dictionary<DirectTypes, SpriteRendererVC>();

            var parent = cell.transform.Find("TrailZone");

            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                _trails.Add(dirT, new SpriteRendererVC(parent.Find(dirT.ToString()).GetComponent<SpriteRenderer>()));
            }

            RiverE = new RiverVE(cell.transform);
        }
    }
}