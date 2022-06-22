using System;
using UnityEngine;

namespace Chessy.Common.Model.System
{
    public sealed partial class SystemsModelCommon : IUpdate
    {
        public void BuyPremiumProduct()
        {
            if (_e.ShopC.IsInitialized) //если покупка инициализирована 
            {
                var product = _e.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _e.ShopC.StoreController.InitiatePurchase(product); //покупаем
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
    }
}