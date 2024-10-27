using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float speed = 1000f;  // Pr�dko�� torpedy
    public float lifetime = 1000f; // Czas �ycia torpedy w sekundach

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Nadaje ci�g torpedzie, aby zacz�a si� porusza�
        rb.AddForce(transform.forward * speed);
        // Zniszcz torped� po up�ywie czasu �ycia
        //Destroy(gameObject, lifetime);
    }
}