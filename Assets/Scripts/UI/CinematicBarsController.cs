using System.Collections;
using TMPro;
using UnityEngine;

public class CinematicBarsController : MonoBehaviour
{
    public static CinematicBarsController Instance { get; private set; }
    [SerializeField] private GameObject _cinematicBarContatinerGo;
    [SerializeField] private Animator _cinematicBarsAnimator;
    [SerializeField] private Canvas _canvasScore;
    [SerializeField] private Canvas _canvasInfo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    public void ShowBars()
    {
        _cinematicBarContatinerGo.SetActive(true);
        _canvasScore.gameObject.SetActive(false);
        _canvasInfo.gameObject.SetActive(false);
    }

    public void HideBars()
    {
        if (_cinematicBarContatinerGo.activeSelf)
            StartCoroutine(HideBarsAndDisableGo());
    }

    private IEnumerator HideBarsAndDisableGo()
    {
        _cinematicBarsAnimator.SetTrigger("HideBars");

        yield return new WaitForSeconds(0.5f);
        _cinematicBarContatinerGo.SetActive(false);
        _canvasScore.gameObject.SetActive(true);
        _canvasInfo.gameObject.SetActive(true);
    }
}
