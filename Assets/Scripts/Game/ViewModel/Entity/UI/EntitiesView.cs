using Chessy.Common;
using Chessy.Game.Entity.View.Cell;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class EntitiesView
    {
        readonly CellVEs[] _cellVEs;
        public CellVEs CellEs(in byte idx) => _cellVEs[idx];
        public CellBuildingVEs BuildingVEs(in byte idx) => CellEs(idx).BuildingEs;
        public SpriteRendererVC BuildingE(in byte idx, in BuildingTypes buildT) => BuildingVEs(idx).Main(buildT);
        public CellUnitVEs UnitEs(in byte idx) => CellEs(idx).UnitVEs;
        public CellUnitVE UnitE(in byte idx, in bool isSelected, in LevelTypes levT, in UnitTypes unitT) => UnitEs(idx).UnitE(isSelected, levT, unitT);
        public CellUnitEffectVEs UnitEffectVEs(in byte idx) => UnitEs(idx).EffectVEs;
        public EnvironmentVEs EnvironmentVEs(in byte idx) => CellEs(idx).EnvironmentVEs;
        public EnvironmentVE EnvironmentVE(in byte idx, in EnvironmentTypes envT) => EnvironmentVEs(idx).EnvironmentE(envT);


        public EntityVPool EntityVPool;

        public CameraVC CameraVC;



        public EntitiesView(out List<object> forData)
        {
            new VideoClipsResC(true);



            CanvasC.SetCurZone(SceneTypes.Game);


            EntityVPool = new EntityVPool(out var actions, out var sounds0, out var sounds1);

            var parCells = new GameObject("Cells");
            parCells.transform.SetParent(EntityVPool.GenegalZone.Transform);

            byte idx_cur = 0;

            var cells = new GameObject[StartValues.CELLS];


            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
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
                _cellVEs[idx_0] = new CellVEs(cells[idx_0], idx_0);
            }

            new CellRiverVEs(cells);
            new SupportCellVEs(cells);



            CameraVC.Camera = Camera.main;


            var isActiveParenCells = new bool[StartValues.CELLS];
            var idCells = new int[StartValues.CELLS];

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
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