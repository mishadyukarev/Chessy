using System;
using UnityEngine;

namespace Game.Game
{
    sealed class RiverCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public RiverCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellWorker.Idxs)
            {
                ref var river_0 = ref RiverEs(idx_0).RiverE.RiverTC;

                switch (Es.WhoseMove.CurPlayerI)
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
                    foreach (var dir_1 in CellEs(idx_0).RiverEs.Keys)
                    {
                        if (dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Left)
                        {
                            CellRiverVEs.River(dir_1, idx_0).SetActive(CellEs(idx_0).RiverEs.HaveRive(dir_1).HaveRiver.Have);
                        }
                    }
                }
            }



        }
    }
}