using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;        // De prefab van de granaat
    public Transform throwPoint;           // Het punt waar de granaat wordt gegooid
    public float throwForce = 10f;         // De kracht waarmee de granaat wordt gegooid
    public float throwCooldown = 1f;       // Tijd tussen het gooien van granaten

    private float lastThrowTime;           // Tijdstip van de laatste worp

    void Update()
    {
        // Controleer of de speler op de linker muisknop klikt én of de cooldown voorbij is
        if (Input.GetMouseButtonDown(0) && Time.time >= lastThrowTime + throwCooldown)
        {
            ThrowGrenade();
            lastThrowTime = Time.time; // Update de tijd van de laatste worp
        }
    }

    void ThrowGrenade()
    {
        if (grenadePrefab == null || throwPoint == null)
        {
            Debug.LogError("GrenadePrefab of ThrowPoint is niet ingesteld in de Inspector!");
            return;
        }

        // Maak een nieuwe granaat
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

        // Zorg ervoor dat de granaat niet onder de grond begint
        grenade.transform.position = new Vector3(grenade.transform.position.x, Mathf.Max(grenade.transform.position.y, 0.5f), grenade.transform.position.z);

        // Voeg een kracht toe aan de granaat
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("De granaat prefab mist een Rigidbody component!");
        }
    }
}
