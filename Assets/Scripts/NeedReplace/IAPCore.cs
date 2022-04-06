using Chessy.Common.Entity;
using System;
using UnityEngine;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы

namespace Chessy.Common
{
    public sealed class IAPCore : IUpdate, IStoreListener //для получения сообщений из Unity Purchasing
    {
        readonly EntitiesModelCommon _eMCommon;


        public IAPCore(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;


            if (PlayerPrefs.HasKey("ads") == true)
            {
                //panelAds.SetActive(false);
                //panelAds_Done.SetActive(true);
            }
            else
            {
                //panelAds.SetActive(true);
                //panelAds_Done.SetActive(false);
            }

            if (PlayerPrefs.HasKey("vip") == true)
            {
                //panelVIP.SetActive(false);
                //panelVIP_Done.SetActive(true);
            }
            else
            {
                //panelVIP.SetActive(true);
                //panelVIP_Done.SetActive(false);
            }

            if (!eMCommon.ShopC.IsInitialized) //если еще не инициализаровали систему Unity Purchasing, тогда инициализируем
            {
                var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

                builder.AddProduct(ShopC.PREMIUM_NAME, ProductType.NonConsumable);

                UnityPurchasing.Initialize(this, builder);
            }



            //shopUIE.BuyButtonC.AddListener(delegate { BuyProductID(ShopC.PREMIUM_NAME); });
        }

        public void Update()
        {
            foreach (var item in _eMCommon.ShopC.StoreController.products.all)
            {
                Debug.Log(item.hasReceipt);
            }


            if (Input.GetKeyDown(KeyCode.L))
            {
                BuyProductID(ShopC.PREMIUM_NAME);
            }

        }

        void BuyProductID(string productId)
        {
            
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
            _eMCommon.ShopC.StoreController = controller;
            _eMCommon.ShopC.StoreExtProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }
    }
}