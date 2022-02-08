using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public float groundRaycastDistance;
    public LayerMask groundMask;

    [Header("Physics")]
    public float gravity;
    public float accelerationPerDegree;
    public float defaultFallSpeed;
    public float frictionDeceleration;

    [Header("Movement")]
    public float maxTurnAngle;
    public float rotationSpeed;
    public float turnDeceleration;
    public float forwardAcceleration;
    public float slowDownDeceleration;
    public float maxSpeed;
    public float maxAcceleratingSpeed;
    public float minSpeed;
    public float finishSlowDownMultiplier;
    public AudioSource skisSfx;
    public float maxSkisSfxVolume;
    public ParticleSystem movementParticles;
    public float particleCountOnMaxSpeed;

    [Header("Jump")]
    public float maxJumpForce;
    public float minJumpForce;
    public float jumpChargeTime;
    public AudioSource jumpSfx;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isAirborne;
    [HideInInspector] public bool pressedJump;
    [HideInInspector] public bool stopMovement;
    [HideInInspector] public bool winBrakes;
    [HideInInspector] public Vector3 effector;

    Vector2 playerInput;
    RaycastHit groundHit;
    Vector3 velocity;
    bool landed;
    float horizontalMovement;
    float verticalMovement;
    Vector3 velocityGravityModifier;
    float jumpStartTime;
    bool isChargingJump;
    bool isJumping;
    float groundAngle;
    CharacterController controller;
    SquashAndStretch squashAndStretchScript;
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        squashAndStretchScript = GetComponent<SquashAndStretch>();
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    // FixedUpdate is called once per frame
    void Update()
    {
        HandlePlayerInput();
        GroundCheck();

        HandleSkisSfx();
        Gravity();

        if (levelManager.started)
        {
            if (!winBrakes)
            {
                HandleRotation();
                MoveForward();
                HandleLanding();
                ClampSpeed();
                HandleJump();
            }
            else
            {
                HandleWinBrakes();
            }
            controller.Move((velocity + velocityGravityModifier + effector) * Time.deltaTime);
        }
    }

    void HandleSkisSfx()
    {
        skisSfx.volume = (velocity.magnitude / maxAcceleratingSpeed) * maxSkisSfxVolume;
        if (isGrounded)
        {
            if (!skisSfx.isPlaying)
            {
                skisSfx.Play();
            }
        }
        else
        {
            if (skisSfx.isPlaying)
            {
                skisSfx.Stop();
            }
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPos.position, groundCheckRadius, groundMask).Length > 0;

        if (isGrounded)
        {
            Physics.Raycast(transform.position, -transform.up, out groundHit, groundRaycastDistance, groundMask);

            if (isJumping && controller.velocity.y < 0)
            {
                isJumping = false;
                squashAndStretchScript.Squash();
            }
        }
    }

    void HandleWinBrakes()
    {
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, slowDownDeceleration * finishSlowDownMultiplier * Time.deltaTime);
        if (velocity.magnitude > 0.01f)
        {
            if (transform.rotation.eulerAngles.y < 180)
            {
                float newRotation = transform.rotation.eulerAngles.y + rotationSpeed * 1.3f * Time.deltaTime * 1;
                if (newRotation < 160)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z);
                }
            }
            else
            {
                float newRotation = transform.rotation.eulerAngles.y + rotationSpeed * 1.3f * Time.deltaTime * -1;
                if (newRotation > 200)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z);
                }
            }
        }
    }

    void HandlePlayerInput()
    {
        if (!stopMovement)
        {
            horizontalMovement = Mathf.Clamp(playerInput.x * 10, -1, 1);
            verticalMovement = Mathf.Clamp(playerInput.y * 10, -1, 1);
        }
        else
        {
            horizontalMovement = 0;
            verticalMovement = 0;
        }
    }

    void HandleJump()
    {
        if (pressedJump)
        {
            if (!isChargingJump)
            {
                isChargingJump = true;
            }
        }

        if (isChargingJump && !pressedJump)
        {
            isChargingJump = false;
            isJumping = true;
            float jumpForceMultiplier = Mathf.Min(1f, (Time.time - jumpStartTime) / jumpChargeTime);
            float jumpHeight = Mathf.Max(minJumpForce, maxJumpForce * jumpForceMultiplier);
            velocity.y += jumpHeight;
            squashAndStretchScript.Stretch();
            jumpSfx.Play();

        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        pressedJump = value.ReadValueAsButton();
        if (isGrounded)
        {
            if (pressedJump)
            {
                jumpStartTime = Time.time;
                squashAndStretchScript.JumpSquash();
            }
        }
        else
        {
            pressedJump = false;
        }
    }

    void ClampSpeed()
    {
        if (isGrounded)
        {
            if (verticalMovement > 0.9f)
            {
                float currMax = (maxAcceleratingSpeed / 15f) * Mathf.Max(15f, groundAngle);
                if (velocity.magnitude > currMax)
                {
                    velocity = Vector3.MoveTowards(velocity, velocity.normalized * currMax, slowDownDeceleration * Time.deltaTime);
                }
            }
            else
            {
                float currMax = (maxSpeed / 15f) * Mathf.Max(15f, groundAngle);
                if (velocity.magnitude > currMax)
                {
                    velocity = Vector3.MoveTowards(velocity, velocity.normalized * currMax, slowDownDeceleration * Time.deltaTime);
                }
            }
        }


        if (velocity.z < 0)
        {
            velocity.z = 0;
        }
    }

    void HandleLanding()
    {
        if (isGrounded)
        {
            if (!landed)
            {
                landed = true;
                velocity.x = velocity.x - (transform.forward.x);
                velocity.y = velocity.y - (transform.forward.y);
                velocity.z = velocity.z - (transform.forward.z);
            }
        }
        else
        {
            landed = false;
        }
    }

    void HandleRotation()
    {
        if (isGrounded)
        {
            float newRotation = transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime * horizontalMovement;
            //clamp the rotation since the game is buggy if the player is facing backwards. Using mathf.clamp was buggy
            //negative angles are sometimes represented by their positive counterpart, ex: -10 degrees is translated to 350 degrees
            //this happens when we have for example: rotation = 0, we decrease it it becomes -0.xxx, same for 360 when increased
            if ((newRotation < maxTurnAngle && newRotation > -maxTurnAngle) || (newRotation < 360 + maxTurnAngle && newRotation > 360 - maxTurnAngle))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z);
            }

            //since I could not find a quick way to rotate the player upwards without messing with the Y rotation, I did this workaround of storing the value in prevYRotation
            float prevYRotation = transform.rotation.eulerAngles.y;
            if (groundHit.transform != null)
            {
                transform.up = groundHit.normal;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, prevYRotation, transform.rotation.eulerAngles.z);
        }
    }

    void MoveForward()
    {
        if (isGrounded)
        {
            //get angle between transform.up and normal
            groundAngle = Vector3.Angle(Vector3.up, transform.up);

            velocity.y = controller.velocity.y;

            if (verticalMovement > 0.8f)
            {
                velocity.x += (transform.forward * accelerationPerDegree * forwardAcceleration * groundAngle * transform.forward.z * Time.deltaTime).x;
                velocity.z += (transform.forward * accelerationPerDegree * forwardAcceleration * groundAngle * transform.forward.z * Time.deltaTime).z;
            }
            else if (verticalMovement < -0.8f)
            {
                if (velocity.magnitude > minSpeed)
                {
                    velocity = Vector3.MoveTowards(velocity, velocity.normalized * minSpeed, slowDownDeceleration * Time.deltaTime);
                }
            }
            else
            {
                velocity.x += (transform.forward * accelerationPerDegree * groundAngle * transform.forward.z * Time.deltaTime).x;
                velocity.z += (transform.forward * accelerationPerDegree * groundAngle * transform.forward.z * Time.deltaTime).z;
            }

            if (Mathf.Abs(horizontalMovement) > 0.99f)
            {
                velocity.x = (new Vector2(velocity.x, velocity.z).magnitude * new Vector3(transform.forward.x, 0, transform.forward.z).normalized.x);
                velocity.z = (new Vector2(velocity.x, velocity.z).magnitude * new Vector3(transform.forward.x, 0, transform.forward.z).normalized.z);

                velocity.x -= transform.forward.x * turnDeceleration * Time.deltaTime;
                velocity.z -= transform.forward.z * turnDeceleration * Time.deltaTime;
            }

            if (velocity.magnitude > minSpeed)
            {
                velocity.x += (transform.forward * accelerationPerDegree * frictionDeceleration * groundAngle * Time.deltaTime).x;
                velocity.z += (transform.forward * accelerationPerDegree * frictionDeceleration * groundAngle * Time.deltaTime).z;
            }
        }

    }

    void Gravity()
    {
        if (!isGrounded)
        {
            velocityGravityModifier = Vector3.zero;
            velocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded)
        {
            velocityGravityModifier = defaultFallSpeed * transform.up;
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        playerInput = value.ReadValue<Vector2>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(groundCheckPos.position, groundCheckRadius);
    }
}
