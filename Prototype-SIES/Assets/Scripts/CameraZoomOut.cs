using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public float targetSize = 10f;   // how far to zoom out
    public float zoomSpeed = 2f;     // speed of zoom
    private Camera cam;
    private bool zooming = false;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (zooming)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);

            // Stop when close enough
            if (Mathf.Abs(cam.orthographicSize - targetSize) < 0.05f)
            {
                zooming = false;

                // Trigger fade once zoom finishes
                FadeOut fade = FindObjectOfType<FadeOut>();
                if (fade != null)
                {
                    fade.StartFade();
                }
            }
        }
    }

    public void StartZoom()
    {
        zooming = true;

        // Stop spawning new asteroids
        AsteroidSpawner spawner = FindObjectOfType<AsteroidSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
        }

        // Stop existing asteroid movement
        Rigidbody2D[] allAsteroids = FindObjectsOfType<Rigidbody2D>();
        foreach (var asteroid in allAsteroids)
        {
            asteroid.linearVelocity = Vector2.zero;
            asteroid.angularVelocity = 0f;
        }

        // Stop background scrolling
        BGLoop[] backgrounds = FindObjectsOfType<BGLoop>();
        foreach (var bg in backgrounds)
        {
            bg.StopScrolling();
        }
    }
}
