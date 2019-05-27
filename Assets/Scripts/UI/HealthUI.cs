using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    const float stayTime = 3;
    float time = 5;
    float lastVisibleTime;
    public GameObject uiPrefab;
    public Transform target;

    Transform ui;
    Image healthSlider;
    Transform cam;

    CharacterStats stats;

    float healthPercentOld;
    float lastHealthChangeTime;

    void Start()
    {
        cam = Camera.main.transform;

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        // healthSlider = 
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void LateUpdate()
    {
        if(ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }

        if(Time.time - lastVisibleTime > time)
        {
            ui.gameObject.SetActive(false);
        }
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        Debug.Log("<color=green>CURRENT HEALTH " + currentHealth + "</color>");
        Debug.Log("<color=green>MAX HEALTH " + maxHealth + "</color>");
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastVisibleTime = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth < 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    float GetHealthPercent()
    {
        return Mathf.Clamp01(stats.currentHealth / (float)GameManager.startingHealth);
    }
}