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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, SetColor, spriteRenderer.color, colorSelected, tweenDuration).setEaseInOutExpo();
    }
    private void OnTriggerExit(Collider other)
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, SetColor, spriteRenderer.color, colorDeselected, tweenDuration).setEaseInOutExpo();
    }

    private void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }
}
