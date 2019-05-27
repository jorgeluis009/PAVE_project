using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    // Use this for initialization
    void Start()
    {
        // EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }


    public override void Die()
    {
        base.Die();
        // PlayerManager.instance.KillPlayer();
    }
}