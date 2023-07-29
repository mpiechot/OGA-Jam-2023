using System.Collections;
using UnityEngine;

public class MovementKI : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of the movement")]
    private float movementSpeed = 1.0f;

    [SerializeField, Tooltip("Acceleration of the movement")]
    private float acceleration = 1.0f;

    [SerializeField, Tooltip("Drag of the movement")]
    private float drag = 1.0f;

    [SerializeField, Tooltip("Maximum speed of the movement")]
    private float maxSpeed = 5.0f;

    [SerializeField, Tooltip("Upper border")]
    private float upperBorder = 0;

    [SerializeField, Tooltip("Lower border")]
    private float lowerBorder = 0;

    [SerializeField, Tooltip("Left border")]
    private float leftBorder = 0;

    [SerializeField, Tooltip("Right border")]
    private float rightBorder = 0;

    private float xVelocity = 0;
    private float yVelocity = 0;
    private bool canMove = true;

    private Coroutine stopRoutine;

    // Start is called before the first frame update
    void Start()
    {
        Mailbox.AddSubscriber<HeatLimitReachedMail>(OnHeatLimitReached);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float xChange = Input.GetAxisRaw("Horizontal");
        float yChange = Input.GetAxisRaw("Vertical");

        float xAccelerationAmount = xChange * acceleration * Time.deltaTime;
        xVelocity += xAccelerationAmount;
        xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed);

        float yAccelerationAmount = yChange * acceleration * Time.deltaTime;
        yVelocity += yAccelerationAmount;
        yVelocity = Mathf.Clamp(yVelocity, -maxSpeed, maxSpeed);

        float xDragAmount = drag * Time.deltaTime * Mathf.Sign(xVelocity);
        xVelocity -= xDragAmount;

        float yDragAmount = drag * Time.deltaTime * Mathf.Sign(yVelocity);
        yVelocity -= yDragAmount;

        float newX = transform.position.x + (movementSpeed * Time.deltaTime * xVelocity);
        newX = Mathf.Clamp(newX, leftBorder, rightBorder);

        float newY = transform.position.y + (movementSpeed * Time.deltaTime * yVelocity);
        newY = Mathf.Clamp(newY, lowerBorder, upperBorder);

        if (newX == leftBorder || newX == rightBorder)
        {
            xVelocity = 0;
        }

        if (newY == lowerBorder || newY == upperBorder)
        {
            yVelocity = 0;
        }

        transform.position = new Vector3(newX, newY, 0);
    }


    private void OnHeatLimitReached(HeatLimitReachedMail mail)
    {
        canMove = false;
        if(stopRoutine != null) StopCoroutine(stopRoutine);
        stopRoutine = StartCoroutine(EnableMovementAfterDelay(5.0f));
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
        Mailbox.InvokeSubscribers(new HeatDecreaseMail(){decreaseAmount = 100});
        stopRoutine = null;
    }

}