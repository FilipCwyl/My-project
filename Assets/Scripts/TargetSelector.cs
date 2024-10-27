using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask selectableLayer;
    private Transform selectedTarget;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Dodanie debugowania warstwy
        Debug.Log("Wartoœæ selectableLayer: " + selectableLayer.value);
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, selectableLayer))
            {
                Debug.Log("Selected obcject: " + hit.transform.name);
                selectedTarget = hit.transform;
            }
            else
            {
                Debug.Log("No obcject selected");
            }

            if (Physics.Raycast(ray, out hit, 100f, selectableLayer))
            {
                selectedTarget = hit.transform;
            }
        }
    }

    public Transform GetSelectedTarget()
    {
        if (selectedTarget != null)
        {
            Debug.Log("Returning selected target: " + selectedTarget.name);
        }
        else
        {
            Debug.Log("Brak wybranego celu do zwrócenia.");
        }

        return selectedTarget;
    }

}

