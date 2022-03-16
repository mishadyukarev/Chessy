using System;
using UnityEngine;

namespace Chessy.Game
{
    static class SyncRiverVS
    {
        public static void Sync(in byte idx_0, in EntitiesView eV, in EntitiesModel e)
        {
            ref var river_0 = ref e.RiverEs(idx_0).RiverTC;

            switch (e.CurPlayerITC.Player)
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


            if (river_0.River == RiverTypes.Start)
            {
                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                {
                    if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                    {
                        CellRiverVEs.River(dir_1, idx_0).SetActive(e.CellEs(idx_0).RiverEs.HaveRive(dir_1));
                    }
                }
            }
        }
    }
}