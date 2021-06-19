using System;
using UnityEngine;

public class Offsets
{
    internal float itemOffset = 0;
    internal float categoryOffset = 0;
    internal float catalogOffset = 0;

    internal Vector3 CalculateItemOffset()
    {
        return new Vector3(categoryOffset + catalogOffset, 0, itemOffset);
    }

    internal Vector3 CalculateCategoryOffset()
    {
        return new Vector3(categoryOffset + catalogOffset, 0, 0 - Constants.itemOffset);
    }

    internal Vector3 CalculateCatalogOffset(int categoryCount)
    {
        float catalogX = (categoryCount / 2) * Constants.categoryOffset; // + (categoryOffset + catalogOffset)

        return new Vector3(catalogX, Constants.catalogOffset, 0);
    }
}
