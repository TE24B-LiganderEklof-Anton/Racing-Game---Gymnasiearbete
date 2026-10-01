using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Goal")
        {
            print("Reached Goal");
        }
    }
}
