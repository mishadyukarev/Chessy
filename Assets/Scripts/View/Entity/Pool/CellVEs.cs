using Chessy.Model;
using Chessy.Model.Component;
using Chessy.View.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chessy.View.Entity
{
    public readonly struct CellVEs
    {
        readonly Dictionary<CellBarTypes, SpriteRendererVC> _bars;
        readonly Dictionary<DirectTypes, SpriteRendererVC> _trails;

        internal readonly GameObjectVC CellParentGOC;
        internal readonly GameObjectVC StandartCellGO;
        internal readonly GameObjectVC DesertCell1GOC;
        internal readonly GameObjectVC DesertCell2GOC;
        internal readonly BoxCollider2D BoxCollider2D;
        internal readonly SpriteRendererVC StandartCellSRC;
        internal readonly TMPC IdxAndXyInfoTMPC;
        internal readonly SpriteRendererVC RedCircularSRC;

        internal readonly FireVE FireVE;
        internal readonly EnvironmentVE EnvironmentVEs;
        internal readonly UnitVEs UnitEs;
        internal readonly CellBuildingVE BuildingEs;



        internal readonly SpriteRendererVC CloudSRC;
        internal readonly SpriteRendererVC SunSideSRC;

        internal readonly SupportCellVE SupportCellEs;
        internal readonly RiverVE RiverE;

        public SpriteRendererVC Bar(in CellBarTypes bar) => _bars[bar];
        public SpriteRendererVC TrailCellVC(in DirectTypes dir) => _trails[dir];


        public CellVEs(in GameObject cell)
        {
            var cellT = cell.transform;

            CellParentGOC = new GameObjectVC(cell);

            var cellUnder = cellT.Find("StandartCell_SR+");


            StandartCellGO = new GameObjectVC(cellUnder.gameObject);
            DesertCell1GOC = new GameObjectVC(cellT.Find("DesertCell1_SR+").gameObject);
            DesertCell2GOC = new GameObjectVC(cellT.Find("DesertCell2_SR+").gameObject);

            BoxCollider2D = cellT.Find("Cell_BoxCollider2D+").GetComponent<BoxCollider2D>();

            StandartCellSRC = new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>());
            IdxAndXyInfoTMPC = new TMPC(cell.transform.Find("IdxAndXyInfo_TMP+").GetComponent<TextMeshPro>());


            RedCircularSRC = new SpriteRendererVC(cellT.Find("RedCircular_SR+").GetComponent<SpriteRenderer>());

            FireVE = new FireVE(cell);
            SupportCellEs = new SupportCellVE(cell.transform);


            BuildingEs = new CellBuildingVE(cell);
            EnvironmentVEs = new EnvironmentVE(cell);
            UnitEs = new UnitVEs(cell.transform);

            var weatherT = cellT.Find("Weather+");
            CloudSRC = new SpriteRendererVC(weatherT.Find("Cloud_SR+").GetComponent<SpriteRenderer>());
            SunSideSRC = new SpriteRendererVC(weatherT.Find("SunSide_SR+").GetComponent<SpriteRenderer>());


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