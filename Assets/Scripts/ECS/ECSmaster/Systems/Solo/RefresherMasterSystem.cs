using Leopotam.Ecs;

public class RefresherMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponent = default;

    internal RefresherMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
    }


    public void Run()
    {
        for (int x = 0; x < Xcount; x++)
        {
            for (int y = 0; y < Ycount; y++)
            {
                CellUnitComponent(x, y).RefreshAmountSteps();
            }
        }
        _economyMasterComponent.Unref().GoldMaster += 20;
        _economyMasterComponent.Unref().GoldOther += 20;
    }
}
