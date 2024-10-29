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
    public Transform cameraTransform; 
    public TMP_Text speedometerText; 
    public TMP_Text accelerationText; 
    private Rigidbody rb;
    private float currentThrust = 0f;
    private bool isBoosting = false;
    private Vector3 lastVelocity; // To calculate acceleration

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = shipMass;
        lastVelocity = rb.velocity; 
    }

    void Update()
    {
        HandleUserInput();

        // Speedometer km/h
        float speedInKmH = rb.velocity.magnitude * 3.6f;
        speedometerText.text = "Speed: " + speedInKmH.ToString("F2") + " km/h";

        // Acceleration in Gs
        Vector3 currentVelocity = rb.velocity;
        float accelerationInG = (currentVelocity - lastVelocity).magnitude / (9.81f * Time.deltaTime);
        lastVelocity = currentVelocity;
        accelerationText.text = "Acceleration: " + accelerationInG.ToString("F2") + " G";
    }

    void FixedUpdate()
    {
        ApplyPhysics();
    
             Debug.Log("Acelleration force: " + currentThrust);

        if (isBoosting)
        {
            // Ustawianie aktualnej siły ciągu
            rb.AddForce(transform.forward * currentThrust * Time.deltaTime, ForceMode.Acceleration);
            Debug.Log("Acceleration force: " + currentThrust);
        }
        else
        {
            Debug.Log("Silnik wyłączony, siła ciągu wynosi 0");
        }
    }

    void HandleUserInput()
    {
        //rotating of ship
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
            transform.Rotate(rotation);
        }

        // Main Drive Control
        currentThrust += Input.GetKey(KeyCode.Space) ? maxThrust * Time.deltaTime : 0f;

        // Testing of Main Drive
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = true;
            Debug.Log("Main Drive is on.");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
            Debug.Log("Main Drive is off.");
        }

        // Ship drift contol
        HandleDriftInput(KeyCode.Q, cameraTransform.up);
        HandleDriftInput(KeyCode.E, -cameraTransform.up); 
        HandleDriftInput(KeyCode.A, -cameraTransform.right);
        HandleDriftInput(KeyCode.D, cameraTransform.right);
        HandleDriftInput(KeyCode.W, cameraTransform.forward);
        HandleDriftInput(KeyCode.S, -cameraTransform.forward);

        // Rotation of ship 
        if (Input.GetKeyDown(KeyCode.Keypad0)) // 0 Button on Num keyboard
        {
            //Right
            rb.AddTorque(Vector3.up * rotationSpeed, ForceMode.VelocityChange);
        }

        else if (Input.GetKeyDown(KeyCode.KeypadPeriod)) // . Button on Num keyboard
        {
            // Left
            rb.AddTorque(Vector3.up * -rotationSpeed, ForceMode.VelocityChange);
        }


        // Main Drive Thrust Control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = !isBoosting;
            Debug.Log("Silnik aktywowany: " + isBoosting);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }

        if (isBoosting)
        {
            // Adjust thrust with continuous keys for smoother increase/decrease
            if (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.Plus))
            {
                currentThrust += throttleIncrement * Time.deltaTime; // smooth adjustment
                Debug.Log("Increasing thrust: " + currentThrust);
            }

            else if (Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.Minus))
            {
                currentThrust -= throttleIncrement * Time.deltaTime;
                Debug.Log("Decreasing thrust: " + currentThrust);
            }

            // Clamp thrust
            currentThrust = Mathf.Clamp(currentThrust, 0, maxThrust);

            // Apply force only when boosting is active
            rb.AddForce(transform.forward * currentThrust, ForceMode.Force);
        }

        else
        {
            // Stop applying force when not boosting
            currentThrust = 0;
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

