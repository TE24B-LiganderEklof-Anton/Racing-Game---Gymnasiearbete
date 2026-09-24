using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
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

    GameObject wheelColliders;
    GameObject wheelModels;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        // rigidBody.centerOfMass = new(0,-0.5f,0);

        wheelColliders = transform.Find("WheelColliders").gameObject;
        wheelModels = transform.Find("WheelModels").gameObject;
    }

    void Update()
    {
        foreach (WheelCollider wheel in this.gameObject.GetComponentsInChildren<WheelCollider>())
        {
            float speed = wheel.rpm / 60 * wheel.radius * 2 * Mathf.PI;

            wheel.motorTorque = 0;
            if ((forward < 0 && speed > 1) || (forward > 0 && speed < -1))
            {
                wheel.brakeTorque = breakTorque;
            }
            else
            {
                if (forward > 0)
                {
                    wheel.motorTorque = torque * forward;
                }
                if (forward < 0)
                {
                    wheel.motorTorque = torque * forward * 0.8f;
                }
                wheel.brakeTorque = 0;
            }
        print(wheel.motorTorque + ":" + wheel.brakeTorque);
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

        // rigidBody.linearVelocity *= 0.99f;
    }
    public void OnMove(InputValue input)
    {
        Vector2 moveVector = input.Get<Vector2>();
        forward = moveVector.y;
        steering = moveVector.x;

    }
}
