using System;
using UnityEngine;

public class ObjectService : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject CategoryPrefab;
    public GameObject CatalogPrefab;
    public GameObject FloorPrefab;
    public GameObject Customer;
    public GameObject MainCamera;
    public GameObject CeilingPrefab;
    public GameObject WallPrefab;

    internal GameObject InstantiateItem(Item item, Offsets offsets)
    {
        return Instantiate (ItemPrefab, offsets.CalculateItemOffset(), Quaternion.identity);
    }

    internal GameObject InstantiateCategory(Category category, Offsets offsets)
    {
       return Instantiate(CategoryPrefab, offsets.CalculateCategoryOffset(), Quaternion.identity);
    }

    internal GameObject InstantiateCatalog(Catalog catalog, Offsets offsets)
    {
       return Instantiate(
           CatalogPrefab,
           offsets.CalculateCatalogOffset(catalog.categories.Count),
           Quaternion.identity
           );
    }

    internal GameObject CreateFloor()
    {
        return Instantiate(FloorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    internal GameObject CreateWall()
    {
        return Instantiate(WallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    internal GameObject CreateCeiling()
    {
        return Instantiate(CeilingPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    internal GameObject InstantiateCustomer(int maxCategories)
    {
        Customer.transform.position = new Vector3(
                (maxCategories / 2) * Constants.categoryOffset,
                2,
                -Constants.itemOffset * 2.5f
                );

        return Customer;
    }

    internal void UpdateBasketInfo(BasketResponse data)
    {
        throw new NotImplementedException();
    }
}
