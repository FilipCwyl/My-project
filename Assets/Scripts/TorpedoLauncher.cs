using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedo; // Prefabrykat torpedy
    public Transform launchPoint; // Punkt startowy torpedy

    void Start()
    {
        // Przypisz transformacj� statku jako punkt startowy torpedy
        launchPoint = transform; // Ustaw bie��c� transformacj� jako punkt startowy
    }

    public float torpedoSpeed = 10f; // Pr�dko�� torpedy
    
    void Update()
    {
        // Sprawd�, czy gracz chce wystrzeli� torped�
        if (Input.GetMouseButtonDown(0)) // Przyk�adowy klawisz (mo�esz zmieni�)
        {
            LaunchTorpedo();
        }
    }

    void LaunchTorpedo()
    {
        // Wystrzelenie torpedy
        GameObject newTorpedo = Instantiate(torpedo, launchPoint.position, launchPoint.rotation);
        Rigidbody torpedoRB = newTorpedo.GetComponent<Rigidbody>();

        // Nadaj torpedzie pr�dko�� w kierunku, w kt�rym patrzy launcher
        if (torpedoRB != null)
        {
            torpedoRB.velocity = launchPoint.forward * torpedoSpeed;
        }
    }
}