using UnityEngine;

public class WagonMove : MonoBehaviour
{
    [SerializeField] private GameObject _wagon;
    [SerializeField] private WheelJoint2D[] _wagonWheels;

    void Start()
    {
        _wagon = GameObject.FindGameObjectWithTag("Wagon");
        _wagonWheels = _wagon.GetComponentsInChildren<WheelJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i < _wagonWheels.Length; i++)
                _wagonWheels[i].useMotor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i < _wagonWheels.Length; i++)
                _wagonWheels[i].useMotor = false;
        }
    }
}
