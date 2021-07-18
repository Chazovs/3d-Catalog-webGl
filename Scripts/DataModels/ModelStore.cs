using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModelStore
{
    private static string modelPath;

    public static void setModel(string newModelPath) {
        modelPath = newModelPath;
    }

    public static string getModel()
    {
        return "http://unimarket.local" + modelPath;
    }

    public static void reset()
    {
        modelPath = "";
    }
}

