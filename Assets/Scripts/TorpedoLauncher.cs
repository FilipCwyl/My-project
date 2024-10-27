using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedoPrefab;
    public Transform launchPoint;
    public TargetSelector targetSelector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            LaunchTorpedo();
        }
    }

    void LaunchTorpedo()
    {
        if (torpedoPrefab == null)
        {
            Debug.LogError("Torpedo prefab is not assigned!");
            return;
        }

        GameObject torpedo = Instantiate(torpedoPrefab, launchPoint.position, launchPoint.rotation);

        torpedo.transform.Rotate(90f, 90f, 0f);  // Torpedo prefab rotation in transform, Adjust values based on your needs

        HomingTorpedo homingTorpedo = torpedo.GetComponent<HomingTorpedo>();
        if (homingTorpedo == null)
        {
            Debug.LogError("HomingTorpedo script is missing from the torpedoPrefab!");
            return;
        }

        if (targetSelector == null)
        {
            Debug.LogError("TargetSelector is not assigned in the TorpedoLauncher!");
            return;
        }

        Transform target = targetSelector.GetSelectedTarget();
        if (target != null)
        {
            homingTorpedo.SetTarget(target);
            Debug.Log("Target selected: " + target.name);
        }
        else
        {
            Debug.LogWarning("No target selected.");
        }
    }


}