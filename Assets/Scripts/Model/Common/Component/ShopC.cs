using UnityEngine.Purchasing;

namespace Chessy.Common
{
    internal struct ShopC
    {
        internal IStoreController StoreController;       //доступ к системе Unity Purchasing
        internal IExtensionProvider StoreExtProvider; // подсистемы закупок для конкретных магазинов

        internal const string PREMIUM_NAME = "premium";

        internal bool IsInitialized => StoreController != default && StoreExtProvider != default;
    }
}