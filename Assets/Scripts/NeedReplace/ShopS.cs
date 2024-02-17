using Chessy.Model.Entity;
using Chessy.Model.Values;
using UnityEngine;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы

namespace Chessy.Model
{
    public sealed class ShopS : SystemAbstract//, IStoreListener //для получения сообщений из Unity Purchasing
    {
        public ShopS(in EntitiesModel eM) : base(eM)
        {
            //if (!_shopC.IsInitialized)
            //{
            //    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //    builder.AddProduct(ShopValues.PREMIUM_NAME, ProductType.NonConsumable);

            //    UnityPurchasing.Initialize(this, builder);
            //}
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) //контроль покупок
        {
            //if (String.Equals(args.purchasedProduct.definition.id, ShopC.PREMIUM_NAME, StringComparison.Ordinal))
            //{
            //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //    //действия при покупке
            //    if (PlayerPrefs.HasKey("vip") == false)
            //    {
            //        PlayerPrefs.SetInt("vip", 0);
            //    }
            //}
            //else
            //{
            //    Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            //}

            return PurchaseProcessingResult.Complete;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            //Debug.Log("OnInitialized: PASS");
            shopC.StoreController = controller;
            shopC.StoreExtProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }
}