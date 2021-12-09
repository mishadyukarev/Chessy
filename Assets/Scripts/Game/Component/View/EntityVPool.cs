using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct EntityVPool
    {
        readonly static Dictionary<CellEntTypes, EcsEntity[]> _cells;

        public static ref T Cell<T>(in byte idx) where T : struct, ICellVE => ref _cells[CellEntTypes.Cell][idx].Get<T>();
        public static ref T UnitCellVC<T>(in byte idx) where T : struct, IUnitCellV => ref _cells[CellEntTypes.Unit][idx].Get<T>();
        public static ref T BuildCellVC<T>(in byte idx) where T : struct, IBuildCellV => ref _cells[CellEntTypes.Build][idx].Get<T>();
        public static ref T EnvCellVC<T>(in byte idx) where T : struct, IEnvCellV => ref _cells[CellEntTypes.Env][idx].Get<T>();
        public static ref T TrailCellVC<T>(in byte idx) where T : struct, ITrailCellV => ref _cells[CellEntTypes.Trail][idx].Get<T>();
        public static ref T FireCellVC<T>(in byte idx) where T : struct, IFireCellV => ref _cells[CellEntTypes.Fire][idx].Get<T>();
        public static ref T CloudCellVC<T>(in byte idx) where T : struct, ICloudCellV => ref _cells[CellEntTypes.Cloud][idx].Get<T>();
        public static ref T RiverCellVC<T>(in byte idx) where T : struct, IRiverCellV => ref _cells[CellEntTypes.River][idx].Get<T>();
        public static ref T ElseCellVC<T>(in byte idx) where T : struct, IElseCellVE => ref _cells[CellEntTypes.Else][idx].Get<T>();


        static EntityVPool()
        {
            _cells = new Dictionary<CellEntTypes, EcsEntity[]>();

            for (var type = CellEntTypes.First; type < CellEntTypes.End; type++)
            {
                _cells.Add(type, new EcsEntity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public EntityVPool(in EcsWorld curGameW)
        {
            var parCells = new GameObject("Cells");
            GenerZoneVC.Attach(parCells.transform);

            byte idx_0 = 0;

            var cells = new GameObject[_cells[CellEntTypes.First].Length];

            for (byte x = 0; x < CellValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellValues.Y_AMOUNT; y++)
                {
                    var sprite = y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0
                        ? SpritesResC.Sprite(SpriteTypes.WhiteCell)
                        : SpritesResC.Sprite(SpriteTypes.BlackCell);


                    var cell = GameObject.Instantiate(PrefabResC.CellGO, MainGoVC.Pos + new Vector3(x, y, MainGoVC.Pos.z), MainGoVC.Rot);
                    cell.name = "CellMain";
                    cell.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                            y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                            y == 1 && x == 3 || y == 1 && x == 12 ||
                            y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);

                    cells[idx_0] = cell;

                    ++idx_0;
                }


            for (byte idx = 0; idx < _cells[CellEntTypes.First].Length; idx++)
            {
                _cells[CellEntTypes.Cell][idx] = curGameW.NewEntity()
                        .Replace(new CellVC(cells[idx]));


                _cells[CellEntTypes.Unit][idx] = curGameW.NewEntity()
                     .Replace(new UnitMainVC(cells[idx]))
                     .Replace(new UnitExtraVC(cells[idx]));


                var build_GO = cells[idx].transform.Find("Building").gameObject;

                _cells[CellEntTypes.Build][idx] = curGameW.NewEntity()
                     .Replace(new BuildVC(build_GO))
                     .Replace(new BuildBackVC(build_GO));


                _cells[CellEntTypes.Env][idx] = curGameW.NewEntity()
                    .Replace(new EnvVC(cells[idx]));


                _cells[CellEntTypes.Trail][idx] = curGameW.NewEntity()
                    .Replace(new TrailVC(cells[idx].transform));


                _cells[CellEntTypes.Fire][idx] = curGameW.NewEntity()
                    .Replace(new FireVC(cells[idx]));


                _cells[CellEntTypes.Cloud][idx] = curGameW.NewEntity()
                    .Replace(new CloudVC(cells[idx]));


                _cells[CellEntTypes.River][idx] = curGameW.NewEntity()
                    .Replace(new RiverVC(cells[idx].transform));


                _cells[CellEntTypes.Else][idx] = curGameW.NewEntity()
                    .Replace(new SupportVC(cells[idx]))
                    .Replace(new BlocksVC(cells[idx]))
                    .Replace(new BarsVC(cells[idx]))
                    .Replace(new StunVC(cells[idx].transform));
            }
        }
    }
}