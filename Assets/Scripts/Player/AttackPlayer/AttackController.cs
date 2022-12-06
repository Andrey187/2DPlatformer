using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _launchOffset;
    [SerializeField] private ShootApple _shootApplePrefab;
    [SerializeField] private float _attackInterval;
    [SerializeField] private float _attackTimer;

    [SerializeField] private Transform _meleeAttackPos;
    [SerializeField] private LayerMask _whatIsEnemies;
    [SerializeField] private float _meleeAttackRangeX;
    [SerializeField] private float _meleeAttackRangeY;
    [SerializeField] private float _meleeDamage;

    private void Update()
    {
        AttackInterval();
    }

    private void AttackInterval()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackInterval)
        {
            MeleeAttack();
            Shoot();
        }
    }

    private void MeleeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("attack");
            _attackTimer = 0;
        }
    }

    private void MeleeDamage()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(_meleeAttackPos.position,
                new Vector2(_meleeAttackRangeX, _meleeAttackRangeY), 0, _whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Health>().TakeHit(_meleeDamage);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(_shootApplePrefab, _launchOffset.position, _launchOffset.rotation);
            _attackTimer = 0;
            _animator.SetTrigger("Shoot");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_meleeAttackPos.position, new Vector3(_meleeAttackRangeX, _meleeAttackRangeY, 1));
    }
}
