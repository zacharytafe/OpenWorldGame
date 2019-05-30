using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 5f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    [Header("Dash")]
    public float dashSpeed = 50f;
    public float dashDuration = .5f;

    private CharacterController controller; // Reference to CharacterController
    private Vector3 motion; // Is the movement offset per frame
    private bool isJumping;
    private float currentJumpHeight, currentSpeed;

    void OnValidate()
    {
        currentJumpHeight = jumpHeight;
        currentJumpHeight = jumpHeight;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Set initial states
        currentSpeed = walkSpeed;
        currentJumpHeight = jumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // WASD to move
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // Left shift Input
        bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
        bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);
        // Space to jump
        bool inputJump = Input.GetButtonDown("Jump");

        if (inputRun)
        {
            currentSpeed = runSpeed;
        }

        if (inputWalk)
        {
            currentSpeed = walkSpeed;
        }

        // Move character with inputs
        Move(inputH, inputV);

        // Is the player grounded
        if(controller.isGrounded)
        {
            // Cancel gravity
            motion.y = 0f;
            if(inputJump)
            {
                Jump(jumpHeight);
            }
            // Pressing jump
            if (isJumping)
            {
                // Jump
                motion.y = currentJumpHeight;
                // reset back to false
                isJumping = false;
            }
        }
        // Apply gravity
        motion.y += gravity * Time.deltaTime;
        // move with motion
        controller.Move(motion * Time.deltaTime);
    }


    // Move the character's motion in direction of input
    void Move(float inputH, float inputV)
    {
        // Generate direction from input
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        //Convert local space to world space direction
        direction = transform.TransformDirection(direction);

        // Check if direction exceeds magnitude of 1
        if (direction.magnitude > 1f)
        {
            // normal it
            direction.Normalize();
        }

        // Apply motion to only x and z
        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    public IEnumerator SpeedBoost(float startSpeed, float endSpeed, float duration)
    {
        currentSpeed = startSpeed;
        yield return new WaitForSeconds(duration);
        currentSpeed = endSpeed;

    }


    public void Jump(float height)
    {
        isJumping = true; // Tell the controller to jump at height
        currentJumpHeight = height;
    }

    public void Dash()
    {
        StartCoroutine(SpeedBoost(dashSpeed, walkSpeed, dashDuration));
    }
}
