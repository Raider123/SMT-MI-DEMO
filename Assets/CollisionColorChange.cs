using UnityEngine;

public class CollisionColorChange : MonoBehaviour
{
    public Color collisionColor = Color.white;  // Farbe, die während der Kollision verwendet wird
    public bool IsColliding { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private string self_reference_tag;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;

            self_reference_tag = this.gameObject.tag;
        }
        else
        {
            Debug.LogError("Kein SpriteRenderer an diesem GameObject gefunden.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (spriteRenderer != null && self_reference_tag == other.gameObject.tag)
        {
            spriteRenderer.material.color = collisionColor;
            IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (spriteRenderer != null && self_reference_tag == other.gameObject.tag)
        {
            spriteRenderer.material.color = originalColor;
            IsColliding = false;
        }
    }
}
