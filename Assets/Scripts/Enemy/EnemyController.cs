using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _objectModelTransform;
    [SerializeField] private Animator _animator;
    private Rigidbody2D _enemyRb;

    private bool _inRange;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemyRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inRange)
        {
            //TargetTracking();
            LookAtPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("Attack");
            _inRange = true;
            _animator.SetBool("inRange",true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("Attack");
            _inRange = true;
            _animator.SetBool("inRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inRange = false;
            _animator.SetBool("inRange", false);
        }
    }

    //private void TargetTracking()
    //{
    //    if (_target.transform.position.x > transform.position.x && !_isFacingRight)
    //    {
    //        Flip();
    //    }
    //    if (_target.transform.position.x < transform.position.x && _isFacingRight)
    //    {
    //        Flip();
    //    }
    //}

    //private void Flip()
    //{
    //    _isFacingRight = !_isFacingRight;
    //    transform.Rotate(0f, 180f, 0f);
    //}

    public void LookAtPlayer()
    {
        if ((_target.position.x < _objectModelTransform.transform.position.x && _isFacingRight) || (_target.position.x > _objectModelTransform.transform.position.x && !_isFacingRight))
        {
            _objectModelTransform.localScale *= new Vector2(-1, 1);
            _isFacingRight = !_isFacingRight;
        }
    }
}
