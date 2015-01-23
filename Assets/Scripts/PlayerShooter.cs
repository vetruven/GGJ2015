using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet;
    public List<Transform> muzzles;
    public float fireCooldown = 1;

    int lastMuzzleShot = 0;
    private float nextShotTime = 0;
    private bool needsToShoot = false;


    void Update()
    {
        if(needsToShoot && nextShotTime < Time.time)
            CreateShot();
    }


    private void CreateShot()
    {
        lastMuzzleShot = (lastMuzzleShot + 1)%muzzles.Count;
        Transform currMuzzle = muzzles[lastMuzzleShot];
        Instantiate(bullet, currMuzzle.position, currMuzzle.rotation);

        needsToShoot = false;
        nextShotTime = Time.time + fireCooldown;
    }


    public void QueueShoot()
    {
        needsToShoot = true;
    }
}
