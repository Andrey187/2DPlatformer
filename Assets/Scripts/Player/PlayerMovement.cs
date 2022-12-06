using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	private bool _isJump = false;
	private PlayerController _controller;
	public float runSpeed = 40f;
	public float horizontalMove = 0f;

    private void Start()
    {
		_controller = gameObject.GetComponent<PlayerController>();
	}

    private void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		_animator.SetFloat("speedX", Mathf.Abs(horizontalMove));
		_animator.SetFloat("yVelocity", _controller._rb.velocity.y);

		Jump();
	}

	public void OnLanding()
    {
		_animator.SetBool("IsJump", false);
    }

	private void FixedUpdate()
	{
		if(_controller._rb.bodyType != RigidbodyType2D.Static)
        {
			Movement();
		}
	}

	private void Jump()
    {
		if (Input.GetKeyDown(KeyCode.Space) && _controller._isGrounded == true)
		{
			_isJump = true;
			_animator.SetBool("IsJump", true);
		}
	}

    private void Movement()
    {
		_controller.Move(horizontalMove * Time.fixedDeltaTime, _isJump);
		_isJump = false;
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            _isJump = false;
            _animator.SetBool("IsJump", false);
        }
    }
}
