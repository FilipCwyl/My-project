using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RocinanteController : MonoBehaviour
{
    public float shipMass = 130f;
    public float maxThrust = 15f;
    public float rotationSpeed = 2f;
    public float maneuveringThrust = 5f;
    public float throttleIncrement = 1f / 3f;
    public Transform cameraTransform; // Referencja do transformacji kamery
    public TMP_Text speedometerText; // Referencja do tekstu prędkościomierza
    public TMP_Text accelerationText; // Referencja do tekstu akcelerometru

    private Rigidbody rb;
    private float currentThrust = 0f;
    private bool isBoosting = false;
    private Vector3 lastVelocity; // Do obliczenia akceleracji

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = shipMass;
        lastVelocity = rb.velocity; // Inicjalizacja prędkości
    }

    void Update()
    {
        HandleUserInput();

        // Oblicz prędkość w km/h
        float speedInKmH = rb.velocity.magnitude * 3.6f;
        speedometerText.text = "Speed: " + speedInKmH.ToString("F2") + " km/h";

        // Oblicz przyspieszenie w G
        Vector3 currentVelocity = rb.velocity;
        float accelerationInG = (currentVelocity - lastVelocity).magnitude / (9.81f * Time.deltaTime);
        lastVelocity = currentVelocity;
        accelerationText.text = "Acceleration: " + accelerationInG.ToString("F2") + " G";
    }

    void FixedUpdate()
    {
        ApplyPhysics();
    

        // Debugowanie aktualnej wartości ciągu
        Debug.Log("Aktualna siła ciągu: " + currentThrust);
    }

    void HandleUserInput()
    {
        // Obrót statku za pomocą myszy, tylko jeśli lewy przycisk myszy jest wciśnięty
        if (Input.GetMouseButton(1)) // Prawy przycisk myszy
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
            transform.Rotate(rotation);
        }

        // Kierowanie siłą ciągu Epsteina
        currentThrust += Input.GetKey(KeyCode.Space) ? maxThrust * Time.deltaTime : 0f;

        // Sprawdzenie czy silnik Epsteina jest włączony
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
            Debug.Log("Silnik Epsteina włączony.");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
            Debug.Log("Silnik Epsteina wyłączony.");
        }

        // Kontrola dryfu statku względem kierunku kamery
        HandleDriftInput(KeyCode.Q, cameraTransform.up);
        HandleDriftInput(KeyCode.E, -cameraTransform.up); // Zmiana z X na E
        HandleDriftInput(KeyCode.A, -cameraTransform.right);
        HandleDriftInput(KeyCode.D, cameraTransform.right);
        HandleDriftInput(KeyCode.W, cameraTransform.forward);
        HandleDriftInput(KeyCode.S, -cameraTransform.forward);

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

        // Kierowanie siłą ciągu Epsteina
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
            // Zwiększanie ciągu przytrzymując Shift i poruszając pokrętłem myszy do góry
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) // Przycisk + na klawiaturze numerycznej
            {
                currentThrust += throttleIncrement;
                Debug.Log("Zwiększono ciąg: " + currentThrust);
            }

            // Zmniejszanie ciągu przytrzymując Shift i poruszając pokrętłem myszy w dół
            else if (Input.GetKeyDown(KeyCode.KeypadMinus)) // Przycisk - na klawiaturze numerycznej
            {
                currentThrust -= throttleIncrement;
                Debug.Log("Zmniejszono ciąg: " + currentThrust);
            }

            // Ograniczenie ciągu do maksymalnej wartości
            currentThrust = Mathf.Clamp(currentThrust, 0, maxThrust);

        }
    }

    void HandleDriftInput(KeyCode key, Vector3 direction)
    {
        // Kontrola dryfu statku w określonym kierunku
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
        rb.AddRelativeForce(-Vector3.forward * acceleration, ForceMode.Acceleration);
    }

    void UpdateUI()
    {
        // Speed
        float speed = rb.velocity.magnitude;
        speedometerText.text = "Speed: " + speed.ToString("F2") + " m/s";

        // Akceleracja ()
        Vector3 acceleration = (rb.velocity - lastVelocity) / Time.deltaTime;
        accelerationText.text = "Acceleration: " + acceleration.magnitude.ToString("F2") + " m/s²";

        lastVelocity = rb.velocity; // Actualization of speed
    }
}

