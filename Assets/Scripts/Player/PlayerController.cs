using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private UnityEvent _onLandEvent;
    [SerializeField] private Transform _groundCheck;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private float _forceMove = 10f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _objectModelTransform;
    private bool _isFacingRight = true;
    private Health _health;
    private Vector3 _velocity = Vector3.zero;

    public bool _airControl = false;
    public bool _isGrounded;
    public Rigidbody2D _rb;
    const float _jumpForce = 160f;
    const float _groundedRadius = .005f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();

        if (_onLandEvent == null)
            _onLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        GroundCheck(_onLandEvent);
    }

    public void Move(float move,bool jump)
    {
        if (_isGrounded || _airControl)
        {
            Vector3 targetVelocity = new Vector2(move * _forceMove, _rb.velocity.y);
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            if ((move > 0 && !_isFacingRight) || (move < 0 && _isFacingRight))
            {
                Flip();
            }
        }
        
        if (_isGrounded && jump)
        {
            _isGrounded = true;
            _rb.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void GroundCheck(UnityEvent _event)
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _isGrounded = true;
                if (!wasGrounded)
                    _event.Invoke();
            }
        }
    }

    private void Flip()
    {
        _objectModelTransform.localScale *= new Vector2(-1, 1);
        _isFacingRight = !_isFacingRight;
    }
}
