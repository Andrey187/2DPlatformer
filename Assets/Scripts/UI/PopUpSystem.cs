using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject _popUpBox;
    [SerializeField] private Animator _anim;
    [SerializeField] private TMP_Text _popUpText;
    private TMP_Text _score;
    private int _nextSceneLoad;

    private void Start()
    {
        _nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        _score = ScoreManager.instance._score;
    }

    public void PopUp(string text)
    {
        _popUpBox.SetActive(true);
        _popUpText.text = text + "\n\n" + _score.text;
        _anim.SetTrigger("pop");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLvL()
    {
        SceneManager.LoadScene(_nextSceneLoad);
    }
}
