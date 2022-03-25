using System.Linq;

namespace Chessy.Game
{
    sealed class UpdGiveWaterCloudScowyMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal UpdGiveWaterCloudScowyMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Snowy))
                {
                    if (eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellEs.Any(e => e.IdxC.Idx == cell_0))
                    {
                        //Es.UnitE(cell_0).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                    }
                    
                }
            }
        }
    }
}