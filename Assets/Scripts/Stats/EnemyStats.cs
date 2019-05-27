using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Animator anim;
    public int golemID;
    public override void Die()
    {
        base.Die();
        anim.SetTrigger("isDead");
        StartCoroutine(DieFun());
        
    }

    IEnumerator DieFun()
    {
        yield return new WaitForSeconds(1.4f);
        switch (golemID)
        {
            case 1:
                GameManager.freeOne = true;
                GameManager.anim2.SetTrigger("warningFlag");
                Debug.Log("<color=cyan>TORRE UNO DESACTIVADA</color>");
                break;
            case 2:
                GameManager.freeTwo = true;
                Debug.Log("<color=cyan>TORRE DOS DESACTIVADA</color>");
                break;
            case 3:
                GameManager.freeThree = true;
                break;
            case 4:
                GameManager.freeFour = true;
                break;
            case 5:
                GameManager.freeFive = true;
                break;
            case 6:
                GameManager.freeSix = true;
                break;
            default:
                break;
        }
        Destroy(gameObject);
        yield return new WaitForSeconds(3f);
        GameManager.anim2.SetTrigger("warningOut");

    }

}