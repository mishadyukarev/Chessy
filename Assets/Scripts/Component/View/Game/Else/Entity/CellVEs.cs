using ECS;
using Game.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellVEs
    {
        static Dictionary<CellEntTypes, Entity[]> _cells;


        public static ref C Cell<C>(in byte idx) where C : struct, ICellVE => ref _cells[CellEntTypes.Cell][idx].Get<C>();
        public static ref C BuildCellVC<C>(in byte idx) where C : struct, IBuildCellV => ref _cells[CellEntTypes.Build][idx].Get<C>();
        public static ref T RiverCellVC<T>(in byte idx) where T : struct, IRiverCellV => ref _cells[CellEntTypes.River][idx].Get<T>();
        public static ref T ElseCellVE<T>(in byte idx) where T : struct, IElseCellVE => ref _cells[CellEntTypes.Else][idx].Get<T>();


        public CellVEs(in EcsWorld curGameW, in byte xAmount, in byte yAmount , out GameObject[] cells)
        {
            _cells = new Dictionary<CellEntTypes, Entity[]>();
            for (var type = CellEntTypes.First; type < CellEntTypes.End; type++)
            {
                _cells.Add(type, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }


            var parCells = new GameObject("Cells");
            EntityVPool.GeneralZone<GeneralZoneVEC>().Attach(parCells.transform);

            byte idx_0 = 0;

            var amountCells = xAmount * yAmount;

            for (var type = CellEntTypes.First; type < CellEntTypes.End; type++)
            {
                _cells[type] = new Entity[amountCells];
            }
            cells = new GameObject[amountCells];

            for (byte x = 0; x < xAmount; x++)
                for (byte y = 0; y < yAmount; y++)
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


            for (byte idx = 0; idx < amountCells; idx++)
            {
                var cellUnder = cells[idx].transform.Find("Cell");

                _cells[CellEntTypes.Cell][idx] = curGameW.NewEntity()
                      .Add(new GameObjectVC(cellUnder.gameObject))
                      .Add(new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>()));


                var build_GO = cells[idx].transform.Find("Building").gameObject;

                _cells[CellEntTypes.Build][idx] = curGameW.NewEntity()
                     .Add(new BuildVC(build_GO))
                     .Add(new BuildBackVC(build_GO));


                _cells[CellEntTypes.River][idx] = curGameW.NewEntity()
                    .Add(new RiverVC(cells[idx].transform));


                _cells[CellEntTypes.Else][idx] = curGameW.NewEntity()
                    .Add(new SupportVC(cells[idx]))
                    .Add(new BlocksVC(cells[idx]))
                    .Add(new BarsVC(cells[idx]))
                    .Add(new StunVC(cells[idx].transform));
            }
        }
    }
}