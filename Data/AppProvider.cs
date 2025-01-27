namespace Data;

public sealed class AppProvider
{
    private static AppProvider _instance;

    private AppProvider()
    {
    }

    public static AppProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AppProvider();
            }

            return _instance;
        }
    }

    public string BaseUrl = "";
}