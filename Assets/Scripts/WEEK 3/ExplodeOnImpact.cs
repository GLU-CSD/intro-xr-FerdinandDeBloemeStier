using UnityEngine;
using UnityEngine.AI;

public class ExplodeOnImpact : MonoBehaviour
{
    public float explosionForce = 500f;      // Kracht van de explosie
    public float explosionRadius = 5f;      // Radius van de explosie
    public float explosionDelay = 5f;       // Tijd tot de explosie
    public GameObject explosionEffect;      // Particle effect prefab voor de explosie
    public ParticleSystem explosion;
    public float explosionDamage = 20f;

    void Start()
    {
        // Start de timer voor de explosie
        Invoke(nameof(Explode), explosionDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Zorg dat vijanden de tag "Enemy" hebben
        {
            Explode();
        }
    }

    void Explode()
    {
        // Toon het particle effect voor de explosie
        if (explosionEffect != null)
        {
            // Maak een instantie van het particle effect
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();

            // Zorg ervoor dat het particle effect zichzelf verwijdert na de levensduur
            Destroy(effect, 3f); // Pas de waarde aan op basis van de levensduur van je particle effect
        }
        else
        {
            Debug.LogWarning("ExplosionEffect prefab is niet ingesteld in de Inspector!");
        }

        // Vind alle objecten in de buurt van de explosie
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            NavMeshAgent agent = nearbyObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }

            HealthBarEnemys healthScript = nearbyObject.GetComponent<HealthBarEnemys>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(explosionDamage);
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            }
        }

        // Verwijder de granaat
        Destroy(gameObject);
    }
}
