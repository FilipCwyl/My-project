using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocinanteController : MonoBehaviour
{
    public float shipMass = 130f;
    public float maxThrust = 15f;
    public float rotationSpeed = 2f;
    public float maneuveringThrust = 5f;
    public float throttleIncrement = 1f / 3f;

    private Rigidbody rb;
    private float currentThrust = 0f;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = shipMass;
    }

    void Update()
    {
        HandleUserInput();
    }

    void FixedUpdate()
    {
        ApplyPhysics();
    }

    void HandleUserInput()
    {
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Obr�t statku
        Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
        transform.Rotate(rotation);
        

        // Kierowanie si�� ci�gu epstaina
        currentThrust += Input.GetKey(KeyCode.Space) ? maxThrust * Time.deltaTime : 0f;

        // Kontrola dryfu statku
        HandleDriftInput(KeyCode.Q, Vector3.up);
        HandleDriftInput(KeyCode.X, Vector3.down);
        HandleDriftInput(KeyCode.A, Vector3.left);
        HandleDriftInput(KeyCode.D, Vector3.right);
        HandleDriftInput(KeyCode.W, Vector3.forward);
        HandleDriftInput(KeyCode.S, Vector3.back);

        
        // Nowe sterowanie
        if (Input.GetMouseButton(1))
        {
            // Obr�t statku tylko po wci�ni�ciu prawego przycisku myszy
            Vector3 rotationMouse = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
            transform.Rotate(rotationMouse);
        }
        

        // Kierowanie si�� ci�gu epstaina
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }

        if (isBoosting)
        {
           
            // Zwi�kszanie ci�gu przytrzymuj�c Shift i poruszaj�c pokr�t�em myszy do g�ry
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                currentThrust += throttleIncrement;
            }

            // Zmniejszanie ci�gu przytrzymuj�c Shift i poruszaj�c pokr�t�em myszy w d�
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                currentThrust -= throttleIncrement;
            }


        }


    }

    void HandleDriftInput(KeyCode key, Vector3 direction)
    {
        // Kontrola dryfu statku w okre�lonym kierunku
        if (Input.GetKey(key))
        {
            rb.AddForce(direction * maneuveringThrust, ForceMode.Force);
        }
    }

    void ApplyPhysics()
    {
        // Zastosowanie fizyki na podstawie wprowadzonych danych
        float thrustPercentage = Mathf.Clamp01(currentThrust / maxThrust);
        float acceleration = thrustPercentage * maxThrust / rb.mass;

        // Symulacja ruchu zgodnie z zasadami fizyki
        rb.AddRelativeForce(Vector3.forward * acceleration, ForceMode.Acceleration);
    }
}
