using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    Rigidbody rb;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    float verticalLookRotation;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
    }
}
