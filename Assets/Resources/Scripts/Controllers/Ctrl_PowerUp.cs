using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PowerUp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private E_PowerUpType _powerUpType;
    [SerializeField] private Ctrl_GamePlay _gameControl;

    private void Start()
    {
        switch (_powerUpType)
        {
            case E_PowerUpType.ExtraLive:
                _sr.color = Color.green;
                break;
            case E_PowerUpType.ExtraBall:
                _sr.color = Color.red;
                break;
            case E_PowerUpType.BallsImmunity:
                _sr.color = Color.blue;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ctrl_Paddle paddle = collision.gameObject.GetComponent<Ctrl_Paddle>();
        if(paddle != null)
        {
            _gameControl.PowerUpTrigger(_powerUpType);
        }
        Debug.Log(collision.name + "Hit");
            Destroy(this.gameObject);
    }
}
