using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _scoreValue = 0;
    [SerializeField] private List<GameObject> _enemy = new List<GameObject>() { };
    [SerializeField] private List<GameObject> _apple = new List<GameObject>() { };
    private GameObject[] _enemyArray;
    private GameObject[] _appleArray;
    public TMP_Text _score;
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        _appleArray = GameObject.FindGameObjectsWithTag("Apple");
        _enemy.AddRange(_enemyArray);
        _apple.AddRange(_appleArray);
    }

    private void Start()
    {
        _score.text = "Collect " + _scoreValue.ToString() + " points";
    }

    private void Update()
    {
        AddPoint();
    }

    private void AddPoint()
    {

        foreach (GameObject enemy in _enemy)
        {
            if (enemy == null)
            {
                _scoreValue += 2;
                _score.text = "Collect " + _scoreValue.ToString() + " points";
                _enemy = _enemy.Where(e => e != null).ToList();
            }

        }

        foreach (GameObject apple in _apple)
        {
            if (apple == null)
            {
                _scoreValue += 1;
                _score.text = "Collect " + _scoreValue.ToString() + " points";
                _apple = _apple.Where(a => a != null).ToList();
            }

        }
    }
}
