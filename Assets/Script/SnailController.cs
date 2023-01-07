using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;

    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
   //ublic PlayerMovementAdvanced pm;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public KeyCode jumpKey = KeyCode.Space;
    public int climbJumps;
    private int climbJumpsLeft;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastWall;
    private Vector3 lastWallNormal;
    public float minWallNormalAngleChange;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Movement")]
    [SerializeField]
    float speed = 2f;
    

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxis("Horizontal");

        if (horizontalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            transform.position = new Vector3(transform.position.x + Time.deltaTime * horizontalVelocity, transform.position.y, transform.position.z);
        }
    }

}
