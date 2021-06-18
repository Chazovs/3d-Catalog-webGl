using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static string bxSessId = "";
    internal static string siteId = "";

    public void SetSiteId(string siteId)
    {
        siteId = siteId;
    }

    public void SetBxSessId(string sessId)
    {
        bxSessId = sessId;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Client client = new Client();*/
        TestClient client = new TestClient();
        StartCoroutine(client.UploadCatalog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
