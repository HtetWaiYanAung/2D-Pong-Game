using UnityEngine;

public class Ctrl_BotBorder : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Ctrl_GamePlay _gamePlayControl;
    [SerializeField] private GameObject _goImmunityBar;
    [SerializeField] private float _triggerDisableTimer = 3f;
    private float _timer;
    private bool _startCountdown = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ctrl_Ball ballControl = collision.gameObject.GetComponent<Ctrl_Ball>();
        if (ballControl != null)
        {
            _gamePlayControl.BallLost(ballControl);
        }
    }

    private void Start()
    {
        _timer = 0;
        _startCountdown = false;
        if (_goImmunityBar != null)
        {
            _goImmunityBar.SetActive(false);
        }
    }

    private void Update()
    {
        if (_startCountdown && Time.time > _timer)
        {
            _startCountdown = false;
            _boxCollider.isTrigger = true;
            if (_goImmunityBar != null)
            {
                _goImmunityBar.SetActive(false);
            }
        }
    }

    internal void DisableTriggerTemporary()
    {
        _boxCollider.isTrigger = false;
        _startCountdown = true;
        _timer = Time.time + _triggerDisableTimer;
        if (_goImmunityBar != null)
        {
            _goImmunityBar.SetActive(true);
        }
    }


}
