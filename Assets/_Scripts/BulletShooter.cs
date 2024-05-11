using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Tooltip("Bullet Prefab which will be shot when Finge Gun Pose Ends.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Force value that determins how fast the bullet will be shot.")]
    [SerializeField] private float shotStrength = 100f;

    [Tooltip("Das GameObject, das den Raycast durchführt")]
    [SerializeField] private GameObject raycastObject; 

    [Tooltip("Maximale Entfernung für den Raycast")]
    [SerializeField] private float maxDistance = 20f; 

    [Tooltip("Eine Instanz des Targetchooser Skripts zum Erhöhen der Munition etc.")]
    [SerializeField] private OnSceneLoad tchooser_script;

    [Tooltip("Die Audiosource, die beim Abschuss gespielt wird")]
    [SerializeField] private AudioSource shoot_audio;

    [Tooltip("Transform that holds the bullets until they are destroyed.")]
    private Transform bulletHolder;

    // Public Method to shoot Bullet prefab when called.
    public void ShootBullet()
    {
        /*
        Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();
        bullet.AddForce(transform.forward * shotStrength);
        Destroy(bullet.gameObject, 5f);
        */

        // Erstellen Sie das Bullet und fügen Sie ihm eine Kraft hinzu
        Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();
        bullet.AddForce(transform.forward * shotStrength, ForceMode.Impulse);

        // Führen Sie den Raycast vom raycastObject aus
        if (Physics.Raycast(raycastObject.transform.position, raycastObject.transform.forward, out RaycastHit hitInfo, maxDistance))
        {
            // Erhalten Sie die Richtung zum getroffenen Punkt
            Vector3 direction = hitInfo.point - transform.position;

            // Erstellen Sie ein Ziel, das ein wenig über dem getroffenen Punkt liegt (optional)
            Vector3 target = hitInfo.point; // + hitInfo.normal * 0.1f;

            // Fügen Sie dem Bullet eine Anziehungskraft hinzu, um es zum getroffenen Punkt zu ziehen
            bullet.AddForce(direction.normalized * shotStrength * 2, ForceMode.Impulse);

            // Drehen Sie das Bullet, um es in Richtung des Ziels zu richten (optional)
            bullet.transform.LookAt(target);
        }
        else
        {
            // Wenn der Raycast kein Ziel trifft, fahren Sie das Bullet geradeaus fort
            //bullet.AddForce(transform.forward * shotStrength, ForceMode.Impulse);
            Destroy(bullet.gameObject, 0f);
        }

        // Spiele den Audioclip ab
        shoot_audio.Play();

        // Erhöhe die Anzahl der verschossenen Munition extern
        tchooser_script.ammo += 1;

        // Zerstören Sie das Bullet nach einer bestimmten Zeit
        Destroy(bullet.gameObject, 5f);
    }

}
