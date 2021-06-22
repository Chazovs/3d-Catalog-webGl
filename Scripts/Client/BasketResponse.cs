using System.Collections.Generic;

public class BasketResponse : AbstractResponse
{
    public Dictionary<string, BasketItems> basketItems;
    public float totalPrice;
}