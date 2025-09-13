using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;          // Player movement speed
    private Rigidbody2D rb;               // Reference to Rigidbody2D
    private Vector2 movement;             // Stores WASD input
    private Vector2 mousePos;             // Stores mouse position in world

    [Header("Rotation")]
    [SerializeField] private float rotationOffset = 90f; // Adjust until gun barrel matches

    [Header("Shooting")]
    [SerializeField] private Transform firePoint; // assign FirePoint (at gun tip) in Inspector
    [SerializeField] private GameObject laserPrefab; // assign laser prefab in Inspector
    public float fireRate = 0.2f; // seconds between shots
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Get WASD input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. Get mouse position in world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 3. Shooting (hold left mouse)
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        // 4. Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // 5. Rotate whole sprite toward mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle + rotationOffset;
    }

    void Shoot()
    {
        if (firePoint == null || laserPrefab == null) return;

        // Spawn laser at firePoint
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        // Fire laser in gun direction
        Vector2 shootDir = firePoint.up; // because your sprite faces UP by default
        laser.GetComponent<Laser>().Fire(shootDir);
    }
}
