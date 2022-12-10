using UnityEngine;

public class PickableObjects : MonoBehaviour, IPickable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _rbColliderGround;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rbColliderGround = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
    }
    public GameObject PickUp()
    {
        if (_rb != null)
        {
            _rb.GetComponent<FixedJoint2D>().enabled = true;
            _rb.GetComponent<FixedJoint2D>().connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.GetComponent<BoxCollider2D>().enabled = false;
            _rbColliderGround.enabled = false;
        }
        return this.gameObject;
    }

    public GameObject PickDown()
    {
        if (_rb != null)
        {
            _rb.GetComponent<FixedJoint2D>().enabled = false;
            _rb.GetComponent<BoxCollider2D>().enabled = true;
            _rbColliderGround.enabled = true;

            if (_rb.velocity.y == 0 && _rb.velocity.x == 0)
            {
                _rb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            Debug.Log(_rb.velocity.y);
        }
        return this.gameObject;
    }
}
