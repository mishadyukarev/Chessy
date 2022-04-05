using Chessy.Game.Model.Entity;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public struct SyncRiverVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eV, in EntitiesModelGame e)
        {
            switch (e.CurPlayerIT)
            {
                case PlayerTypes.None: throw new Exception();
                case PlayerTypes.First:
                    CellRiverVEs.Parent(idx_0).LocalEulerAngles = new Vector3(0, 0, 0);
                    break;

                case PlayerTypes.Second:
                    CellRiverVEs.Parent(idx_0).LocalEulerAngles = new Vector3(0, 0, 180);
                    break;

                default: throw new Exception();
            }


            if (e.RiverT(idx_0) == RiverTypes.Start)
            {
                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                {
                    if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                    {
                        CellRiverVEs.River(dir_1, idx_0).SetActive(e.HaveRiverC(idx_0).HaveRive(dir_1));
                    }
                }
            }
        }
    }
}