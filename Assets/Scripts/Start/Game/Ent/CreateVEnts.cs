using Game.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class CreateVEnts : IEcsInitSystem
    {
        private EcsWorld _curGameW = default;

        public void Init()
        {
            ///Cells
            ///
            var parCells = new GameObject("Cells");
            GenerZoneVC.Attach(parCells.transform);

            byte idx_0 = 0;

            for (byte x = 0; x < CellValuesC.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValuesC.CELL_COUNT_Y; y++)
                {
                    var sprite = y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0
                        ? SpritesResC.Sprite(SpriteTypes.WhiteCell)
                        : SpritesResC.Sprite(SpriteTypes.BlackCell);


                    var cell = GameObject.Instantiate(PrefabResC.CellGO, MainGoVC.Pos + new Vector3(x, y, MainGoVC.Pos.z), MainGoVC.Rot);
                    cell.name = "Cell";
                    cell.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                            y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                            y == 1 && x == 3 || y == 1 && x == 12 ||
                            y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);


                    var cellView_GO = cell.transform.Find("Cell").gameObject;

                    _curGameW.NewEntity()
                        .Replace(new CellVC(cellView_GO))
                        .Replace(new EnvVC(cell))
                        .Replace(new FireVC(cell))
                        .Replace(new SupportVC(cell))
                        .Replace(new CloudVC(cell))
                        .Replace(new RiverVC(cell.transform));


                    _curGameW.NewEntity()
                         .Replace(new BuildVC(cell));


                    _curGameW.NewEntity()
                         .Replace(new UnitMainVC(cell))
                         .Replace(new UnitExtraVC(cell))
                         .Replace(new BlocksVC(cell))
                         .Replace(new BarsVC(cell))
                         .Replace(new StunVC(cell.transform));


                    _curGameW.NewEntity()
                        .Replace(new TrailVC(cell.transform));




                    ++idx_0;
                }
        }
    }
}
