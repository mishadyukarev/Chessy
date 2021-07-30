using Assets.Scripts.ECS.Game.General.Entities.Containers;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal sealed class CellFireDataWorker
    {
        private static CellFireDataContainerEnts _cellFireEntsContainer;

        internal CellFireDataWorker(CellFireDataContainerEnts cellFireEntsContainer)
        {
            _cellFireEntsContainer = cellFireEntsContainer;
        }

        internal static void SetFire(bool haveEffect, int[] xy) => _cellFireEntsContainer.CellFireEnt_HaverEffectCom(xy).HaveFire = haveEffect;
        internal static bool HaveFire(int[] xy) => _cellFireEntsContainer.CellFireEnt_HaverEffectCom(xy).HaveFire;
        internal static void EnableFire(int[] xy) => SetFire(true, xy);
        internal static void ResetFire(int[] xy) => SetFire(false, xy);

        internal static int TimeSteps(int[] xy) => _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps;
        internal static void SetTimeSteps(int value, int[] xy) => _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps = value;
        internal static void ResetTimeSteps(int[] xy) => SetTimeSteps(default, xy);
        internal static void AddTimeSteps(int[] xy, int adding = 1) => SetTimeSteps(TimeSteps(xy) + adding, xy);
        internal static void TakeTimeSteps(int[] xy, int taking = 1) => SetTimeSteps(TimeSteps(xy) - taking, xy);

        internal static void SyncFireData(bool haveFire, int timeSteps, int[] xy)
        {
            SetFire(haveFire, xy);
            SetTimeSteps(timeSteps, xy);
        }
    }
}
