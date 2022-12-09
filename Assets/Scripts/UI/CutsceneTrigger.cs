using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private Animator _camAnim;
    [SerializeField] private float _time = 10f;
    public static bool isCutsceneOn;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;


    private void Start()
    {
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _playerAnimator = _playerRigidbody.GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Collider2D>().CompareTag("Player"))
        {
            _playerRigidbody.bodyType = RigidbodyType2D.Static;
            _playerAnimator.enabled = false;
            isCutsceneOn = true;
            _camAnim.SetBool("Cutscene1", true);
            if (CinematicBarsController.Instance != null)
                CinematicBarsController.Instance.ShowBars();
            Invoke(nameof(StopCutscene), _time);
        }
    }

    private void StopCutscene()
    {
        _playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _playerAnimator.enabled = true;
        isCutsceneOn = false;
        _camAnim.SetBool("Cutscene1", false);
        if (CinematicBarsController.Instance != null)
            CinematicBarsController.Instance.HideBars();
        Destroy(gameObject);
    }
}
