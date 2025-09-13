using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f; // Laser speed
    public float lifeTime = 2f; // Destroy after this time

    private Rigidbody2D rb;

    void Awake()
    {
        // Cache the Rigidbody2D once
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Destroy automatically after some time
        Destroy(gameObject, lifeTime);
    }

    // Called by PlayerController
    public void Fire(Vector2 direction)
    {
        if (rb == null)
        {
            Debug.LogError("Laser has no Rigidbody2D attached!");
            return;
        }

        rb.linearVelocity = direction.normalized * speed;
    }
}
