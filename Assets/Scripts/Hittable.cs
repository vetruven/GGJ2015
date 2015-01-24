using UnityEngine;
using System.Collections;

public class Hittable : MonoBehaviour
{

    private Enemy e;

    void Awake()
    {
        e = GetComponent<Enemy>();
    }

    public void Hit(Bullet pBullet)
    {
        if (e != null)
            e.RemoveLife(pBullet.damage);
    }
}
