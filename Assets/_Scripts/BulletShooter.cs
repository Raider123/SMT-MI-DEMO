using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletShooter : MonoBehaviour
{
    [Tooltip("Bullet Prefab which will be shot when Finge Gun Pose Ends.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Force value that determins how fast the bullet will be shot.")]
    [SerializeField] private float shotStrength = 100f;

    [Tooltip("Das GameObject, das den Raycast durchführt")]
    [SerializeField] private GameObject raycastObject; 

    [Tooltip("Maximale Entfernung für den Raycast")]
    [SerializeField] private float maxDistance = 10f; 

    [Tooltip("Eine Instanz des Targetchooser Skripts zum Erhöhen der Munition etc.")]
    [SerializeField] private OnSceneLoad tchooser_script;

    [Tooltip("Die Audiosource, die beim Abschuss gespielt wird")]
    [SerializeField] private AudioSource shoot_audio_1;

    [Tooltip("Die zweite Audiosource, die beim Abschuss gespielt wird")]
    [SerializeField] private AudioSource shoot_audio_2;

    [Tooltip("Transform that holds the bullets until they are destroyed.")]
    private Transform bulletHolder;

    private Rigidbody bullet;

    private Transform hitObjectTransform;

    // Public Method to shoot Bullet prefab when called.
    public void ShootBullet()
    {
        // Führen Sie den Raycast vom raycastObject aus 
        if (Physics.Raycast(raycastObject.transform.position, raycastObject.transform.forward, out RaycastHit hitInfo, maxDistance))
            {
                // Erstellen Sie das Bullet und fügen Sie ihm eine Kraft hinzu
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();
                //bullet.AddForce(transform.forward * shotStrength, ForceMode.Impulse);


                // Erhalten Sie die Transform des getroffenen Gameobjects
                hitObjectTransform = hitInfo.collider.transform;

                // Erhalten Sie die Richtung zum getroffenen Punkt (in y und z Richtung)
                Vector3 direction = hitObjectTransform.position - bullet.transform.position;

                // Fügen Sie dem Bullet eine Anziehungskraft hinzu, um es zum getroffenen Punkt zu ziehen
                bullet.AddForce(1f * shotStrength * direction.normalized, ForceMode.Impulse);

                // Drehen Sie das Bullet, um es in Richtung des Ziels zu richten (optional)
                bullet.transform.LookAt(hitObjectTransform.position);


                // Spiele den Audioclip ab (mit Variation zwischen Clip 1 und Clip 2)
                float randomValue = Random.value;

                // Überprüfe, ob die Zufallszahl kleiner als 0.5 ist
                if (randomValue < 0.5f)
                {
                    shoot_audio_1.Play();
                }
                else
                {
                    shoot_audio_2.Play();
                }

                // Erhöhe die Anzahl der verschossenen Munition extern
                tchooser_script.ammo += 1;

                // Zerstören Sie das Bullet nach einer bestimmten Zeit
                Destroy(bullet.gameObject, 1.5f);
            }
            
    }

    private void Update()
    {
        // If we activate this method, we guide the bullet straight into the goal
        // Führen Sie den Raycast vom raycastObject aus
        if(bullet != null)
        {
            // Erhalten Sie die Richtung zum getroffenen Punkt (in y und z Richtung)
            Vector3 direction = hitObjectTransform.position - bullet.transform.position;

            // Fügen Sie dem Bullet eine Anziehungskraft hinzu, um es zum getroffenen Punkt zu ziehen
            bullet.AddForce(0.8f * shotStrength * direction.normalized, ForceMode.Impulse);

            // Drehen Sie das Bullet, um es in Richtung des Ziels zu richten (optional)
            bullet.transform.LookAt(hitObjectTransform.position);
        }      
    }
}
