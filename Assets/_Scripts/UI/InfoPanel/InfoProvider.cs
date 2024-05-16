using UnityEngine;

public class InfoProvider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Sprite Sprite => _spriteRenderer.sprite;
    public string DisplayName => gameObject.name;
}