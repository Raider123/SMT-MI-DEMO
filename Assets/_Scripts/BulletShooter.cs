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

    [Tooltip("Die Audiosource, die beim Abschuss gespielt wird")]
    [SerializeField] private AudioSource shoot_audio_1;

    [Tooltip("Die zweite Audiosource, die beim Abschuss gespielt wird")]
    [SerializeField] private AudioSource shoot_audio_2;

    [Tooltip("Transform that holds the bullets until they are destroyed.")]
    private Transform bulletHolder;

    [Tooltip("Reference to the targetchooser-Object to retrieve the new_target position.")]
    [SerializeField] private GameObject target_chooser;

    private Rigidbody bullet;

    private Transform hitObjectTransform;

    // Diese Methode bildet im Normalfall eine realistischere Bewegung der Geschosse zum Ziel, da sie mit der Position des Gaze Interactors zusammenfallen
    public void ShootBullet()
    {
        // Führen Sie den Raycast vom raycastObject aus 
        if (Physics.Raycast(raycastObject.transform.position, raycastObject.transform.forward, out RaycastHit hitInfo, maxDistance))
            {
                // Erstellen Sie das Bullet und fügen Sie ihm eine Kraft hinzu
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();

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

                // Zerstören Sie das Bullet nach einer bestimmten Zeit
                Destroy(bullet.gameObject, 1.5f);
            }
    }

    // Im MI-Fall garantieren wir immer das Treffen des Ziels. Hier werden wir extern beeinflussen, ob wir die Mitte, Dazwischen oder den äußeren Rand treffen (d.h. der Schuss wird mit dem mitgelieferten Argument vorprogrammiert)
    public void Mi_shoot(float decision_value)
    {
        // Erstellen Sie das Bullet und fügen Sie ihm eine Kraft hinzu
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder).GetComponent<Rigidbody>();

        // Erhalten Sie die Transform des getroffenen Gameobjects
        GameObject actual_target = target_chooser.GetComponent<OnSceneLoad>().getActualTarget();

        //hitObjectTransform = actual_target.transform;
        hitObjectTransform = DetermineCoordinates(actual_target, decision_value);

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

        // Zerstören Sie das Bullet nach einer bestimmten Zeit
        Destroy(bullet.gameObject, 1.5f);
    }

    public Transform DetermineCoordinates(GameObject object2, float targetAccuracy)
    {
        // Get the collider size and radius of object1
        Vector3 colliderSize = object2.transform.position;
        float radius = colliderSize.y * 0.5f; // Radius is 0.25 in the normal condition

        // Determine the desired distance based on the target accuracy
        float desiredDistance = radius * (targetAccuracy / 100.0f);

        // Calculate the actual distance needed (using the radius and target accuracy)
        float yDistance = desiredDistance * 0.5f;
        float zDistance = desiredDistance * 0.5f;


        object2.transform.position = new Vector3(0, object2.transform.position.y + yDistance, object2.transform.position.z + zDistance);

        return object2.transform;
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
