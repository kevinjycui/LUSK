using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;

    [Header("Detection")]
    [SerializeField]
    float detectionLength;

    private RaycastHit frontWallHit;
    private float frontHitAngle;
    private bool wallFront;

    private RaycastHit groundHit;
    private float groundHitAngle;
    private Vector3 groundDirection;
    private bool onGround;
    [SerializeField]
    float sphereCastRadiusGround;

    private RaycastHit cliffHit;
    private bool onCliff;

    [Header("Movement")]
    [SerializeField]
    float speed = 5f;
    [SerializeField,HideInInspector]
    private bool climbing;
    private bool descending;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        GroundCheck();
        CliffCheck();
        setClimbing();
        if (climbing)
        {
            HandleClimb();
        }
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove != 0 ){
            float horizontalVelocity = horizontalMove * speed;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 15, 0f);
        }
         else if (verticalMove > 0) {
            float verticalVelocity = verticalMove * speed;
            transform.Translate(0, 0, Time.deltaTime * verticalVelocity);
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.Raycast((transform.position + transform.forward * 0.3f), transform.forward, out frontWallHit, detectionLength);
        frontHitAngle = Vector3.Angle(frontWallHit.normal, Vector3.up);
    }

    private void GroundCheck()
    {
        onGround = Physics.SphereCast(transform.position, sphereCastRadiusGround, -transform.up, out groundHit, detectionLength);
        groundHitAngle = Vector3.Angle(groundHit.normal, Vector3.up);
        groundDirection = Vector3.zero;
    }

    private void CliffCheck()
    {

    }

    private void setClimbing()
    {
        if (frontHitAngle < 92 && frontHitAngle > 15 && !descending)
        {
            climbing = true;
        }
        else
        {
            climbing = false;
        }
    }

    private void setDescending()
    {
        if(frontHitAngle < 92 && frontHitAngle > 15 && descending)
        {
            descending = true;
        }
        else
        {
            descending = false;
        }
    }

    private void HandleClimb()
    {
        transform.Rotate((360 - frontHitAngle), 180f, 180f);

        
    }

    private void ClimbOff()
    {
        climbing = false;
    }
}
