using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] private GameObject rainParticles;
    [SerializeField] private GameObject snowParticles;
    [SerializeField] private GameObject sunParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sunParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        snowParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        rainParticles.transform.position = transform.position - new Vector3(2f, 0f, 0f);
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 10, player.transform.position.z);
    }
}
