using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianManager : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    GameObject aux;
    public float range = 9f;
    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        // Debug.Log("Interact method");

        base.Interact();
        aux = GameObject.FindGameObjectWithTag("Enemy");
        // float dist = Vector3.Distance(target.position, transform.position);

        if (aux != null)
        {
            CharacterCombat playerCombat = aux.GetComponent<CharacterCombat>();
            // CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
            if (playerCombat != null)
            {
                playerCombat.Attack(myStats);
            }
        }
    }
}
