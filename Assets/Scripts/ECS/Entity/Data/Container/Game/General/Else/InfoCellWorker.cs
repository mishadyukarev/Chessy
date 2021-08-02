namespace Assets.Scripts.Workers.Game.Else.Cell
{
    internal sealed class InfoCellWorker
    {
        private static EntDataGameGeneralElseManager EGGM => Main.Instance.ECSmanager.EntDataGameGeneralElseManager;

        internal static bool IsStartedCell(bool key, int[] xy) => EGGM.CellsInfoEnt_XyStartCellsCom.XyStartCellsDict[key].TryFindCell(xy);
    }
}
