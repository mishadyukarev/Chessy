using UnityEngine.Purchasing;

namespace Chessy.Common
{
    public struct ShopComC
    {
        private static IStoreController _storeController;       //доступ к системе Unity Purchasing
        private static IExtensionProvider _storeExtProvider; // подсистемы закупок для конкретных магазинов

        public const string PREMIUM_NAME = "premium";

        public static bool IsInitialized => _storeController != default && _storeExtProvider != default;
        public static Product Product(string id)
        {
            return _storeController.products.WithID(id);
        }
        public static bool HasReceipt(string id)
        {
            return Product(id).hasReceipt;
        }



        public static void Set(IStoreController store)
        {
            _storeController = store;
        }
        public static void Set(IExtensionProvider exten)
        {
            _storeExtProvider = exten;
        }
        public static void InitiatePurchase(Product product)
        {
            _storeController.InitiatePurchase(product);
        }
    }
}