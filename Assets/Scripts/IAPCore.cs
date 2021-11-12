using System;
using UnityEngine;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы
using UnityEngine.UI;

public class IAPCore : MonoBehaviour, IStoreListener //для получения сообщений из Unity Purchasing
{
    private IStoreController _storeController;          //доступ к системе Unity Purchasing
    private IExtensionProvider _storeExtProvider; // подсистемы закупок для конкретных магазинов

    public string _premium = "premium"; //одноразовые - nonconsumable или может быть подписка

    private bool IsInitialized => _storeController != default && _storeExtProvider != default;

    private void Start()
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


        if (!IsInitialized) //если еще не инициализаровали систему Unity Purchasing, тогда инициализируем
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(_premium, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        GetComponent<Button>().onClick.AddListener(delegate { BuyProductID(_premium); } );
    }

    private void Update()
    {
        foreach (var item in _storeController.products.all)
        {
            Debug.Log(item.hasReceipt);
        }
    }

    private void BuyProductID(string productId)
    {
        if (IsInitialized) //если покупка инициализирована 
        {
            var product = _storeController.products.WithID(productId); //находим продукт покупки 

            if (product == default) throw new Exception();

            if (product.availableToPurchase) //если продукт найдет и готов для продажи
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                _storeController.InitiatePurchase(product); //покупаем
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
        if (String.Equals(args.purchasedProduct.definition.id, _premium, StringComparison.Ordinal))
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
        _storeController = controller;
        _storeExtProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
}