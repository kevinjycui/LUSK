using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stalactite: MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public LayerMask snailLayer;
    [SerializeField] private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        if ((Mathf.Abs(transform.position.x - player.position.x) < 0.5) && (Mathf.Abs(transform.position.z - player.position.z) < 0.5) && (0 < (transform.position.y - player.position.y)) && (transform.position.y - player.position.y) < 12.26){
            rigid.useGravity = true;
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(gameObject);
    }

}
