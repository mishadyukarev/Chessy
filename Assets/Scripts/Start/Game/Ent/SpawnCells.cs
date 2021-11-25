using Game.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class SpawnCells : IEcsInitSystem
    {
        private EcsWorld _curGameW = default;

        public void Init()
        {
            ///Cells
            ///
            var parCells = new GameObject("Cells");
            GenerZoneVC.Attach(parCells.transform);

            byte idx_0 = 0;

            var cells = new GameObject[CellValuesC.AMOUNT_ALL_CELLS];

            for (byte x = 0; x < CellValuesC.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValuesC.CELL_COUNT_Y; y++)
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


            new EntityViewPool(_curGameW, cells);
        }
    }
}
