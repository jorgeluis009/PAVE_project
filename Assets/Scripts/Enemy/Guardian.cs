using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guardian : MonoBehaviour
{
    public float range = 9f;
    public float AreaToAttack = 2.5f;

    NavMeshAgent agent;
    Transform target;
    // public GameObject player;
    public string enemyTag = "Enemy";
    public Animator animator;
    CharacterCombat combat;
    public enum GolemState { idle, chasing, attack };
    public GolemState state = GolemState.idle;
    GameObject aux;
    void Start()
    {
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        StartCoroutine(UseBrain());
        // GameObject enemy = GameObject.FindGameObjectWithTag(enemyTag);
        // target = enemy.transform;
        agent = GetComponent<NavMeshAgent>();
        //  InvokeRepeating("setTarget", 0f, 0.5f);

    }

    void setTarget()
    {
        // target = PlayerManager.instance.player.transform;
        
    }

    IEnumerator UseBrain()
    {
        while (true)
        {
            aux = GameObject.FindGameObjectWithTag(enemyTag);
            if (aux != null)
            {
                target = aux.transform;

                float dist = Vector3.Distance(target.position, transform.position);

                switch (state)
                {
                    case GolemState.idle:
                        if (dist < range && animator != null)
                        {
                            animator.SetBool("isChasing", true);
                            state = GolemState.chasing;
                        }

                        agent.SetDestination(transform.position);
                        break;

                    case GolemState.chasing:
                        if (dist > range && animator != null)
                        {
                            animator.SetBool("isChasing", false);
                            state = GolemState.idle;
                        }
                        else if (dist < AreaToAttack && animator != null)
                        {
                            state = GolemState.attack;
                            animator.SetBool("isOnAtk", true);
                        }
                        agent.SetDestination(target.position);
                        break;

                    case GolemState.attack:
                        yield return new WaitForSeconds(0.8f);

                        /*   if (healthScript.health > 0)
                           {
                               healthScript.health -= dmg;
                           }
                           if (healthScript.health < 0)
                           {
                               FindObjectOfType<ManagerScript>().GameOver();
                           }
                           */
                        CharacterStats targetStats = target.GetComponent<CharacterStats>();
                        if (targetStats != null)
                        {
                            combat.Attack(targetStats);
                        }
                        // Debug.Log("DISTANCIA = "+ dist);
                        FaceTarget();
                        if (dist > AreaToAttack && dist < range && animator != null)
                        {
                            state = GolemState.chasing;
                            animator.SetBool("isChasing", true);
                            animator.SetBool("isOnAtk", false);
                        }
                        else if (dist < AreaToAttack && animator != null)
                        {
                            state = GolemState.attack;
                            animator.SetBool("isChasing", false);
                            animator.SetBool("isOnAtk", true);
                        }
                        agent.SetDestination(transform.position);
                        break;

                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Update()
    {
      /*  float distance = 0;
        if(target != null)
            distance = Vector3.Distance(target.position, transform.position);

        // Debug.Log(distance);

        if (distance <= range && target != null)
        {
            animator.SetBool("isChasing", true);
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {

                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                FaceTarget();
            }
        }
        animator.SetBool("isChasing", false);
        */

    }

    public void Attack()
    {
    }

    void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
