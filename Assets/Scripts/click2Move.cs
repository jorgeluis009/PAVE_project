using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class click2Move : NetworkBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();    
        agent = GetComponent<NavMeshAgent>();    
    }


    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 100))
            {
                agent.destination = hit.point;
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

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }
}
