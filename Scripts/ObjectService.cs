using System;
using UnityEngine;

public class ObjectService : MonoBehaviour
{
    public GameObject ItemPrefab;

    internal void InstantiateItem(Item item)
    {
        Instantiate(ItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}