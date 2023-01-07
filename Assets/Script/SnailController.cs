using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField, HideInInspector]
    bool isHidden;

    [Header("References")]
    private Transform orientation;
    private Rigidbody rigidBody;
    [SerializeField]
    LayerMask wallLayer;

    [Header("Detection")]
    [SerializeField]
    float detectionLength;
    public float sphereCastRadius;

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
    [SerializeField,HideInInspector]
    private bool climbing;


    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        HandleMovement();
    }

    void HandleMovement()
    {

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            float horizontalVelocity = horizontalMove * speed;
            float verticalVelocity = verticalMove * speed;

            transform.localPosition += transform.forward * Time.deltaTime * verticalVelocity;
            transform.Rotate(0f, Time.deltaTime * horizontalVelocity * 5, 0f);
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, wallLayer);
    }

    private void ClimbOn()
    {
        climbing = true;
    }

    private void HandleClimb()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, speed, rigidBody.velocity.z);
    }

    private void ClimbOff()
    {
        climbing = false;
    }

}
