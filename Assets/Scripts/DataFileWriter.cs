using System.IO;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void WriteString(string line)
    {
        using (StreamWriter sw = new StreamWriter(Application.dataPath + $"/{fileName}.txt",true))
        {
            sw.WriteLine(line);
        }
    }
    public void SaveData()
    {   
        int flooringMult = 10^(dataDecimalCount-1);
        nothingTime = Mathf.Floor(nothingTime*flooringMult)/flooringMult;
        driveTime = Mathf.Floor(driveTime*flooringMult)/flooringMult;
        brakeTime = Mathf.Floor(brakeTime*flooringMult)/flooringMult;
        reverseTime = Mathf.Floor(reverseTime*flooringMult)/flooringMult;

        string line = $"nothingTime:{nothingTime}|driveTime:{driveTime}|brakeTime:{brakeTime}|reverseTime:{reverseTime}";
        WriteString(line);
    }
    void OnApplicationQuit()
    {
        SaveData();
    }
}
