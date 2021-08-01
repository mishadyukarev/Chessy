using Assets.Scripts.ECS.Entities.Game.General.Base.View.Containers.Cell;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal sealed class CellFireVisWorker
    {
        private static CellFireViewContainerEnts _cellFireViewContainerEnts;

        internal CellFireVisWorker(CellFireViewContainerEnts cellFireViewContainerEnts)
        {
            _cellFireViewContainerEnts = cellFireViewContainerEnts;
        }


        internal static SpriteRenderer GetSR(int[] xy) => _cellFireViewContainerEnts.CellFireEnt_SprRendCom(xy).SpriteRenderer;

        private static void ActiveSR(bool isEnabled, int[] xy) => GetSR(xy).enabled = isEnabled;
        internal static void EnableSR(int[] xy) => ActiveSR(true, xy);
        internal static void DisableSR(int[] xy) => ActiveSR(false, xy);
    }
}
