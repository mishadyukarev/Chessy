using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static.Cell;

namespace Assets.Scripts
{
    public class CellWorker : MainWorker
    {
        internal static int InstanceIDGO(int[] xy) => EGGM.CellEnt_CellBaseCom(xy).InstanceIDGO;
        internal static bool IsActiveSelfGO(int[] xy) => EGGM.CellEnt_CellBaseCom(xy).IsActiveSelfGO;
        internal static bool IsStartedCell(bool key, int[] xy) => EGGM.CellEnt_CellBaseCom(xy).IsStartedCell(key);

        internal static float GetEulerAngle(XyzTypes xyzType, int[] xy) => EGGM.CellEnt_CellBaseCom(xy).GetEulerAngle(xyzType);
    }
}