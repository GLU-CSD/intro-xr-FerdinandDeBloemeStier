using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemys : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;
    public Animator e_animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
        e_animator.SetTrigger("Damage");

    }

    public void restoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
        e_animator.SetTrigger("Restore");
    }
}