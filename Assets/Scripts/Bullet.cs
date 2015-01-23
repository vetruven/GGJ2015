using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 200;
    public float range = 400;
    public Vector3 origPos;
    public GameObject hitParticlePrefab;

    void Awake()
    {
        rigidbody.velocity = transform.forward*speed;
        origPos = transform.position;
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(origPos,transform.position) > range)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        ParticleSystem hitParticle = ((GameObject)Instantiate(hitParticlePrefab)).GetComponent<ParticleSystem>();
        hitParticle.transform.forward = (origPos - transform.position).normalized;
        hitParticle.Play();
        Destroy(gameObject);
    }
}
