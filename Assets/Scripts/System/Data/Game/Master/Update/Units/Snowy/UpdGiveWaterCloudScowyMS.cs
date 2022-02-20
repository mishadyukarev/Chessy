using System.Linq;

namespace Game.Game
{
    sealed class UpdGiveWaterCloudScowyMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdGiveWaterCloudScowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Snowy))
                {
                    if (E.CellEs(E.CenterCloudIdxC.Idx).AroundCellEs.Any(e => e.IdxC.Idx == idx_0))
                    {
                        //Es.UnitE(idx_0).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                    }
                    
                }
            }
        }
    }
}