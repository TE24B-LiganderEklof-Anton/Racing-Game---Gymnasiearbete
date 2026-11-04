using TMPro;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI uiText;
    [SerializeField]
    DataFileWriter dataFileWriter;
    bool goalReached = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Goal" && !goalReached)
        {
            goalReached = true;
            uiText.text = "YOU WON!!!";
            dataFileWriter.SaveData();
        }
    }
}
