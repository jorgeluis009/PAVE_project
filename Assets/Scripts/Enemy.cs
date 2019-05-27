using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    //private Transform target;
    private int wavepointIndex = 0;

    private float health;
    public float startHealth = GameManager.startingHealth;
    // public int worth = 50;

    // public Image healthBar;
    public event System.Action<int, int> OnHealthChanged;

    void Start()
    {
       // target = WayPoints.points[0];
        health = GameManager.startingHealth;
    }

    void Update()
    {
        //Vector3 dir = target.position - transform.position;
       // transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        //{
          //  GetNextWaypoint();
        //}
    }

   /* void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }*/

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (OnHealthChanged != null)
        {
            Debug.Log("Health: <color=green>startHealth " + startHealth + "</color>");
            Debug.Log("Health: <color=green>health " + health + "</color>");

            OnHealthChanged((int)startHealth, (int)health);
        }
        // healthBar.fillAmount = health / startHealth;
        // Debug.Log("Health: <color=green>"+ health +"</color>");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

   /* void EndPath()
    {
       // Destroy(gameObject);
    }*/
}
