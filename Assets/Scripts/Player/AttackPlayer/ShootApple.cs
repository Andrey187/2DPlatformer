using UnityEngine;

public class ShootApple : MonoBehaviour
{
    [SerializeField] private float _speed = 4.5f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _lifeTime = 3;
    [SerializeField] private Rigidbody2D _appleRigidbody;
    [SerializeField] private SpriteRenderer _playerRb;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        _playerRb = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>();
        _playerTransform = _playerRb.GetComponent<Transform>();
    }

    private void Start()
    {
        _appleRigidbody.velocity = new Vector2(_playerTransform.transform.localScale.x * _speed, 1f);
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();

        if (enemy != null)
        {
            enemy.TakeHit(_damage);
            Destroy(gameObject);
        }
    }
}
