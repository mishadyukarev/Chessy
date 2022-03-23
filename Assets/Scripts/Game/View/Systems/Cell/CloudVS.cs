using Chessy.Game.Entity;

namespace Chessy.Game
{
    static class CloudVS
    {
        public static void Run(in EntitiesViewGame eV, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            for (byte idx_0 = 0; idx_0 < e.LengthCells; idx_0++)
            {
                eV.CellEs(idx_0).CloudCellVC.SetActive(false);
            }

            var centerCloud = e.WeatherE.CloudC.Center;

            foreach (var cellE in e.CellEs(centerCloud).AroundCellEs)
            {
                eV.CellEs(cellE.IdxC.Idx).CloudCellVC.SetActive(true);
            }

            eV.CellEs(centerCloud).CloudCellVC.SetActive(true);
        }
    }
}