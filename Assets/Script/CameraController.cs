using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] private GameObject rainParticles;
    [SerializeField] private GameObject snowParticles;
    [SerializeField] private GameObject sunParticles;

    private float targetY;
    private Vector3 targetAngle;

    [SerializeField] private LayerMask mask;

    [SerializeField]
    public float speed = 10000f;
    public float rotationalSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 10f, player.transform.position.z);
        targetY = transform.position.y;
        targetAngle = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        sunParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        snowParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        rainParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), speed * Time.deltaTime);

        Vector3 direction = Vector3.RotateTowards(transform.forward, targetAngle, rotationalSpeed * Time.deltaTime, 1000f);
        transform.rotation = Quaternion.LookRotation(direction);

        // if (transform.position.y == targetY && transform.forward == targetAngle) {
        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = transform.forward;

        RaycastHit hit;

        bool hitFlag = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
        if (hitFlag && GameObject.ReferenceEquals(hit.collider.gameObject, player)) return;

        Vector3 origin = new Vector3(transform.position.x, player.transform.position.y + 10f, transform.position.z);

        for (int i=0; i<4; i++) {
            for (int j=-i; j<=i+1; j+=2*i+1) {
                ray.origin = origin + Vector3.up * (j * 15f);
                ray.direction = player.transform.position - ray.origin;
                Debug.DrawRay(ray.origin, ray.direction * 200f, Color.white, 0.5f, false);
                hitFlag = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
                // Debug.Log(GameObject.ReferenceEquals(hit.collider.gameObject, player));
                if (hitFlag && GameObject.ReferenceEquals(hit.collider.gameObject, player)) {
                    targetY = origin.y + j * 15f;
                    // float angle = Vector3.Angle(transform.forward, ray.direction);
                    // Debug.Log(angle);
                    // transform.forward = ray.direction;
                    targetAngle = ray.direction;
                    // Debug.Log(targetAngle);
                    return;
                }
            }
        // }
        // }
        
        }
    }
}
