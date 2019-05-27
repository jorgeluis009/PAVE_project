﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackRate = 1f;
    private float attackCountdown = 0f;

    public Animator animator;
    public event System.Action OnAttack;

    public Transform healthBarPos;
    // public float
    CharacterStats myStats;
    CharacterStats enemyStats;


    void Start()
    {
        animator = GetComponent<Animator>();

        myStats = GetComponent<CharacterStats>();
        //  HealthUIManager.instance.Create(healthBarPos, myStats);
    }

    void Update()
    {
        attackCountdown -= Time.deltaTime;
    }

    public void Attack(CharacterStats enemyStats)
    {
        //animator.SetBool("isOnAtk", true);

        if (attackCountdown <= 0f)
        {
            this.enemyStats = enemyStats;
            attackCountdown = 1f / attackRate;

            StartCoroutine(DoDamage(enemyStats, .6f));

            if (OnAttack != null)
            {
                OnAttack();
            }
        }
    }


    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        // print("Start");
        yield return new WaitForSeconds(delay);

        Debug.Log(transform.name + " swings for " + myStats.damage.GetValue() + " damage");
        enemyStats.TakeDamage(myStats.damage.GetValue());
    }


}