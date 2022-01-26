using System;
using UnityEngine;
using static Game.Game.CellRiverEs;

namespace Game.Game
{
    struct RiverCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                ref var river_0 = ref River(idx_0).RiverTC;

                switch (Entities.WhoseMoveE.CurPlayerI)
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
                    foreach (var dir_1 in CellRiverEs.Keys)
                    {
                        if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                        {
                            CellRiverVEs.River(dir_1, idx_0).SetActive(HaveRive(dir_1, idx_0).HaveRiver.Have);
                        }
                    }
                }
            }



        }
    }
}