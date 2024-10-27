using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedo; // Prefabrykat torpedy
    public Transform launchPoint; // Punkt startowy torpedy

    void Start()
    {
        // Przypisz transformacjê statku jako punkt startowy torpedy
        launchPoint = transform; // Ustaw bie¿¹c¹ transformacjê jako punkt startowy
    }

    public float torpedoSpeed = 10f; // Prêdkoœæ torpedy
    
    void Update()
    {
        // SprawdŸ, czy gracz chce wystrzeliæ torpedê
        if (Input.GetMouseButtonDown(0)) // Przyk³adowy klawisz (mo¿esz zmieniæ)
        {
            LaunchTorpedo();
        }
    }

    void LaunchTorpedo()
    {
        // Wystrzelenie torpedy
        GameObject newTorpedo = Instantiate(torpedo, launchPoint.position, launchPoint.rotation);
        Rigidbody torpedoRB = newTorpedo.GetComponent<Rigidbody>();

        // Nadaj torpedzie prêdkoœæ w kierunku, w którym patrzy launcher
        if (torpedoRB != null)
        {
            torpedoRB.velocity = launchPoint.forward * torpedoSpeed;
        }
    }
}