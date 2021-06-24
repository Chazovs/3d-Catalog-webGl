using System.Collections.Generic;

public class Category : Interactive
{
    public int depthLevel;
    public int id;
    public List<Item> items;
    public string? name;
    public int? parentId;
    public int? parentSection;
    public string? picture;

    public void MouseOneDown()
    {
        throw new System.NotImplementedException();
    }

    public void MouseZeroDown()
    {
        throw new System.NotImplementedException();
    }
}