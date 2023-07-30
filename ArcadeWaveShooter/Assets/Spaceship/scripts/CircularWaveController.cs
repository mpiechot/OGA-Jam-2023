using UnityEngine;

public class CircularWaveController : MonoBehaviour
{
    public Material waveMaterial;
    
    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (sphereCollider.enabled)
        {
            // Get the screen position of the transform
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            // Convert the screen position to UV coordinates
            Vector2 uvPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

            // Scale the UV coordinates to be between 0 and 1
            uvPos.x = Mathf.Clamp01(uvPos.x);
            uvPos.y = Mathf.Clamp01(uvPos.y);

            // Set the center and radius values in the wave material
            waveMaterial.SetVector("_Center", uvPos);
            waveMaterial.SetFloat("_WaveRadius", sphereCollider.radius / 10f);
        }
        else
            waveMaterial.SetFloat("_WaveRadius", 0);

    }
}