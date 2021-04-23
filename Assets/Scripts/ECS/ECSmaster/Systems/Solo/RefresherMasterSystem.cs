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

                if (CellUnitComponent(x, y).IsRelaxed)
                {
                    switch (CellUnitComponent(x, y).UnitType)
                    {
                        case UnitTypes.King:
                            AddHealth(_startValues.HEALTH_FOR_ADDING_KING);
                            break;

                        case UnitTypes.Pawn:
                            AddHealth(_startValues.HEALTH_FOR_ADDING_PAWN);
                            break;

                        default:
                            break;

                            void AddHealth(int health)
                            {
                                CellUnitComponent(x, y).AmountHealth += health;
                                if (CellUnitComponent(x, y).AmountHealth > health)
                                    CellUnitComponent(x, y).AmountHealth = health;
                            }
                    }
                }

            }
        }
        _economyMasterComponent.Unref().GoldMaster += 20;
        _economyMasterComponent.Unref().GoldOther += 20;
    }
}
