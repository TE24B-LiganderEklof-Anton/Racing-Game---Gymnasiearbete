using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler instance;
    public bool recordData;
    void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
}
