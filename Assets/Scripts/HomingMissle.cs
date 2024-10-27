using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTorpedo : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 5f;
    private Transform target;

    void Update()
    {
        if (target != null) // If there is a target assigned
        {
            // Calculate direction to target
            Vector3 direction = (target.position - transform.position).normalized;
            // Create a rotation towards the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            // Move towards the target
            transform.position += transform.forward * speed * Time.deltaTime;

            // Debugging: Log that the torpedo is moving towards the target
            Debug.Log("Torpedo is moving towards: " + target.name);
        }
        else
        {
            // Debugging: Log that there is no target assigned
            Debug.Log("No target assigned.");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("Target set: " + target.name);
    }
}

