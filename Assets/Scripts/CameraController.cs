using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Obiekt do œledzenia (np. statek kosmiczny)
    public float distance = 10.0f; // Odleg³oœæ kamery od obiektu
    public float sensitivity = 2.0f; // Czu³oœæ ruchów myszy

    private float yaw = 0.0f; // K¹t obrotu wokó³ osi Y (lewo-prawo)
    private float pitch = 0.0f; // K¹t obrotu wokó³ osi X (góra-dó³)

    void Start()
    {
        // Ukryj i zablokuj kursor myszy na œrodku ekranu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Odczytaj ruchy myszy
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Zaktualizuj k¹ty obrotu
        yaw += mouseX;
        pitch -= mouseY;

        // Zapewnij p³ynne obroty kamery wokó³ obiektu
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * direction;
        transform.LookAt(target.position);
    }
}
