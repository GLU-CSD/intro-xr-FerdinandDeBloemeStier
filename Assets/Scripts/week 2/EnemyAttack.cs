using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 10f;       // Schade per aanval
    public float attackCooldown = 2f;      // Tijd tussen aanvallen
    private float lastAttackTime;          // Tijd sinds laatste aanval

    private healthbarBase baseHealth;       // Referentie Health script base

    void Update()
    {
        if (baseHealth != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log($"{this.name} is attacking the base!");
            baseHealth.TakeDamage(damageAmount);
            lastAttackTime = Time.time;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            Debug.Log(this.name + " collided with the base!");
            baseHealth = collision.gameObject.GetComponent<healthbarBase>();

            if (baseHealth == null)
            {
                Debug.LogError("No healthbarBase script found on the base!");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            Debug.Log(this.name + " exited collision with the base.");
            baseHealth = null;
        }
    }
}
