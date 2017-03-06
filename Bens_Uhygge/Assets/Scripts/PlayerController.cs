using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;
    public int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    void FixedUpdate()
    {
        // Get input for movement and set speed
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // Note that movement is normalized to prevent accumulating speed on multiple key presses. This might interfere with joystick input.
        rb.velocity = movement.normalized * speed;

        // Adds rotation to make object face direction of travel
        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Makes sure player only picks up gameObjects tagged as "Pick Up"
        if (other.gameObject.CompareTag("Pick Up")) 
        {
            // Deaktivates the "Pick Up" object, rather than destroying it.
            other.gameObject.SetActive(false);
            count = count + 1;
        }

        //unlock door
        if (count >= 1 && other.gameObject.CompareTag("Door"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
        }
    }
}