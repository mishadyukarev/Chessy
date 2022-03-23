using Chessy.Common.Entity.View.UI;
using System;
using UnityEngine;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы

namespace Chessy.Common
{
    public sealed class IAPCore : IStoreListener //для получения сообщений из Unity Purchasing
    {
        public IAPCore(in ShopUIE shopUIE)
        {
            //if (PlayerPrefs.HasKey("ads") == true)
            //{
            //    //panelAds.SetActive(false);
            //    //panelAds_Done.SetActive(true);
            //}
            //else
            //{
            //    //panelAds.SetActive(true);
            //    //panelAds_Done.SetActive(false);
            //}

            //if (PlayerPrefs.HasKey("vip") == true)
            //{
            //    //panelVIP.SetActive(false);
            //    //panelVIP_Done.SetActive(true);
            //}
            //else
            //{
            //    //panelVIP.SetActive(true);
            //    //panelVIP_Done.SetActive(false);
            //}

            if (!ShopC.IsInitialized) //если еще не инициализаровали систему Unity Purchasing, тогда инициализируем
            {
                var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

                builder.AddProduct(ShopC.PREMIUM_NAME, ProductType.NonConsumable);

                UnityPurchasing.Initialize(this, builder);
            }

            shopUIE.BuyButtonC.AddListener(delegate { BuyProductID(ShopC.PREMIUM_NAME); });
        }

        //private void Update()
        //{
        //    foreach (var item in _storeController.products.all)
        //    {
        //        Debug.Log(item.hasReceipt);
        //    }
        //}

        private void BuyProductID(string productId)
        {
            if (ShopC.IsInitialized) //если покупка инициализирована 
            {
                var product = ShopC.Product(productId); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    ShopC.InitiatePurchase(product); //покупаем
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }



        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) //контроль покупок
        {
            if (String.Equals(args.purchasedProduct.definition.id, ShopC.PREMIUM_NAME, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

                //действия при покупке
                if (PlayerPrefs.HasKey("vip") == false)
                {
                    PlayerPrefs.SetInt("vip", 0);
                }
            }
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            return PurchaseProcessingResult.Complete;
        }
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }



        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
            ShopC.Set(controller);
            ShopC.Set(extensions);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }
    }
}