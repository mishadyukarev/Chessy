using Assets.Scripts.ECS.Game.General.Entities.Containers;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal sealed class CellFireVisWorker
    {
        private static CellFireEntsContainer _cellFireEntsContainer;

        internal CellFireVisWorker(CellFireEntsContainer cellFireEntsContainer)
        {
            _cellFireEntsContainer = cellFireEntsContainer;
        }


        internal static SpriteRenderer GetSR(int[] xy) => _cellFireEntsContainer.CellFireEnt_SprRendCom(xy).SpriteRenderer;

        private static void ActiveSR(bool isEnabled, int[] xy) => GetSR(xy).enabled = isEnabled;
        internal static void EnableSR(int[] xy) => ActiveSR(true, xy);
        internal static void DisableSR(int[] xy) => ActiveSR(false, xy);
    }
}
