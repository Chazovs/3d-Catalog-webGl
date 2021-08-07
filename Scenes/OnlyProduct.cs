using UnityEngine;

public class OnlyProduct : MonoBehaviour
{
    public string currentModelPath = "";

    private void Start()
    {
        Debug.Log("2");
        Debug.Log(currentModelPath);
        ModelStore.setModel(currentModelPath);
    }

    public void SetModelPath(string modelPath = "")
    {
        Debug.Log("1");
        Debug.Log(currentModelPath);
        currentModelPath = modelPath;
    }
}
