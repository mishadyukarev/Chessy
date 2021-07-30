using Assets.Scripts.Workers.Cell;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal sealed class CellFireInfoWorker : MainGeneralWorker
    {
        private static List<int[]> XyAmountCellFireInGame => EGGM.CellFireInfoEnt_XyCellFireInfoCom.XyAmountCellFireInGameList;
        internal static int AmountFireInGame => XyAmountCellFireInGame.Count;

        internal static bool IsFireCell(int[] xy) => XyAmountCellFireInGame.TryFindCell(xy);

        internal static void AddFire(int[] xy) => XyAmountCellFireInGame.Add(xy);
        internal static void RemoveFire(int[] xy) => XyAmountCellFireInGame.Remove(xy);
    }
}
