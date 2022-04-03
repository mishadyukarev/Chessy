using Chessy.Game.Model.Entity;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public struct SyncRiverVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eV, in EntitiesModelGame e)
        {
            ref var river_0 = ref e.RiverEs(idx_0).RiverTC;

            switch (e.CurPlayerITC.PlayerT)
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
                        CellRiverVEs.River(dir_1, idx_0).SetActive(e.CellEs(idx_0).RiverEs.HaveRiverC.HaveRive(dir_1));
                    }
                }
            }
        }
    }
}