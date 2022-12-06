using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private SliderJoint2D _slider;
    private JointMotor2D _motor;

    private void Start()
    {
        _slider = GetComponent<SliderJoint2D>();
        _motor = _slider.motor;
    }


    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if(_slider.limitState == JointLimitState2D.LowerLimit)
        {
            _motor.motorSpeed = 0.5f;
            _slider.motor = _motor;
        }

        if(_slider.limitState == JointLimitState2D.UpperLimit)
        {
            _motor.motorSpeed = -0.5f;
            _slider.motor = _motor;
        }

    }
}
