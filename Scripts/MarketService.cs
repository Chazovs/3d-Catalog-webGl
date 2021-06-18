using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketService
{
    public void CreateMarket(Dictionary<string, Catalog> catalogs)
    {
        foreach(var keyPair in catalogs)
        {
            Debug.Log("catalog");
            CreateMarketCatalog(keyPair);
        }
    }

    private void CreateMarketCatalog(KeyValuePair<string, Catalog> catalogkeyPair)
    {
        Catalog catalog = catalogkeyPair.Value;

        foreach(var categoryKeyPair in catalog.categories)
        {
            Debug.Log("category");
            CreateMarketCategory(categoryKeyPair);
        }
    }

    private void CreateMarketCategory(KeyValuePair<string, Category> categoryKeyPair)
    {
        Category category = categoryKeyPair.Value;

        foreach(Item item in category.items)
        {
            Debug.Log("item");
            CreateMarketItem(item);
        }
    }

    private void CreateMarketItem(Item item)
    {
        ObjectService objectService = GameObject.Find("ObjectService").GetComponent<ObjectService>();

        objectService.InstantiateItem(item);
    }
}