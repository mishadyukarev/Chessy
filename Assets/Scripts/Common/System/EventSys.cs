namespace Chessy.Common
{
    public sealed class EventSys
    {
        public EventSys()
        {
            ShopUIC.AddListExit(ExitShop);
        }

        private void ExitShop()
        {
            ShopUIC.DisableZone();
        }
    }
}