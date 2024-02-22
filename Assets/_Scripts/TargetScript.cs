using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private bool isUp = true;

    [SerializeField] private float tweenTime = 0.4f;

    [Tooltip("Amount of seconds a target stays down before swinging back up 'reanimating' itself. If '<=0', the target will permanently stay down.")]
    [SerializeField] private int downtime = 5;

    // Catch Collision and make target fall back.
    private void OnCollisionEnter(Collision collision)
    {
        // only execute when target actually up
        if (isUp)
        {
            isUp = false;

            // tween the target into down position
            LeanTween.cancelAll(gameObject);
            LeanTween.rotate(gameObject, new Vector3(90, 0, 0), tweenTime).setEase(LeanTweenType.easeOutBounce);

            if (downtime > 0) StartCoroutine(ReactivateTarget());
        }
    }

    private IEnumerator ReactivateTarget()
    {
        yield return new WaitForSeconds(downtime);

        // tweeen target up
        LeanTween.cancelAll();
        LeanTween.rotate(gameObject, Vector3.zero, tweenTime).setEase(LeanTweenType.easeOutBounce);

        isUp = true;
    }
}
