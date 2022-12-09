using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Canvas _canvasSlider;
    [SerializeField] private Color _low;
    [SerializeField] private Color _high;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _sliderPosition;

    private void Awake()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        if(_sliderPosition != null) _canvasSlider.transform.position = _sliderPosition.transform.position;
    }

    private void LateUpdate()
    {
        _slider.transform.LookAt(transform.position + _cam.forward);
    }

    public void SetHealth(float health, float maxHealth)
    {
        _slider.gameObject.SetActive(health < maxHealth);
        _slider.value = health;
        _slider.maxValue = maxHealth;

        _slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_low, _high, _slider.normalizedValue);
    }
}
