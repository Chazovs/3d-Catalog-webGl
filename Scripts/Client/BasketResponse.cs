using System.Collections.Generic;

public class BasketResponse : AbstractResponse
{
    public List<BasketItem> basketItems;
    public float? totalPrice;
}