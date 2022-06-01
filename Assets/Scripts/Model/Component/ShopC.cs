using UnityEngine.Purchasing;

namespace Chessy.Common
{
    public struct ShopC
    {
        public IStoreController StoreController;      //доступ к системе Unity Purchasing
        public IExtensionProvider StoreExtProvider; // подсистемы закупок для конкретных магазинов

        public const string PREMIUM_NAME = "premium";

        public bool IsOpenedShopZone;

        public bool IsInitialized => StoreController != default && StoreExtProvider != default;
    }
}