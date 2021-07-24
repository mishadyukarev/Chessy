using Assets.Scripts.Workers.Cell;

namespace Assets.Scripts.Workers.Game.Else.Cell
{
    internal sealed class InfoCellWorker : MainGeneralWorker
    {
        internal static bool IsStartedCell(bool key, int[] xy) => EGGM.CellsInfoEnt_XyStartCellsCom.XyStartCellsDict[key].TryFindCell(xy);
    }
}
