using UnityEngine;

public class ECSpawner : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float angle;

    public void Spawn()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            float lowestAngle = -angle / 2;
            float angleDelta = amountToSpawn > 1 ? angle / (amountToSpawn - 1) : 0;

            // Ensure that the spawned object is at z-position, so that it can be hit by the player
            var spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(toSpawn, spawnPosition, transform.rotation * Quaternion.Euler(0, 0, lowestAngle + angleDelta * i));
        }
    }
}
