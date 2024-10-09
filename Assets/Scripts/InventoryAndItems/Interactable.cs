using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool inRange = false;
    bool hasInteracted = false;
    Transform player;

    // Is meant to be overwritten
    public virtual void Interact()
    {
        //Debug.Log("Interact with " + transform.name);
        InRange(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !hasInteracted) // Interact with the item if it's within range and hasn't already interacted with
        {
            float distance = Vector2.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void InRange (Transform playerTransform) // Called when an interactable object is in range
    {
        inRange = true;
        player = playerTransform;
        hasInteracted = false;
    }

/*    public void OnDefocused()
    {
        inRange = false;
        player = null;
        hasInteracted = false;
    }*/

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
