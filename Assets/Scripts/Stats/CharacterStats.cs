using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    // Health
    public int maxHealth;
    public int currentHealth { get; private set; }
    
    public Stat damage;
    public Stat armor;
//     public Stat health2;
    public Image healthBar;
    public event System.Action<int, int> OnHealthChanged;

    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;
        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(transform.name != null && transform.name.Contains("Golem"))
            Debug.Log("<color=red>"+transform.name +"</color>+ "+" takes <color=red>" + damage + "</color> damage.");
        else
            Debug.Log("<color=cyan>"+transform.name +"</color>+ "+" takes <color=red>" + damage + "</color> damage.");

        if (healthBar !=null)
            healthBar.fillAmount = currentHealth / maxHealth;
        
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // This method is meant to be overwritten
    }

}