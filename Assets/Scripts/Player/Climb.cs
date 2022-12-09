using Cinemachine;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [SerializeField] private bool _onWall;
    [SerializeField] private bool _onWallUp;
    [SerializeField] private bool _onWalldown;
    [SerializeField] private bool _onLedge;
    [SerializeField] private float _ledgeRayCorrectY = 0.5f;
    [SerializeField] private float _wallCheckRayDistance = 1f;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _minCorrectdistance = 0.01f;
    [SerializeField] private LayerMask _wall;
    [SerializeField] private Transform _wallCheckUp;
    [SerializeField] private Transform _wallCheckDown;
    [SerializeField] private Transform _finishLedgePosition;
    [SerializeField] private Canvas _healthBarCanvas;
    [SerializeField] private CinemachineVirtualCamera _cnCamera;

    private float _wallCheckRadiusDown;
    private Animator _anim;
    private Rigidbody2D _rb;
    private Transform _sliderPosition;
    private PlayerMovement _playerMove;
    public bool _blockMoveXYforLedge;

    private void Awake()
    {
        _cnCamera = Camera.main.gameObject.GetComponentInParent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _rb = gameObject.GetComponentInParent<Rigidbody2D>();
        _anim = gameObject.GetComponentInChildren<Animator>();
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _wallCheckRadiusDown = _wallCheckDown.GetComponentInChildren<CircleCollider2D>().radius;
        _sliderPosition = GameObject.FindGameObjectWithTag("SliderPosition").GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        LedgeGo();
        CanMove();
    }

    private void FixedUpdate()
    {
        CheckingLedge();
        CheckingWall();
    }

    private void CanMove()
    {
        if (_onWall && _onLedge && _blockMoveXYforLedge)
        {
            _playerMove.runSpeed = 0;
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(0, 0);
        }
        else
        {
            _playerMove.runSpeed = 5f;
            _rb.gravityScale = 1f;
        }
    }

    private void CheckingWall()
    {
        _onWallUp = Physics2D.Raycast(_wallCheckUp.position, new Vector2(transform.localScale.x, 0), _wallCheckRayDistance, _wall);
        _onWalldown = Physics2D.OverlapCircle(_wallCheckDown.position, _wallCheckRadiusDown, _wall);
        _onWall = (_onWallUp && _onWalldown);
        _anim.SetBool("onWall", _onWall);
    }

    private void CheckingLedge()
    {
        if (_onWallUp)
        {
            _onLedge = !Physics2D.Raycast
             (
                 new Vector2(_wallCheckUp.position.x, _wallCheckUp.position.y + _ledgeRayCorrectY),
                 new Vector2(transform.localScale.x, 0),
                 _wallCheckRayDistance,
                 _wall
             );
        }
        else { _onLedge = false; }

        _anim.SetBool("onLedge", _onLedge);

        if (_onLedge)
        {
            _rb.gravityScale = 0;
            _rb.velocity = new Vector2(0, 0);
            offsetCalculateAndCorrect();
            _anim.SetBool("IsJump", false);
        }
    }

    private void LedgeGo()
    {
        _blockMoveXYforLedge = true;

        if (_onLedge && Input.GetKeyDown(KeyCode.E))
        {
            _anim.Play("ClimbAnimation");
        }
    }

    private void FinishLedge() //добавляется в event animation
    {
        transform.position = new Vector3(_finishLedgePosition.position.x, _finishLedgePosition.position.y, _finishLedgePosition.position.z);
        _healthBarCanvas.transform.position = _sliderPosition.transform.position;
        _anim.Play("IdleAnimation");
        _blockMoveXYforLedge = false;
    }

    private void offsetCalculateAndCorrect()
    {
        _offsetY = Physics2D.Raycast
        (
            new Vector2(_wallCheckUp.position.x + _wallCheckRayDistance * transform.localScale.x, _wallCheckUp.position.y + _ledgeRayCorrectY),
            Vector2.down,
            _ledgeRayCorrectY,
            _wall
        ).distance;

        if(_offsetY > _minCorrectdistance * 1.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - _offsetY + _minCorrectdistance, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_wallCheckUp.position, new Vector2(_wallCheckUp.position.x + _wallCheckRayDistance * transform.localScale.x, _wallCheckUp.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine
        (
            new Vector2(_wallCheckUp.position.x, _wallCheckUp.position.y + _ledgeRayCorrectY),
            new Vector2(_wallCheckUp.position.x + _wallCheckRayDistance * transform.localScale.x, _wallCheckUp.position.y + _ledgeRayCorrectY)
        );

        Gizmos.color = Color.green;
        Gizmos.DrawLine
        (
            new Vector2(_wallCheckUp.position.x + _wallCheckRayDistance * transform.localScale.x, _wallCheckUp.position.y + _ledgeRayCorrectY),
            new Vector2(_wallCheckUp.position.x + _wallCheckRayDistance * transform.localScale.x, _wallCheckUp.position.y)
        );
    }
}
