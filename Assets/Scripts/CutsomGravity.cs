using Unity.VisualScripting;
using UnityEngine;

public class CutsomGravity : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField]
    float gravityScale = 1f;
    float globalgravity = -9.82f;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }
    void FixedUpdate()
    {
        Vector3 force = Physics.gravity * gravityScale;
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
