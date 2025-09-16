using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Vector2 movement;
    Vector2 lastDir = Vector2.down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Read input
        movement.x = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement (optional)
        if (movement.sqrMagnitude > 1f) movement.Normalize();

        // Update animator parameters (names must match exactly)
        animator.SetFloat("move_X", movement.x);
        animator.SetFloat("move_Y", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Save last non-zero direction for Idle facing
        if (movement.sqrMagnitude > 0.01f)
        {
            lastDir = movement.normalized;
            animator.SetFloat("lastMove_X", lastDir.x);
            animator.SetFloat("lastMove_Y", lastDir.y);
        }

        // Optional: flip sprite instead of having separate right/left frames
        if (movement.x != 0)
            spriteRenderer.flipX = movement.x < 0f;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

