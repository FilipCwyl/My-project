using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Obiekt do �ledzenia (np. statek kosmiczny)
    public float distance = 10.0f; // Odleg�o�� kamery od obiektu
    public float sensitivity = 2.0f; // Czu�o�� ruch�w myszy

    private float yaw = 0.0f; // K�t obrotu wok� osi Y (lewo-prawo)
    private float pitch = 0.0f; // K�t obrotu wok� osi X (g�ra-d�)

    void Start()
    {
        // Ukryj i zablokuj kursor myszy na �rodku ekranu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Odczytaj ruchy myszy
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Zaktualizuj k�ty obrotu
        yaw += mouseX;
        pitch -= mouseY;

        // Zapewnij p�ynne obroty kamery wok� obiektu
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * direction;
        transform.LookAt(target.position);
    }
}
