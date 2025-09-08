using UnityEngine;
using System.Collections;

public class ShipAnimatorController : MonoBehaviour
{
    public float flightDuration = 3f;   // time before blast
    public float floatDuration = 2f;    // time ship drifts after blast
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found on Ship!");
        }

        // Start the sequence
        Invoke("TriggerBlast", flightDuration);
    }

    void TriggerBlast()
    {
        anim.SetTrigger("Blast"); // switch from jet to ShipBlast
        StartCoroutine(HandleAfterBlast());
    }

    IEnumerator HandleAfterBlast()
    {
        // Wait for the blast animation to finish
        float blastLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(blastLength);

        // Let ship float damaged for some time
        yield return new WaitForSeconds(floatDuration);

        // Trigger camera zoom (script must be on the Main Camera)
        CameraZoomOut camZoom = Camera.main.GetComponent<CameraZoomOut>();
        if (camZoom != null)
        {
            camZoom.StartZoom();
        }
    }
}
