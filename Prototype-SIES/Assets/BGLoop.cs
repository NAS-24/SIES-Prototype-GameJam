using UnityEngine;

public class BGLoop : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float spriteHeight;

    // Small overlap to avoid gaps (tweak if needed)
    private float overlapFix = 0.01f;

    void Start()
    {
        // Get the full height of the sprite in world units
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteHeight = sr.bounds.size.y;
    }

    void Update()
    {
        // Move downward
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // If this BG moved completely off-screen
        if (transform.position.y <= -spriteHeight)
        {
            // Jump above the other background with a tiny overlap
            transform.position += new Vector3(0, (spriteHeight * 2f) - overlapFix, 0);
        }
    }
}
