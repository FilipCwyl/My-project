using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyMarkerController : MonoBehaviour
{
    public Transform enemy;  
    public Vector3 offset = new Vector3(0, 2, 0);  
    private RectTransform markerTransform;

    void Start()
    {
        markerTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (enemy != null)
        {
            markerTransform.position = Camera.main.WorldToScreenPoint(enemy.position + offset);
        }
    }
}
