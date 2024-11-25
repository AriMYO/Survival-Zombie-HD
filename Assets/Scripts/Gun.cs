using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float impactForce = 30f;
    private float nextTimeToFire = 0f;
    private float timeBetweenShots = 0.1f;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / timeBetweenShots;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * impactForce, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }
}
