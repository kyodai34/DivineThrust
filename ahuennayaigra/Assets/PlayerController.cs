using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    [Header("Movement")]
    [SerializeField] public float runSpeed;
    [SerializeField] public float currentSpeed;
    public float speed = 1f;
    public float gravity = -9.8f;
    public float groundDistance = -0.4f;

    [Header("Ground check")]
    public Transform ground_check;
    public LayerMask groundMask;



    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(ground_check.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        animator.SetFloat("Forward", y);
        animator.SetFloat("Strafe", x);

        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            animator.SetBool("run", true);
        }
        else
        {
            speed = currentSpeed;
            animator.SetBool("run", false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
