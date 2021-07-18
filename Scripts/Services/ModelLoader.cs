using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;

public class ModelLoader : MonoBehaviour
{
    GameObject wrapper;
    string filePath;

    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/Files/";

        wrapper = new GameObject
        {
            name = "Model"
        };

        DownloadFile(ModelStore.getModel());
    }
    public void DownloadFile(string url)
    {
        string path = GetFilePath(url);

        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");
            }
            else
            {
                Debug.Log("start load");
                // Save the model into a new wrapper
                LoadModel(path);
            }
        }));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];

        return $"{filePath}{filename}";
    }

    void LoadModel(string path)
    {
        ResetWrapper();
        GameObject model = Importer.LoadFromFile(path);

        model.AddComponent<MeshCollider>();
        model.AddComponent<Renderer>();
        Material material = Resources.Load<Material>("Materials/default");
        model.GetComponent<Renderer>().material = material;
        /*model.transform.SetParent(wrapper.transform);*/
    }

    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            yield return req.SendWebRequest();
            callback(req);
        }
    }

    void ResetWrapper()
    {
        if (wrapper != null)
        {
            foreach (Transform trans in wrapper.transform)
            {
                Destroy(trans.gameObject);
            }
        }
    }
}