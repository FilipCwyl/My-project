using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float speed = 1000f;  // Prêdkoœæ torpedy
    public float lifetime = 1000f; // Czas ¿ycia torpedy w sekundach

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Nadaje ci¹g torpedzie, aby zaczê³a siê poruszaæ
        rb.AddForce(transform.forward * speed);
        // Zniszcz torpedê po up³ywie czasu ¿ycia
        //Destroy(gameObject, lifetime);
    }
}