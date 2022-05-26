using UnityEngine;

public class Ctrl_PowerUp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private E_PowerUpType _powerUpType;
    [SerializeField] private Ctrl_GamePlay _gameControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ctrl_Paddle paddle = collision.gameObject.GetComponent<Ctrl_Paddle>();
        if (paddle != null)
        {
            _gameControl.PowerUpTrigger(_powerUpType);
        }
        Destroy(this.gameObject);
    }
}
