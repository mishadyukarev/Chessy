using Chessy.Game.Entity;
using Chessy.Game.Values;

namespace Chessy.Game
{
    static class CloudVS
    {
        public static void Run(in EntitiesViewGame eV, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                eV.CellEs(idx_0).CloudCellVC.SetActive(false);
            }

            var centerCloud = e.WeatherE.CloudC.Center;

            foreach (var cellE in e.AroundCellsE(centerCloud).CellsAround)
            {
                eV.CellEs(cellE).CloudCellVC.SetActive(true);
            }

            eV.CellEs(centerCloud).CloudCellVC.SetActive(true);
        }
    }
}