using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed;

    void Awake()
    {
        rigidbody.velocity = transform.forward*speed;
    }
}
