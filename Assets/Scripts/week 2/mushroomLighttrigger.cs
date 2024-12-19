using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mushroomLighttrigger : MonoBehaviour
{
    [SerializeField] private Light mushroomLight;

    private void Start()
    {
        mushroomLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Geef de XR Rig de tag "Player"
        {
            mushroomLight.enabled = true;
            Debug.Log("Licht aan");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mushroomLight.enabled = false;
            Debug.Log("Licht uit");
        }
    }
}
