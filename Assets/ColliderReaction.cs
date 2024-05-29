using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpriteRenderer))]
public class ColliderReaction : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] private float tweenDuration = 0.5f;
    [SerializeField] private Color colorDeselected;
    [SerializeField] private Color colorSelected;

    [SerializeField] private GameObject sphere;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, SetColor, spriteRenderer.color, colorSelected, tweenDuration).setEaseInOutExpo();

        sphere.SetActive(true);



    }
    private void OnTriggerExit(Collider other)
    {
        //LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, SetColor, spriteRenderer.color, colorDeselected, tweenDuration).setEaseInOutExpo();

        sphere.SetActive(false);
    }

    private void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }
}
