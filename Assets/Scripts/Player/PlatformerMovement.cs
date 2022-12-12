using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isOnPlatform;
    private Rigidbody2D _platformRB;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_isOnPlatform)
        {
            _rb.velocity = _rb.velocity + _platformRB.velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            _platformRB = col.gameObject.GetComponent<Rigidbody2D>();
            _isOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            _isOnPlatform = false;
            _platformRB = null;
        }
    }
}
