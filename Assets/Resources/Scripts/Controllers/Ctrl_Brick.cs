using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Brick : MonoBehaviour
{
    public int Scores
    {
        get
        {
            return _scores;
        }
    }
    [SerializeField] private Ctrl_GamePlay _gamePlayControl;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _scores;
    [SerializeField] private SpriteRenderer _sr;

    private int _currentHitPoints;

    private void Start()
    {
        _currentHitPoints = _hitPoints;
    }

    public void ReceiveDamage(int damage)
    {
        _currentHitPoints -= damage;

        if (_currentHitPoints <= 0)
        {
            DestroyBrick();
        }
        else
        {
            float colorPercent = ((float)_currentHitPoints / (float)_hitPoints);
            Color newColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, colorPercent);
            _sr.color = newColor;
        }
    }

    private void DestroyBrick()
    {
        GameManager.Instance.BrickDestoryed(this);
    }
}
