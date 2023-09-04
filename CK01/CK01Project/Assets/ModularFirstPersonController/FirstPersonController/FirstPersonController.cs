
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Transform cameraTransform;

    private float xRotation = 0f;

    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 리지드바디가 회전하지 않도록 설정
        cameraTransform = Camera.main.transform;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        // 마우스 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 플레이어 이동
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveForward = cameraTransform.forward;
        moveForward.y = 0f;
        moveDirection = (moveForward.normalized * vertical + cameraTransform.right.normalized * horizontal).normalized;

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
        moveVelocity.y = rb.velocity.y; // 리지드바디의 y축 속도 유지
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        float distanceToGround = 0.1f;
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround);
    }
}