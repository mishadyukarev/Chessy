using Chessy.Common.Entity;
using System;
using UnityEngine;

namespace Chessy.Common
{
    public sealed class BuyPremiumProductS
    {
        readonly EntitiesModelCommon _eMC;

        internal BuyPremiumProductS(in EntitiesModelCommon eMC)
        {
            _eMC = eMC;
        }


        public void Buy()
        {
            if (_eMC.ShopC.IsInitialized) //если покупка инициализирована 
            {
                var product = _eMC.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _eMC.ShopC.StoreController.InitiatePurchase(product); //покупаем
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