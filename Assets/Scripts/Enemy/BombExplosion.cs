using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private Health _playerHealth;
    private float _damage = 3;

    private void Start()
    {
        _explosion.GetComponent<GameObject>();
        _bomb.GetComponent<GameObject>();
        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject cloneExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            _playerHealth.TakeHit(_damage);
            _bomb.gameObject.SetActive(false);
            Destroy(cloneExplosion, 1f);
        }
        
    }
}
