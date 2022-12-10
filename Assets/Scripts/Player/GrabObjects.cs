using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private GameObject _box;
    private bool _hold;
    private RaycastHit2D _hitInfo;
    private IPickable pickableItem;

    private void Update()
    {
        Grab();
    }

    private void FixedUpdate()
    {
        StaticBox();
    }

    private void Grab()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!_hold)
            {
                Physics2D.queriesStartInColliders = false;
                _hitInfo = Physics2D.Raycast(_rayPoint.transform.position, Vector2.right * transform.localScale.x, _distance, _hitMask);

                if (_hitInfo.collider != null && _hitInfo.collider.tag == "Object")
                {
                    pickableItem = _hitInfo.collider.GetComponent<IPickable>();
                    if (pickableItem != null)
                    {
                        _hold = true;
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            _hold = false;
        }
    }

    private void StaticBox()
    {
        switch (_hold)
        {
            case true:
                _hitInfo.collider.gameObject.transform.position = _holdPoint.position;
                _box = _hitInfo.collider.gameObject;
                _box = pickableItem.PickUp();
                break;

            case false:
                if (_box == null) return;
                _box = pickableItem.PickDown();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(_rayPoint.transform.position, _rayPoint.transform.position + Vector3.right * transform.localScale.x * _distance);
    }
}
