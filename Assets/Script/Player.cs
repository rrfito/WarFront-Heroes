using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public AudioSource footstepAudio;

    public float speed = 6f;
    public float crouchSpeed = 3f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Animator animator;
    private bool isCrouching = false;
    public bool canMove = true;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        footstepAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!canMove) return;

        // Periksa apakah pemain berada di tanah
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset kecepatan vertikal saat pemain berada di tanah
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = direction.magnitude >= 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            animator.SetBool("isCrouch", isCrouching);
        }

        float currentSpeed = isCrouching ? crouchSpeed : speed;

        float blendValue = isCrouching ? Mathf.Clamp(direction.magnitude * crouchSpeed, 0, crouchSpeed) : Mathf.Clamp(direction.magnitude * speed, 0, speed);
        animator.SetFloat("Blend", blendValue);

        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * currentSpeed * Time.deltaTime);

            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }

        // Terapkan gravitasi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
