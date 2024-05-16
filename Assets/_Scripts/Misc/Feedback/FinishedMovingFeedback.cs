using UnityEngine;

public class FinishedMovingFeedback : MonoBehaviour, ITurnDependent
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _darkColor;
    
    private Color _originalColor;

    private void Start()
    {
        _originalColor = _spriteRenderer.color;
    }

    public void PlayFeedback()
    {
        _spriteRenderer.color = _darkColor;
    }

    public void StopFeedback()
    {
        _spriteRenderer.color = _originalColor;
    }

    public void WaitTurn()
    {
        StopFeedback();
    }
}