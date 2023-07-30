#nullable enable

using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float droneSpeed = 2f;

    [SerializeField]
    private float followDistance;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private LineRenderer lineRenderer;

    private GameObject? objectToFollow;

    private GameObject ObjectToFollow => objectToFollow != null ? objectToFollow : throw new ObjectNotInitializedException();

    public void Initialize(GameObject objectToFollow)
    {
        transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        this.objectToFollow = objectToFollow;
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, ObjectToFollow.transform.position);
        var distance = Vector2.Distance(transform.position, ObjectToFollow.transform.position);
        if (distance > followDistance)
        {
            Vector3 direction = ObjectToFollow.transform.position - transform.position;
            rb.AddForce(direction.normalized * droneSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        if (objectToFollow == null)
        {
            return;
        }

        Vector2 target = ObjectToFollow.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = target - currentPosition;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction.normalized * followDistance);
    }

    public void TakeDamage(float minorHealth)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, minorHealth);
    }
}
