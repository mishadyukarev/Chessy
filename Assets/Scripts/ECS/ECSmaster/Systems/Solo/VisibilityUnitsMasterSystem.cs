using Leopotam.Ecs;
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
                    if(CellUnitComponent(x, y).IsHim(InstanceGame.MasterClient))
                    {
                        if (CellEnvironmentComponent(x, y).HaveTree)
                        {
                            CellUnitComponent(x, y).IsActiveUnitOther = false;
                        }
                    }
                    else
                    {
                        if (CellEnvironmentComponent(x, y).HaveTree)
                        {
                            CellUnitComponent(x, y).IsActiveUnitMaster = false;
                        }
                    }
                }
            }
        }
    }
}