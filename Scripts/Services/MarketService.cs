using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketService
{
    private ObjectService _objectService;
    private Client _client;
    internal Offsets offsets = new Offsets();
    private int _maxItemInCategoryCount = 0;
    private int _categoriesCount = 0;

    public MarketService() {
        _objectService = GameObject.Find("ObjectService").GetComponent<ObjectService>();
        _client = GameObject.Find("Client").GetComponent<Client>();
    }

    public void CreateMarket(Dictionary<string, Catalog> catalogs)
    {
        foreach(var keyPair in catalogs)
        {
            CreateCatalogObject(keyPair.Value);
            CreateMarketCatalog(keyPair);
            offsets.catalogOffset += Constants.catalogOffset;
        }

        CreateFloorObject();
        _objectService.InstantiateCustomer(_categoriesCount);
    }

    private void CreateMarketCatalog(KeyValuePair<string, Catalog> catalogkeyPair)
    {
        Catalog catalog = catalogkeyPair.Value;
        _categoriesCount += catalog.categories.Count;

        foreach (var categoryKeyPair in catalog.categories)
        {
            offsets.itemOffset = 0f;
            CreateCategoryObject(categoryKeyPair.Value);
            CreateMarketCategory(categoryKeyPair);
            offsets.categoryOffset += Constants.categoryOffset;
        }
    }

    private void CreateMarketCategory(KeyValuePair<string, Category> categoryKeyPair)
    {
        Category category = categoryKeyPair.Value;
        _maxItemInCategoryCount = category.items.Count > _maxItemInCategoryCount 
            ? category.items.Count 
            : _maxItemInCategoryCount;

        foreach (Item item in category.items)
        {
            CreateMarketItem(item);
            offsets.itemOffset += Constants.itemOffset;
        }
    }

    private void CreateMarketItem(Item item)
    {
        GameObject itemObject = _objectService.InstantiateItem(item, offsets);

        _client.GetImageSprite(item.imagePath, itemObject);

        itemObject.GetComponent<BaseContainer>().content = item;
    }

    private void CreateCategoryObject(Category category)
    {
        GameObject categoryObject = _objectService.InstantiateCategory(category, offsets);
        categoryObject.GetComponent<BaseContainer>().content = category;
    }

    private void CreateCatalogObject(Catalog catalog)
    {
       GameObject catalogObject = _objectService.InstantiateCatalog(catalog, offsets);
       catalogObject.GetComponent<TextMesh>().text = catalog.name;
    }

    private void CreateFloorObject()
    {
        GameObject floorObject = _objectService.CreateFloor();

        floorObject.transform.localScale += new Vector3(
            _categoriesCount * Constants.categoryOffset,
            0,
            _maxItemInCategoryCount * Constants.itemOffset + (Constants.itemOffset * 3)
            );

        floorObject.transform.position += new Vector3(
            (_categoriesCount * Constants.categoryOffset)/2 - (Constants.catalogOffset/4),
            -(Constants.scale/2),
            (_maxItemInCategoryCount * Constants.itemOffset)/2 - (Constants.itemOffset * 1.5f)
            );
    }
}