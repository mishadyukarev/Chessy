using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;


internal class VisibilityUnitsMasterSystem : CellReduction, IEcsRunSystem
{
    internal VisibilityUnitsMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }


    public void Run()
    {
        for (int x = 0; x < Xcount; x++)
        {
            for (int y = 0; y < Ycount; y++)
            {
                CellUnitComponent(x, y).IsActiveUnitMaster = true;
                CellUnitComponent(x, y).IsActiveUnitOther = true;


                if (CellUnitComponent(x, y).HaveUnit)
                {
                    if (CellUnitComponent(x, y).IsHim(InstanceGame.MasterClient))
                    {
                        if (CellEnvironmentComponent(x, y).HaveTree)
                        {
                            List<int[]> list = InstanceGame.SupportGameManager.FinderWay.TryGetXYAround(new int[] { x, y });

                            foreach (var item in list)
                            {
                                //if (CellUnitComponent(item).IsHim(InstanceGame.MasterClient))
                                //{
                                //    CellUnitComponent(x, y).IsActiveUnitOther = false;
                                //    break;
                                //}
                            }
                        }
                    }
                    else
                    {
                        if (CellEnvironmentComponent(x, y).HaveTree)
                        {
                            List<int[]> list = InstanceGame.SupportGameManager.FinderWay.TryGetXYAround(new int[] { x, y });

                            foreach (var item in list)
                            {
                                //if (CellUnitComponent(item).IsHim(InstanceGame.MasterClient))
                                //{
                                //    CellUnitComponent(x, y).IsActiveUnitMaster = false;
                                //    break;
                                //}
                            }
                        }
                    }
                }
            }
        }
    }
}