
public static class ModelStore
{
    private static string modelPath;

    public static void setModel(string newModelPath) {
        modelPath = newModelPath;
    }

    public static string getModel()
    {
        return modelPath;
    }

    public static void reset()
    {
        modelPath = "";
    }
}

