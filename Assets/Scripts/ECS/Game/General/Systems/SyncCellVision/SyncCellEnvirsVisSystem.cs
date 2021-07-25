using Assets.Scripts.Workers.Game.Else.CellEnvir;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEnvirsVisSystem : SystemGeneralReduction
    {
        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    var xy = new int[] { x, y };


                    for (int curNumberEnvirType = 1; curNumberEnvirType <= (int)EnvironmentTypes.Mountain; curNumberEnvirType++)
                    {
                        if (CellEnvirDataWorker.HaveEnvironment((EnvironmentTypes)curNumberEnvirType, xy))
                        {
                            CellEnvirVisWorker.ActiveEnvirVis(true, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                        else
                        {
                            CellEnvirVisWorker.ActiveEnvirVis(false, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                    }
                }
        }

    }
}
