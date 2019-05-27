using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class click2Move : NetworkBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public Interactable focus;  // Our current focus: Item, Enemy etc.
    PlayerMotor motor;

    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();    
        agent = GetComponent<NavMeshAgent>();
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // agent.destination = hit.point; // move to point
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // We create a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if (interactable != null)
                {
                    // Debug.Log("Interactable clicked");
                    SetFocus(interactable);
                }
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        // animator.SetBool("isMoving", isMoving);
        animator.SetBool("isWalking", isMoving);
    }

    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
               // focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            motor.FollowTarget(newFocus);   // Follow the new focus
        }
        
       newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
       // if (focus != null)
          //  focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }

    public override void OnStartLocalPlayer()
    {
        GameManager.anim.SetTrigger("fadeIn");
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }
}
