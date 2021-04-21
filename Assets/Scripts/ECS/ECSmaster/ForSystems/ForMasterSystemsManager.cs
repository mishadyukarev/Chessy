internal class ForMasterSystemsManager
{

    internal GetterUnitForMasterSystem InventorForGeneralSystem;

    internal ForMasterSystemsManager(ECSmanager eCSmanager, SupportManager supportManager)
    {
        InventorForGeneralSystem = new GetterUnitForMasterSystem(eCSmanager, supportManager);
    }
}
