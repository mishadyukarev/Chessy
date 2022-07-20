using UnityEngine.Purchasing;
namespace Chessy.Model
{
    public sealed class ShopC
    {
        public IStoreController StoreController;      //доступ к системе Unity Purchasing
        public IExtensionProvider StoreExtProvider; // подсистемы закупок для конкретных магазинов

        public bool IsOpenedShopZone;

        public bool IsInitialized => StoreController != default && StoreExtProvider != default;

        public bool HasReceipt(in string name) => StoreController.products.WithID(name).hasReceipt;
    }
}