using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;



public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public static Purchaser instance { set; get; }

    public static string PRODUCT_10DIAMONDS = "10_diamonds";
    public static string PRODUCT_50HEARTS = "50_hearts";
    public static string PRODUCT_5HEARTS = "5_hearts";
    
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {

        if (m_StoreController == null)
        {

            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {

        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }


        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


        builder.AddProduct(PRODUCT_10DIAMONDS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_50HEARTS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_5HEARTS, ProductType.Consumable);






        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy10Diamonds()
    {
        BuyProductID(PRODUCT_10DIAMONDS);
    }

    public void Buy50Hearts()
    {
        BuyProductID(PRODUCT_50HEARTS);
    }

    public void Buy5Hearts()
    {
        BuyProductID(PRODUCT_5HEARTS);
    }


    private void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }






    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string? message = null)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_10DIAMONDS, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("10 Diamonds Purchased Success!!", args.purchasedProduct.definition.id));
            //PlayerPrefs.SetInt("ads", 1);
            Score.Instance.Purchased10Diamonds();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_50HEARTS, StringComparison.Ordinal))
        {
            Debug.Log("50 Hearts purchased Successfully!!");
            Score.Instance.Purchase50Hearts();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_5HEARTS, StringComparison.Ordinal))
        {
            Debug.Log("5 Hearts purchased Successfully!!");
            Score.Instance.Purchase5Hearts();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }


        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}