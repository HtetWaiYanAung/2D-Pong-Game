using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_BotBorder : MonoBehaviour
{
    [SerializeField] private Ctrl_GamePlay _gamePlayControl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ctrl_Ball ballControl = collision.gameObject.GetComponent<Ctrl_Ball>();
        if(ballControl != null)
        {
            _gamePlayControl.BallLost(ballControl);
        }
    }
}
