using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownHandler : MonoBehaviour
{
    [SerializeField]
    GameObject car;

    [SerializeField]
    float countdownTime = 3;
    CarController carController;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject controls;
    bool done = false;
    bool started = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carController = car.GetComponent<CarController>();
        carController.canDrive = false;
        countdownTime += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        if (done) return;
        countdownTime -= Time.deltaTime;
        text.SetText(Mathf.Floor(countdownTime).ToString());
        if (countdownTime <= 1)
        {
            text.SetText("GO");
            carController.canDrive = true;
            if (countdownTime <= 0)
            {
                done = true;
                text.SetText("");
            }
        }

    }
    public void StartCountdown()
    {
        started = true;
        controls.SetActive(false);
    }
}
