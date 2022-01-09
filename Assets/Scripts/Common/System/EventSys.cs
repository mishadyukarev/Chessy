namespace Game.Common
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