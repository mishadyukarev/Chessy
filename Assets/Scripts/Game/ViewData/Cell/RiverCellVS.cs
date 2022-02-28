using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class RiverCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal RiverCellVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                ref var river_0 = ref E.RiverEs(idx_0).RiverTC;

                switch (E.CurPlayerITC.Player)
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
                            CellRiverVEs.River(dir_1, idx_0).SetActive(E.CellEs(idx_0).RiverEs.HaveRive(dir_1));
                        }
                    }
                }
            }
        }
    }
}