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
    private float nearCliffAngle;
    private bool onCliff;


    [SerializeField] private LayerMask wallMask;
    [Header("Movement")]
    [SerializeField]
    float speed = 5f;
    [SerializeField,HideInInspector]
    private bool climbing;
    private bool descending;

    [SerializeField, HideInInspector] Quaternion initialRot;
    bool first = true;
    private bool nearCliff;

    void Start()
    {
        Quaternion initialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        GroundCheck();
        setClimbing();
        CliffCheck();

        if (!onGround)
        {
            transform.Translate(0, Time.deltaTime * -3f, 0);
        }
        else if (climbing)
        {
            HandleClimb();
        }
        else if (!nearCliff)
        {
            nearCliff = Physics.Raycast((transform.position - transform.up * detectionLength + transform.forward * 0.3f), -transform.forward, out groundHit, detectionLength);
            nearCliffAngle = Vector3.Angle(transform.forward, groundHit.normal);
            HandleClimbDown();
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
        frontHitAngle = Vector3.Angle(transform.up, frontWallHit.normal);
        Debug.Log(frontHitAngle);
    }
    
    private void GroundCheck()
    {
        onGround = Physics.SphereCast(transform.position, 0.01f, -transform.up, out groundHit, 0.05f); 
        


    }

    private void CliffCheck()
    {
        nearCliff = Physics.Raycast((transform.position + transform.forward * 0.3f), -transform.up, out groundHit, detectionLength);


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
        transform.position += 0.88f * transform.forward;
        transform.position += transform.up;
        transform.Rotate(-frontHitAngle, 0f, 0f);
    }

    private void HandleClimbDown()
    {
        transform.position += 0.88f * transform.forward;
        transform.position -= transform.up;
        transform.Rotate(90-nearCliffAngle, 0f, 0f);
    }
}
