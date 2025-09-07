using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // Destroy anything that exits the boundary
        Destroy(other.gameObject);
    }
}
