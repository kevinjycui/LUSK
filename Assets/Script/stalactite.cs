using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite: MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private float distanceFall;
    [SerializeField] private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < distanceFall || Mathf.Abs(transform.position.z - player.transform.position.z) < distanceFall)
        {
            rigid.useGravity = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(gameObject);
    }

}
