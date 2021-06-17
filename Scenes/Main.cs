using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static string bxSessId = "";

    public void logBxSessId()
    {
        Debug.Log(bxSessId);

        MarketService marketService = new MarketService();

        marketService.CreateMarket();
    }

    public void setBxSessId(string sessId)
    {
        bxSessId = sessId;
    }

    // Start is called before the first frame update
    void Start()
    {
        Client client = new Client();
        StartCoroutine(client.UploadCatalog());
/*        MarketService marketService = new MarketService();

        marketService.CreateMarket();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
