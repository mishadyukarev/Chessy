internal class SupportSystems
{
    internal InventorSupportSystem InventorSupportSystem;


    internal SupportSystems(ECSmanager eCSmanager, SupportManager supportManager)
    {
        InventorSupportSystem = new InventorSupportSystem(eCSmanager, supportManager);
    }
}