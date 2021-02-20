using UnityEngine;

public class CellRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public void Render(bool isVisible)
    {
        _sprite.enabled = isVisible;
    }
}