using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Tooltip("Bullet Prefab which will be shot when Finge Gun Pose Ends.")]
    [SerializeField] GameObject bulletPrefab;

    [Tooltip("Force value that determins how fast the bullet will be shot.")]
    [SerializeField] private float shotStrength = 100f;

    [Tooltip("Transform that holds the bullets until they are destroyed.")]
    private Transform bulletHolder;

    // Public Method to shoot Bullet prefab when called.
    public void ShootBullet()
    {
        Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();
        bullet.AddForce(transform.forward * shotStrength);
        Destroy(bullet.gameObject, 5f);
    }

}
