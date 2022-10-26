using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float force;
    Rigidbody rig;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rig.AddForce(transform.forward * force, ForceMode.Impulse);
    }
    void Update()
    {
    }
}
