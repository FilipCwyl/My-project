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

        // Obrót statku
        Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
        transform.Rotate(rotation); // Obrót statku przy u¿yciu myszy

        // Kierowanie si³¹ ci¹gu Epsteina
        currentThrust += Input.GetKey(KeyCode.Space) ? maxThrust * Time.deltaTime : 0f;

        // Kontrola dryfu statku
        HandleDriftInput(KeyCode.Q, Vector3.up);
        HandleDriftInput(KeyCode.X, Vector3.down);
        HandleDriftInput(KeyCode.A, Vector3.left);
        HandleDriftInput(KeyCode.D, Vector3.right);
        HandleDriftInput(KeyCode.W, Vector3.forward);
        HandleDriftInput(KeyCode.S, Vector3.back);

        // Nowe sterowanie obrotem statku
        if (Input.GetKeyDown(KeyCode.Keypad0)) // Przycisk 0 na klawiaturze numerycznej
        {
            // Obrót statku w prawo
            rb.AddTorque(Vector3.up * rotationSpeed, ForceMode.VelocityChange);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPeriod)) // Przycisk . na klawiaturze numerycznej
        {
            // Obrót statku w lewo
            rb.AddTorque(Vector3.up * -rotationSpeed, ForceMode.VelocityChange);
        }

        // Kierowanie si³¹ ci¹gu Epsteina
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
            // Zwiêkszanie ci¹gu przytrzymuj¹c Shift i poruszaj¹c pokrêt³em myszy do góry
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) // Przycisk + na klawiaturze numerycznej
            {
                currentThrust += throttleIncrement;
            }

            // Zmniejszanie ci¹gu przytrzymuj¹c Shift i poruszaj¹c pokrêt³em myszy w dó³
            else if (Input.GetKeyDown(KeyCode.KeypadMinus)) // Przycisk - na klawiaturze numerycznej
            {
                currentThrust -= throttleIncrement;
            }
        }
    }

    void HandleDriftInput(KeyCode key, Vector3 direction)
    {
        // Kontrola dryfu statku w okreœlonym kierunku
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
