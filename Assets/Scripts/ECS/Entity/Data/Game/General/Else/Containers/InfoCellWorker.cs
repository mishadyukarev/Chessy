namespace Assets.Scripts.Workers.Game.Else.Cell
{
    internal sealed class InfoCellWorker
    {
        private static EntGameGeneralElseDataManager EGGM => Main.Instance.ECSmanager.EntGameGeneralElseDataManager;

        internal static bool IsStartedCell(bool key, int[] xy) => EGGM.CellsInfoEnt_XyStartCellsCom.XyStartCellsDict[key].TryFindCell(xy);
    }
}
