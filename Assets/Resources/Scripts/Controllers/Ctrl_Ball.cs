using UnityEngine;

public class Ctrl_Ball : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _ballMaterial;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _ballStartingPosition = Vector2.down * 8.5f;
    [SerializeField] private int _ballDamage = 1;
    [SerializeField] private float _ballSpeed = 15f;
    [SerializeField] private float _minXRange = -0.2f;
    [SerializeField] private float _maxXRange = 0.2f;
    [SerializeField] private float _minYRange = 0.6f;
    [SerializeField] private float _maxYRange = 0.8f;
    [SerializeField] private float _directionDeflectionThreshold = 0.05f;
    [SerializeField] private float _minXDeflectionRange = 0.25f;
    [SerializeField] private float _maxXDeflectionRange = 0.5f;
    [SerializeField] private float _minYDeflectionRange = 0.5f;
    [SerializeField] private float _maxYDeflectionRange = 1f;

    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.position = _ballStartingPosition;
        float x = Random.Range(_minXRange, _maxXRange);
        float y = Random.Range(_minYRange, _maxYRange);
        _rb.velocity = (new Vector2(x, y)).normalized * _ballSpeed;
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
        if (brickControl != null)
        {
            brickControl.ReceiveDamage(_ballDamage);
        }

        if (Mathf.Abs(_rb.velocity.x) <= _directionDeflectionThreshold)
        {
            if (_rb.position.x < 0)
            {
                _rb.velocity = new Vector2(Random.Range(_minXDeflectionRange, _maxXDeflectionRange), _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(-1 * Random.Range(_minXDeflectionRange, _maxXDeflectionRange), _rb.velocity.y);
            }
        }
        if (Mathf.Abs(_rb.velocity.y) <= 0.05f)
        {
            if (_rb.position.y < 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, Random.Range(_minYDeflectionRange, _maxYDeflectionRange));
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -1f * Random.Range(_minYDeflectionRange, _maxYDeflectionRange));
            }
        }
        _rb.velocity = _rb.velocity.normalized * _ballSpeed;
    }

}
