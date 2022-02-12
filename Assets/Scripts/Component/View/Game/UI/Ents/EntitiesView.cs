using ECS;
using Game.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct EntitiesView
    {
        readonly CellVEs[] _cellVEs;
        public CellVEs CellEs(in byte idx) => _cellVEs[idx];
        public CellBuildingVEs BuildingVEs(in byte idx) => CellEs(idx).BuildingEs;
        public CellBuildingVE BuildingE(in byte idx, in BuildingTypes buildT) => BuildingVEs(idx).Main(buildT);
        public CellUnitVEs UnitEs(in byte idx) => CellEs(idx).UnitVEs;
        public CellUnitVE UnitE(in byte idx, in bool isSelected, in LevelTypes levT, in UnitTypes unitT) => UnitEs(idx).UnitE(isSelected, levT, unitT);
        public CellUnitEffectVEs UnitEffectVEs(in byte idx) => UnitEs(idx).EffectVEs;
        public CellEnvironmentVEs EnvironmentVEs(in byte idx) => CellEs(idx).EnvironmentVEs;
        public CellEnvironmentVE EnvironmentVE(in byte idx, in EnvironmentTypes envT) => EnvironmentVEs(idx).EnvironmentE(envT);

        public EntitiesView(in EcsWorld gameW, out List<object> forData)
        {
            new VideoClipsResC(true);



            CanvasC.SetCurZone(SceneTypes.Game);


            new EntityVPool(gameW, out var actions, out var sounds0, out var sounds1);

            var parCells = new GameObject("Cells");
            EntityVPool.GeneralZone<GeneralZoneVEC>().Attach(parCells.transform);

            byte idx_cur = 0;

            var cells = new GameObject[CellStartValues.ALL_CELLS_AMOUNT];


            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
                {

                    //    if(y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0)
                    //{
                    //    cells[idx_cur].transform.Find("Black").gameObject.SetActive(false);
                    //}
                    //else
                    //{
                    //    cells[idx_cur].transform.Find("White").gameObject.SetActive(false);
                    //}

                        //? 
                        //: ResourceSpriteEs.Sprite(false).SpriteC.Sprite;


                    var cell = GameObject.Instantiate(PrefabResC.CellGO, MainGoVC.Pos + new Vector3(x, y, MainGoVC.Pos.z), MainGoVC.Rot);
                    cell.name = "CellMain";
                    //cell.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                            y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                            y == 1 && x == 3 || y == 1 && x == 12 ||
                            y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);

                    cells[idx_cur] = cell;

                    ++idx_cur;
                }



            _cellVEs = new CellVEs[cells.Length];
            for (byte idx_0 = 0; idx_0 < _cellVEs.Length; idx_0++)
            {
                _cellVEs[idx_0] = new CellVEs(cells[idx_0], idx_0, gameW);
            }

            new CellTrailVEs(gameW, cells);
            new CellCloudVEs(gameW, cells);
            new CellRiverVEs(gameW, cells);
            new SupportCellVEs(gameW, cells);
            new CellBlocksVEs(gameW, cells);
            new CellBarsVEs(gameW, cells);


            var isActiveParenCells = new bool[CellStartValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveParenCells[idx] = CellEs(idx).CellParent.IsActiveSelf;
                idCells[idx] = CellEs(idx).CellGO.InstanceID;
            }

            forData = new List<object>();
            forData.Add(actions);
            forData.Add(sounds0);
            forData.Add(sounds1);
            forData.Add(isActiveParenCells);
            forData.Add(idCells);
        }
    }
}