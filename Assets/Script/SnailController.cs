using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    [SerializeField] private LayerMask wallMask;
    [Header("Movement")]
    [SerializeField]
    float speed = 5f;

    void Start()
    {
        Quaternion initialRot = transform.rotation;
      
        MoveToGround();
        
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        CliffCheck();

        HandleMovement();
        // MoveToGround();

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
        float detectionLength = 1.6f;
        RaycastHit frontWallHit;
        float frontHitAngle;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward + transform.up;
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.green, 2, false);
        if (Physics.Raycast(ray, out frontWallHit, detectionLength, wallMask)) {
            Debug.Log("up");
            frontHitAngle = Vector3.Angle(transform.up, frontWallHit.normal);
            
            Debug.Log(frontHitAngle);
            if (frontHitAngle > -10 && frontHitAngle < 10) return;
            
            transform.Rotate(-frontHitAngle, 0f, 0f);
            transform.position = frontWallHit.point;
        }
        // Debug.Log(frontHitAngle);
    }
    
    private bool GroundCheck()
    {
        float detectionLength = 2.8f;
        float sphereCastRadiusGround = 30f;

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = -transform.up;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.white, 2, false);
        return Physics.SphereCast(ray, sphereCastRadiusGround, detectionLength, wallMask);
    }

    private void MoveToGround()
    {
        if (GroundCheck()) return;

        Debug.Log("ground");

        Ray ray = new Ray();
        ray.origin = transform.position + transform.up;
        ray.direction = -transform.up;
        
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask);

        transform.position = hit.point;
    }

    private void CliffCheck()
    {
        float detectionLength = 3.2f;

        RaycastHit groundHit;
        float nearCliffAngle;

        Ray ray = new Ray();
        ray.origin = transform.position + transform.forward * 0.1f + transform.up;
        ray.direction = -transform.up;
        Debug.DrawRay(ray.origin, ray.direction * detectionLength, Color.red, 2, false);

        Ray ray2 = new Ray();
        ray2.origin = ray.origin + ray.direction * detectionLength;
        ray2.direction = -transform.forward;
        Debug.DrawRay(ray2.origin, ray2.direction * detectionLength, Color.blue, 2, false);

        RaycastHit tmp;

        if (!Physics.Raycast(ray, out tmp, detectionLength, wallMask)) {
            // Debug.Log("cliff");
            if (Physics.Raycast(ray2, out groundHit, detectionLength, wallMask)) {
                Debug.Log("down");
                nearCliffAngle = Vector3.Angle(transform.forward, groundHit.normal);

                Debug.Log(nearCliffAngle);

                transform.Rotate(90-nearCliffAngle, 0f, 0f);
                transform.position = groundHit.point;
            }
        }

    }

}
