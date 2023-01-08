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

    private RaycastHit groundHit;
    private float groundHitAngle;
    private Vector3 groundDirection;
    [SerializeField]
    float sphereCastRadiusGround;

    private RaycastHit cliffHit;
    private float nearCliffAngle;


    [SerializeField] private LayerMask wallMask;
    [Header("Movement")]
    [SerializeField]
    float speed = 5f;

    [SerializeField, HideInInspector] Quaternion initialRot;
    bool first = true;

    void Start()
    {
        Quaternion initialRot = transform.rotation;
      
        MoveToGround();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToGround();

        WallCheck();
        CliffCheck();

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
        if (verticalMove > 0) {
            float verticalVelocity = verticalMove * speed;
            transform.Translate(0, 0, Time.deltaTime * verticalVelocity);
        }
        // transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f));
    }

    private void WallCheck()
    {
        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward * 0.3f;
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.green, 2, false);
        if (Physics.Raycast(ray, out frontWallHit, detectionLength, wallMask)) {
            Debug.Log("up");
            frontHitAngle = Vector3.Angle(transform.up, frontWallHit.normal);
            transform.Rotate(-frontHitAngle, 0f, 0f);
            transform.position = frontWallHit.point;
        }
        // Debug.Log(frontHitAngle);
    }
    
    private bool GroundCheck()
    {
        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = -transform.up;
        return Physics.SphereCast(ray, sphereCastRadiusGround, 0.3f + detectionLength, wallMask);

    }

    private void MoveToGround()
    {
        if (GroundCheck()) return;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = -transform.up;
        
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask);

        transform.position = hit.point;
    }

    private void CliffCheck()
    {
        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward * (0.3f + 0.1f) + transform.up;
        ray.direction = -transform.up;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.red, 2, false);

        Ray ray2 = new Ray();
        ray2.origin = ray.origin + ray.direction * detectionLength;
        ray2.direction = -transform.forward;
        Debug.DrawRay(ray2.origin, ray2.direction * detectionLength, Color.blue, 2, false);

        if (!Physics.Raycast(ray, detectionLength * 10f, wallMask)) {
            Debug.Log("cliff");
            if (Physics.Raycast(ray2, out groundHit, 0.3f)) {
                Debug.Log("down");
                nearCliffAngle = Vector3.Angle(transform.forward, groundHit.normal);
                transform.Rotate(90-nearCliffAngle, 0f, 0f);
                transform.position = groundHit.point;
            }
        }

    }

}
