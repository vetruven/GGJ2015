using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 200;
    public float range = 400;
    public float damageMin = 10;
    public float damageMax = 15;
    public float damage;

    public Vector3 origPos;
    public GameObject hitParticlePrefab;

    void Awake()
    {
        origPos = transform.position;
        damage = Random.Range(damageMin, damageMax) * (Random.value < 0.075f ? 2:1);
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
            {
                h.Hit(this);
                CollideBullet(rchit.point);
            }
            else
                MoveBullet(); 
        }
        else
            MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position = transform.position + transform.forward*Time.deltaTime*speed;
    }

    void CollideBullet(Vector3 pCollisionPos)
    {
        ParticleSystem hitParticle = ((GameObject)Instantiate(hitParticlePrefab)).GetComponent<ParticleSystem>();
        hitParticle.transform.position = transform.position;
        hitParticle.transform.forward = (origPos - transform.position).normalized;
        Destroy(gameObject);
    }
}
