using UnityEngine;

public class FireScorch : MonoBehaviour
{
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _fireTimer;
    private Health _wizardHealth;
    private bool _attack = true;
    private Health _playerHealth;

    public void Awake()
    {
        _wizardHealth = gameObject.GetComponentInParent<Health>();
        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        CanAttack();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_fireTimer >= _shootInterval && _attack == true)
            {
                Scorch();
            }
        }
    }

    private void Scorch()
    {
        if (_playerHealth != null)
        {
           _playerHealth.TakeHit(_damage);
            _fireTimer = 0f;
        }
    }

    private void CanAttack()
    {
        if (_wizardHealth._hitPoints <= 0)
        {
            _attack = false;
        }
    }
}
