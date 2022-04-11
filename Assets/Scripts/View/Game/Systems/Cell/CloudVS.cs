using Chessy.Game.Entity;
using Chessy.Game.Values;

namespace Chessy.Game
{
    static class CloudVS
    {
        static bool[] _needActive = new bool[StartValues.CELLS];

        public static void Run(in EntitiesViewGame eV, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            for (byte cellStart = 0; cellStart < StartValues.CELLS; cellStart++)
            {
                _needActive[cellStart] = false;
            }

            var centerCloud = e.WeatherE.CloudC.Center;
            _needActive[centerCloud] = true;

            foreach (var cellStart in e.AroundCellsE(centerCloud).CellsAround)
            {
                _needActive[cellStart] = true;
            }



            for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
            {
                eV.CellEs(cell_start).CloudCellVC.GO.SetActive(_needActive[cell_start]);
            }
        }
    }
}