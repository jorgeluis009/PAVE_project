using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool freeOne = false;
    public static bool freeTwo = false;
    public static bool freeThree = false;
    public static bool freeFour = false;
    public static bool freeFive = false;
    public static bool freeSix = false;
    public static float startingHealth = 300;
    public Text instructions;
    public Text instructions2;
    public static Animator anim;
    public static Animator anim2;

    public void Start()
    {
        instructions = GameObject.FindGameObjectWithTag("InsText").GetComponent<Text>();
        instructions2 = GameObject.FindGameObjectWithTag("InsText2").GetComponent<Text>();
        anim = instructions.GetComponent<Animator>();
        anim2 = instructions2.GetComponent<Animator>();
        StartCoroutine(textTimer());
    }

    IEnumerator textTimer()
    {
        yield return new WaitForSeconds(6f);
        anim.SetTrigger("fadeOut");
        // instructions.gameObject.SetActive(false);

    }
}
