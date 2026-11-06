using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataFileWriter : MonoBehaviour
{   
    [SerializeField]
    string fileName;
    [SerializeField]
    int dataDecimalCount;

    public float nothingTime = 0;
    public float driveTime = 0;
    public float brakeTime = 0;
    public float reverseTime = 0;
    public float totalDriveInput = 0;
    PermissionsHandler permissionsHandler;
    void Start()
    {
        permissionsHandler = GetComponent<PermissionsHandler>();
    }

    void WriteString(string line)
    {
        using (StreamWriter sw = new StreamWriter(Application.dataPath + $"/{fileName}.txt",true))
        {
            sw.WriteLine(line);
        }
    }
    public void SaveData()
    {   
        // int flooringMult = 10^(dataDecimalCount-1);
        // nothingTime = Mathf.Floor(nothingTime*flooringMult)/flooringMult;
        // driveTime = Mathf.Floor(driveTime*flooringMult)/flooringMult;
        // brakeTime = Mathf.Floor(brakeTime*flooringMult)/flooringMult;
        // reverseTime = Mathf.Floor(reverseTime*flooringMult)/flooringMult;
        // float averageDriveInput = Mathf.Floor((totalDriveInput/driveTime)*flooringMult)/flooringMult;
        float averageDriveInput = totalDriveInput/driveTime;

        string line = $"nothingTime:{nothingTime}|driveTime:{driveTime}|brakeTime:{brakeTime}|reverseTime:{reverseTime}|averageDriveInput:{averageDriveInput}";
        #if UNITY_ANDROID && !UNITY_EDITOR
        permissionsHandler.SaveTextToDocuments(fileName, line);
        #else
        WriteString(line);
        #endif
    }
}
