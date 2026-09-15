using Unity.Mathematics;
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

    // Update is called once per frame
    void Update()
    {
       foreach(WheelCollider wheel in this.gameObject.GetComponentsInChildren<WheelCollider>())
        {
            wheel.motorTorque = torque*forward;

            // wheel.transform.localRotation = quaternion.EulerXYZ(wheel.transform.rotation.x,wheel.transform.rotation.y+turnAngle*steering,wheel.transform.rotation.z);
            wheel.steerAngle = turnAngle*steering;
        } 
    }
    public void OnMove(InputValue input)
    {
        Vector2 moveVector = input.Get<Vector2>();
        forward = moveVector.y;
        steering = moveVector.x;

    }
}
