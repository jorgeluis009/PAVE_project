using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float dist { get; set; }
    public float range = 15f;
    public string enemyTag = "Enemy";
    public int turretID;

    private GameObject aux;
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy<5)
                Debug.Log("<color=red>"+distanceToEnemy+"</color>");
            dist = distanceToEnemy;
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        if (fireCountdown <= 0f)
        {
            if (isAvailable())
                Shoot();
            else
            {
                Debug.Log("<color=red> TORRE " + turretID + " DESACTIVADA </color>");
                GameManager.anim.SetTrigger("warningFlag");
            }

            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    bool isAvailable()
    {
        switch (turretID)
        {
            case 1:
                if (!GameManager.freeOne)
                    return true;
                return false;
            case 2:
                if (!GameManager.freeTwo)
                    return true;
                return false;
            case 3:
                if (!GameManager.freeThree)
                    return true;
                return false;
            case 4:
                if (!GameManager.freeFour)
                    return true;
                return false;
            case 5:
                if (!GameManager.freeFive)
                    return true;
                return false;
            case 6:
                if (!GameManager.freeSix)
                    return true;
                return false;
            default:
                return false;
        }
    }
    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}



