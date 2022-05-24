using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Paddle : MonoBehaviour
{
    [SerializeField] private Ctrl_GamePlay _gamePlayControl;
    [SerializeField] private PhysicsMaterial2D _paddleMaterial;
    [SerializeField] private float _paddleSpeed = 30f;
    [SerializeField] private Vector2 _paddleStartingPosition = Vector2.down * 9f;
    [SerializeField] private float _paddleWidthRatioToScreen = 0.3f;

    
    private SpriteRenderer _sr;
    private const float FLT_PADDLE_HEIGHT = 0.5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        _rb = GetComponent<Rigidbody2D>();
        if(_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            if(_paddleMaterial != null)
            {
                _rb.sharedMaterial = _paddleMaterial;
            }
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }
        _sr = GetComponent<SpriteRenderer>();

        float paddleWidth = Hlpr_ScreenSize.GetScreenToWorldWidth * _paddleWidthRatioToScreen;
        transform.localScale = new Vector2(paddleWidth, FLT_PADDLE_HEIGHT);
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float maxDistance = _gamePlayControl.TfRightX.position.x - _sr.bounds.extents.x;
            vec.x= Mathf.Clamp(vec.x, -1 * maxDistance, maxDistance);
            transform.position = new Vector2(vec.x, transform.position.y);
        }
    }

    public void Reset()
    {
        transform.position = _paddleStartingPosition;
    }

}
