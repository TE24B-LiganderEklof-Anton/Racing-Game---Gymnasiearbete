using System.IO;
using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

public class PermissionsHandler : MonoBehaviour
{
    private void Start()
    {
        RequestPermissions();
    }

    private void RequestPermissions()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
#endif
    }

    public string GetDocumentsDirectoryPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
        {
            using (AndroidJavaObject file = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", "Documents"))
            {
                return file.Call<string>("getAbsolutePath");
            }
        }
#else
        // Fallback path when testing in Unity Editor
        return Application.persistentDataPath;
#endif
    }

    public void SaveTextToDocuments(string fileName, string fileContent)
    {
        string folderPath = GetDocumentsDirectoryPath();

        // Ensure the Documents folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fullPath = Path.Combine(folderPath, fileName);

        try
        {
            File.WriteAllText(fullPath, fileContent);
            Debug.Log($"File successfully saved to: {fullPath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to write file to Documents: {ex.Message}");
        }
    }
}