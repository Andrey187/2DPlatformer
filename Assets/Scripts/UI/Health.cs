using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _hitPoints;
    [SerializeField] private float _maxHitPoints = 5;
    [SerializeField] HealthBarSlider _healthBar;
    [SerializeField] private GameOver _gameOver;
    private Animator _anim;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _gameOver = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameOver>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        _hitPoints = _maxHitPoints;
        _healthBar.SetHealth(_hitPoints, _maxHitPoints);
    }

    public void TakeHit(float damage)
    {
        BombExplosion _bomb = gameObject.GetComponent<BombExplosion>();
        _hitPoints -= damage;
        _healthBar.SetHealth(_hitPoints, _maxHitPoints);

        if (_anim != null)
        {
            _anim.SetTrigger("TakeHit");
        }

        if (_hitPoints <= 0)
        {
            _rb.bodyType = RigidbodyType2D.Static;

            if (_bomb != null)
            {
                Destroy(_bomb);
            }

            if (_anim != null)
            {
                _anim.SetTrigger("Death");
            }

            if (_rb.gameObject.tag == "Player")
            {
                _gameOver.CanvasActive();
            }
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        if (_anim != null) _anim.enabled = false;

        if (gameObject != gameObject.CompareTag("Player"))

            Destroy(gameObject);
    }
}
