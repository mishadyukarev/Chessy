using System.Linq;

namespace Game.Game
{
    sealed class UpdAttackFromWaterHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdAttackFromWaterHellMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Hell))
                {
                    if (E.RiverEs(idx_0).RiverTC.HaveRiverNear)
                    {
                        //Es.UnitE(idx_0).Take(Es, 0.15f);
                    }

                    if (E.CellEs(E.CenterCloudIdxC.Idx).AroundCellEs.Any(e => e.IdxC.Idx == idx_0))
                    {
                        //Es.UnitE(idx_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                    {
                        if (E.BuildTC(cellE.IdxC.Idx).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(idx_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }
        }
    }
}