using UnityEngine;

[ExecuteInEditMode]
public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxEffect;
    [SerializeField] private Material _material;
    
    private Vector2 _offset;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = transform.root;
    }

    private void LateUpdate()
    {
        _offset = new Vector2(_cameraTransform.position.x * (_parallaxEffect.x / 100), _cameraTransform.position.y * (_parallaxEffect.y / 100));
        _material.mainTextureOffset = _offset;
    }
}
