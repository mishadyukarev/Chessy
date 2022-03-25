using System.Linq;

namespace Chessy.Game
{
    sealed class UpdAttackFromWaterHellMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal UpdAttackFromWaterHellMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    if (eMGame.RiverEs(cell_0).RiverTC.HaveRiverNear)
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                    }

                    if (eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                    {
                        //Es.UnitE(cell_0).Take(Es, 0.15f);
                        break;
                    }

                    foreach (var cellE in eMGame.CellEs(cell_0).AroundCellEs)
                    {
                        if (eMGame.BuildingTC(cellE.IdxC.Idx).Is(BuildingTypes.IceWall))
                        {
                            //Es.UnitE(cell_0).Take(Es, 0.15f);
                            break;
                        }
                    }
                }
            }
        }
    }
}