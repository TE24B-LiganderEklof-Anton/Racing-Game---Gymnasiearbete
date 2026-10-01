using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel;
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OnRecordDataToggleChange(Toggle toggle)
    {
        DataHandler.instance.recordData = toggle.isOn;
        print(DataHandler.instance.recordData);
    }

}
