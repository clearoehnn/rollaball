using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float moveSpeed;
    private bool onWater = false;
    public GameObject miniMapCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        miniMapCamera.transform.position = new Vector3(transform.position.x, 150, transform.position.z);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * moveSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            onWater = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            onWater = false;
        }
    }
}
