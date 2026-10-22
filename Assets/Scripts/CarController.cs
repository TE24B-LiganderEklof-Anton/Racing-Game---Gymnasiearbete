using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarController : MonoBehaviour
{
    [SerializeField]
    DataFileWriter dataFileWriter;
    float forward = 0;
    float steering = 0;
    [SerializeField]
    float turnAngle = 15;
    [SerializeField]
    float torque = 1;
    [SerializeField]
    float breakTorque = 1;
    [SerializeField]
    WheelCollider frontLeftWheel;
    [SerializeField]
    WheelCollider frontRightWheel;
    [SerializeField]
    Transform steeringWheel;
    [SerializeField]
    float steeringWheelMaxAngle = 180;
    Vector3 steeringWheelDefaultRot;
    public bool canDrive = true;

    GameObject wheelColliders;
    GameObject wheelModels;

    void Start()
    {
        wheelColliders = transform.Find("WheelColliders").gameObject;
        wheelModels = transform.Find("WheelModels").gameObject;
        steeringWheelDefaultRot = steeringWheel.localEulerAngles;
    }

    void Update()
    {
        float targetRot = steeringWheelDefaultRot.z + (steering * steeringWheelMaxAngle);
        float difference = targetRot - steeringWheel.localEulerAngles.z;

        steeringWheel.localEulerAngles = new Vector3(steeringWheelDefaultRot.x, steeringWheelDefaultRot.y, steeringWheel.localEulerAngles.z + (difference * 0.1f));

        if (!canDrive)
        {
            return;
        }

        float speed = 0;
        foreach (WheelCollider wheel in this.gameObject.GetComponentsInChildren<WheelCollider>())
        {
            speed += speed = wheel.rpm / 60 * wheel.radius * 2 * Mathf.PI;
        }
        speed /= this.gameObject.GetComponentsInChildren<WheelCollider>().Length;
        float motorTorque = 0;
        float brakeTorque = 0;

        if ((forward < 0 && speed > 1) || (forward > 0 && speed < -1))
        {
            brakeTorque = breakTorque;
            dataFileWriter.brakeTime += Time.deltaTime;
        }
        else if (forward > 0)
        {
            motorTorque = torque * forward;
            brakeTorque = 0;
            dataFileWriter.driveTime += Time.deltaTime;
        }
        else if (forward < 0)
        {
            motorTorque = torque * forward * 0.8f;
            brakeTorque = 0;
            dataFileWriter.reverseTime += Time.deltaTime;
        }
        else if (speed > -1 && speed < 1)
        {
            brakeTorque = breakTorque;
            dataFileWriter.nothingTime += Time.deltaTime;
        }
        else
        {
            dataFileWriter.nothingTime += Time.deltaTime;
  
        }

        foreach (WheelCollider wheel in this.gameObject.GetComponentsInChildren<WheelCollider>())
        {

            wheel.motorTorque = motorTorque;
            wheel.brakeTorque = brakeTorque;

            // print(wheel.motorTorque + ":" + wheel.brakeTorque);
        }
        frontRightWheel.steerAngle = turnAngle * steering;
        frontLeftWheel.steerAngle = turnAngle * steering;

        foreach (Transform transform in wheelModels.GetComponentsInChildren<Transform>())
        {
            if (transform.name == wheelModels.name) continue;
            string name = "Wheel-" + transform.name;
            WheelCollider collider = wheelColliders.transform.Find(name).GetComponent<WheelCollider>();
            Vector3 pos;
            Quaternion rot;
            collider.GetWorldPose(out pos, out rot);

            transform.position = pos;
            transform.rotation = rot;

        }
    }
    // public void OnMove(InputValue input)
    // {
    //     Vector2 moveVector = input.Get<Vector2>();
    //     forward = moveVector.y;
    //     steering = moveVector.x;

    // }
    public void OnDrive(InputValue input)
    {
        forward = input.Get<float>();
    }
    public void OnMove(InputValue input)
    {
        // steering = input.Get<Vector2>().x;
        float inputValue = input.Get<Vector2>().x;
        steering = 1 - Mathf.Cos(inputValue * Mathf.PI / 2);
        if (inputValue < 0)
        {
            steering *= -1;
        }
        // print(inputValue + ":" + steering);
    }
}
