using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Tooltip("Ref. to the RigidBody")]
    private Rigidbody2D rigidbody2;

    [SerializeField, Tooltip("Thrust of the movement")]
    private float thrust = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidbody2.AddForce(Vector3.up * thrust, ForceMode2D.Impulse);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            rigidbody2.AddForce(Vector3.down * thrust, ForceMode2D.Impulse);
        }
    }
}
