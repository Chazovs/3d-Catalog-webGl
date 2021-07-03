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
            if(keyPair.Value.categories == null)
            {
                continue;
            }

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

        _client.GetImageSprite(category.picture, categoryObject);

        categoryObject.transform.Find("TextCanva").transform.Find("CategoryText").GetComponent<Text>().text = category.name;
        categoryObject.GetComponent<BaseContainer>().content = category;
    }

    private void CreateCatalogObject(Catalog catalog)
    {
       GameObject catalogObject = _objectService.InstantiateCatalog(catalog, offsets);
        catalogObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        catalogObject.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = catalog.name;
    }

    private void CreateFloorObject()
    {
        GameObject floorObject = _objectService.CreateFloor();
        GameObject ceilingObject = _objectService.CreateCeiling();
        GameObject frontWallObject = _objectService.CreateWall();
        GameObject rearWallObject = _objectService.CreateWall();
        GameObject leftWallObject = _objectService.CreateWall();
        GameObject rightWallObject = _objectService.CreateWall();

        Vector3 ceilFloorScale = new Vector3(
            _categoriesCount * Constants.categoryOffset,
            0,
            _maxItemInCategoryCount * Constants.itemOffset + (Constants.itemOffset * 3)
            );
        Vector3 ceilFloorPosition = new Vector3(
            (_categoriesCount * Constants.categoryOffset) / 2 - (Constants.catalogOffset / 4),
            0,
            (_maxItemInCategoryCount * Constants.itemOffset) / 2 - (Constants.itemOffset * 1.5f)
            );

        floorObject.transform.localScale += ceilFloorScale;
        ceilingObject.transform.localScale += ceilFloorScale;
        floorObject.transform.position += ceilFloorPosition;
        ceilingObject.transform.position += ceilFloorPosition + new Vector3(0, Constants.ceilLevel, 0);

        frontWallObject.transform.localScale = new Vector3(floorObject.transform.localScale.x, 0, Constants.ceilLevel);
        frontWallObject.transform.position = new Vector3(
            floorObject.transform.position.x,
            ceilFloorPosition.y + (Constants.ceilLevel/2),
             floorObject.transform.position.z + floorObject.transform.localScale.z/2
            );
        frontWallObject.transform.rotation = Quaternion.Euler(90, 0, 0);

        rearWallObject.transform.localScale = frontWallObject.transform.localScale;
        rearWallObject.transform.rotation = frontWallObject.transform.rotation;
        rearWallObject.transform.position = new Vector3(
            frontWallObject.transform.position.x,
            frontWallObject.transform.position.y,
            frontWallObject.transform.position.z - floorObject.transform.localScale.z
            );

        leftWallObject.transform.localScale = new Vector3(Constants.ceilLevel, ceilFloorScale.y, floorObject.transform.localScale.z);
        leftWallObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        leftWallObject.transform.position = new Vector3(
            floorObject.transform.position.x + floorObject.transform.localScale.x / 2,
            ceilFloorPosition.y + (Constants.ceilLevel / 2),
            floorObject.transform.position.z
            );

        rightWallObject.transform.localScale = leftWallObject.transform.localScale;
        rightWallObject.transform.rotation = leftWallObject.transform.rotation;
        rightWallObject.transform.position = new Vector3(
            floorObject.transform.position.x - floorObject.transform.localScale.x / 2,
           leftWallObject.transform.position.y,
            leftWallObject.transform.position.z
            ); ;
    }
}