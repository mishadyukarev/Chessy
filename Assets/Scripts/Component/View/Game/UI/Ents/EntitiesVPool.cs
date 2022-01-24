using ECS;
using Game.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct EntitiesVPool
    {
        public EntitiesVPool(in EcsWorld gameW, out List<object> forData)
        {
            #region View

            CanvasC.SetCurZone(SceneTypes.Game);

            new ResourcesSpriteVEs(gameW);
            new VideoClipsResC(true);

            new EntityVPool(gameW, out var actions, out var sounds0, out var sounds1);

            var parCells = new GameObject("Cells");
            EntityVPool.GeneralZone<GeneralZoneVEC>().Attach(parCells.transform);

            byte idx_cur = 0;

            var cells = new GameObject[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
                {
                    var sprite = y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0
                        ? ResourcesSpriteVEs.Sprite(SpriteTypes.WhiteCell).Sprite
                        : ResourcesSpriteVEs.Sprite(SpriteTypes.BlackCell).Sprite;


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

                    cells[idx_cur] = cell;

                    ++idx_cur;
                }


            new CellVEs(gameW, cells);
            new UnitCellVEs(gameW, cells);
            new CellFireVEs(gameW, cells);
            new CellEnvVEs(gameW, cells);
            new CellTrailVEs(gameW, cells);
            new CellCloudVEs(gameW, cells);
            new CellBuildingVEs(gameW, cells);
            new CellRiverVEs(gameW, cells);
            new SupportCellVEs(gameW, cells);
            new CellBlocksVEs(gameW, cells);
            new CellBarsVEs(gameW, cells);
            new StunCellVEs(gameW, cells);
            new CellIceWallVEs(gameW, cells);


            ///Left
            var leftZone = CanvasC.FindUnderCurZone("LeftZone").transform;
            new EntityLeftCityUIPool(gameW, leftZone);
            new EntityLeftEnvUIPool(gameW, leftZone);

            ///Center
            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;
            new EntityCenterUIPool(gameW, centerZone);
            new CenterHerosUIE(gameW, centerZone);
            new CenterFriendUIE(gameW, centerZone);
            new CenterUpgradeUIE(gameW, centerZone);
            new CenterHintUIE(gameW, centerZone);
            new CenterSelectorUIE(gameW, centerZone);
            new CenterKingUIE(gameW, centerZone);
            new MistakeUIE(gameW, centerZone);

            ///Up
            var upZone = CanvasC.FindUnderCurZone("UpZone").transform;
            new EconomyUpUIE(gameW, upZone);
            new UpSunsUIEs(gameW, upZone);

            ///Down
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;
            new DownToolWeaponUIEs(gameW, downZone);
            new UIEntDownDoner(gameW, downZone);
            new UIEntDownUpgrade(gameW, downZone);
            var takeUnitZone = downZone.Find("TakeUnitZone");
            new PawnArcherDownUIE(gameW, takeUnitZone);
            new UIEntDownScout(gameW, takeUnitZone);
            new DownHeroUIE(gameW, takeUnitZone);

            #endregion


            var isActiveParenCells = new bool[CellStartValues.ALL_CELLS_AMOUNT];
            var idCells = new int[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                isActiveParenCells[idx] = CellVEs.CellParent<GameObjectVC>(idx).IsActiveSelf;
                idCells[idx] = CellVEs.Cell<GameObjectVC>(idx).InstanceID;
            }

            forData = new List<object>();
            forData.Add(actions);
            forData.Add(isActiveParenCells);
            forData.Add(idCells);
            forData.Add(sounds0);
            forData.Add(sounds1);
        }
    }
}