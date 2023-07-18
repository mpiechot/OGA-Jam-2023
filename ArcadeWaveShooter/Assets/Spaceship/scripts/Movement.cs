using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField, Tooltip("Speed of the movement")]
    private float movementSpeed = 1.0f;

    [SerializeField, Tooltip("Upper border")]
    private float upperBorder = 0;

    [SerializeField, Tooltip("Lower border")]
    private float lowerBorder = 0;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float yChange = 0;

        if (Input.GetKey(KeyCode.W))
        {
            yChange = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yChange = -1;
        }

        float newY = transform.position.y + (movementSpeed * Time.deltaTime * yChange);
        newY = Mathf.Clamp(newY, lowerBorder, upperBorder);

        transform.position = new Vector3(transform.position.x, newY, 0);
    }

}