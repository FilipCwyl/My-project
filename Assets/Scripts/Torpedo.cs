using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float speed = 1000f;  
    public float lifetime = 1000f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.AddForce(transform.forward * speed);
        // Destroing of torpedo timesetting - turn on back when need it
        //Destroy(gameObject, lifetime);
    }
}