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
        origPos = transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(origPos,transform.position) > range)
            Destroy(gameObject);

        RaycastHit rchit;
        if (Physics.Raycast(transform.position, transform.forward, out rchit, Time.deltaTime*speed))
        {
            Hittable h = rchit.transform.GetComponent<Hittable>();

            if (h != null)
                CollideBullet(rchit.point);
        }
        else
            transform.position = transform.position + transform.forward * Time.deltaTime * speed;
    }

    void CollideBullet(Vector3 pCollisionPos)
    {
        ParticleSystem hitParticle = ((GameObject)Instantiate(hitParticlePrefab)).GetComponent<ParticleSystem>();
        hitParticle.transform.position = transform.position;
        hitParticle.transform.forward = (origPos - transform.position).normalized;
        Destroy(gameObject);
    }
}
