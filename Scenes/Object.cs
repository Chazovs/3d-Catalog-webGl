using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
/*        UnityEngine.Object model = Resources.Load("https://bitrix.g4v.ru/test.3ds");
        Debug.Log(model);
        Debug.Log("Привет");
        Instantiate(model, new Vector3(0, 0, 0), Quaternion.identity);*/

        StartCoroutine(LoadObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoadObject()
    {
        WWW www = new WWW("https://bitrix.g4v.ru/test.3ds");

        yield return www;

        string write_path = Application.dataPath + "/Resources/model.3ds";

        System.IO.File.WriteAllBytes(write_path, www.bytes);
        
        Resources.Load<GameObject>("model");
        Debug.Log("Wrote to path");


       /* public ObjImporter objImporter;
    public GameObject emptyPrefabWithMeshRenderer;

    GameObject spawnedPrefab;

        Mesh importedMesh = objImporter.ImportFile(Application.dataPath + "/Objects/" + modelName);

        spawnedPrefab = Instantiate(emptyPrefabWithMeshRenderer);*/
    }
}
