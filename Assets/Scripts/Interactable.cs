using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour
{

    public float radius = 3f;
    public Transform interactionTransform;
    
    bool isFocus = false;  
    Transform player;     

    bool hasInteracted = false;

    void Update()
    {
        if (isFocus)    // If currently being focused
        {
            // Debug.Log("focused");
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            // If we haven't already interacted and the player is close enough
            if (!hasInteracted && distance <= radius)
            {
                // Interact with the object
                hasInteracted = true;
                Interact();
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        // Debug.Log(playerTransform);
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    public virtual void Interact()
    {
        // Debug.Log("INTERACTING");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}