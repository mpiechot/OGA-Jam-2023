using UnityEngine;

public class ECRotator : MonoBehaviour
{
    [SerializeField]
    private float minRotationSpeedRange;

    [SerializeField]
    private float maxRotationSpeedRange;

    private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = UnityEngine.Random.Range(minRotationSpeedRange, maxRotationSpeedRange);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
