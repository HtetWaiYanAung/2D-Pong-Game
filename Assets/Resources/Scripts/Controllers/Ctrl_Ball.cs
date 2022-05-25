using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Ball : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _ballMaterial;
    [SerializeField] private float _ballSpeed = 15f;
    [SerializeField] private Vector2 _ballStartingPosition = Vector2.down * 8.5f;
    [SerializeField] private int _ballDamage = 1;
    [SerializeField] private Rigidbody2D _rb;


    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.position = _ballStartingPosition;
        float x = Random.Range(-0.2f, 0.2f);
        float y = Random.Range(0.6f, 0.8f);
        _rb.velocity = (new Vector2(x,y)).normalized * _ballSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ctrl_Brick brickControl = collision.gameObject.GetComponent<Ctrl_Brick>();
        if (brickControl != null)
        {
            brickControl.ReceiveDamage(_ballDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ctrl_Brick brickControl = collision.gameObject.GetComponent<Ctrl_Brick>();
        if(brickControl!= null)
        {
            brickControl.ReceiveDamage(_ballDamage);
        }

        if (Mathf.Abs(_rb.velocity.x) <= 0.05f)
        {
            if (_rb.position.x < 0)
            {
                _rb.velocity = new Vector2(Random.Range(0.25f, 0.5f), _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(-1 * Random.Range(0.25f, 0.5f), _rb.velocity.y);
            }
        }
        if (Mathf.Abs(_rb.velocity.y) <= 0.05f)
        {
            if (_rb.position.y < 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, Random.Range(0.5f, 1f));
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -1f * Random.Range(0.5f, 1f));
            }
        }
        _rb.velocity = _rb.velocity.normalized * _ballSpeed;
    }

}
