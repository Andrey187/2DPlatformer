using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _alphaObj;
    [SerializeField] private Image _alphaImage;
    [SerializeField] private int _alpha;
    [SerializeField] private GameObject[] _water;
    [SerializeField] private string _pop;
    //private PopUpSystem _popUp;
    private GameObject _particleFire;
    private SpriteRenderer _player;
    private Canvas _gameOver;
    private bool _canvasActive = false;

    private void Awake()
    {
        _alphaImage = _alphaObj.GetComponent<Image>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        _gameOver = gameObject.GetComponent<Canvas>();
        _water = GameObject.FindGameObjectsWithTag("Water");
        _particleFire = GameObject.FindGameObjectWithTag("Fire");
        //_popUp = GameObject.FindGameObjectWithTag("PopUp").GetComponent<PopUpSystem>();
    }

    private void Start()
    {
        _gameOver.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_alpha == 0 && _canvasActive == true)
        {
            _player.sortingLayerName = "UI";
            _alphaImage.color = new Color(_alphaImage.color.r, _alphaImage.color.g, _alphaImage.color.b, _alphaImage.color.a + 0.5f * Time.deltaTime);
            if(_alphaImage.color.a >= 1.0f)
            {
                _alpha = 1;
            }
            StartCoroutine(RestartButton());
        }
    }

    private IEnumerator RestartButton()
    {
        yield return new WaitForSeconds(2f);
        //_popUp.PopUp(_pop);
        DisableObjects();
    }

    public void CanvasActive()
    {
        _gameOver.gameObject.SetActive(true);
        _canvasActive = true;
    }

    private void DisableObjects()
    {
        if (_water != null && _particleFire != null)
        {
            foreach (GameObject a in _water)
            {
                MeshRenderer _waterRenderer = a.GetComponent<MeshRenderer>();
                _waterRenderer.enabled = false;
            }

            ParticleSystem _firePatricle = _particleFire.GetComponent<ParticleSystem>();
            _firePatricle.Stop();
        }
    }
}
