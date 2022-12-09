using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private LayerMask _whatIsEnemies;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionDamage;
    private GameObject cloneExplosion;

    private void Start()
    {
        _explosionPrefab.GetComponent<GameObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _whatIsEnemies);
        foreach (var obj in enemiesToDamage)
        {
            var _target = obj.GetComponentInParent<Health>();
            if (_target)
            {
                cloneExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _target.TakeHit(_explosionDamage / 2);
                Destroy(cloneExplosion, 1f);
            }
        }
        Destroy(gameObject, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
