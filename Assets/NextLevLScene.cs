using UnityEngine;

public class NextLevLScene : MonoBehaviour
{
    private GameOver _gameOver;
    private Rigidbody2D _platform;
    private Rigidbody2D _player;

    private void Awake()
    {
        _gameOver = GameObject.FindGameObjectWithTag("LevelCompleted").GetComponent<GameOver>();
        _platform = gameObject.GetComponentInParent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Collider2D>().CompareTag("Player"))
        {
            if (_platform != null)
            {
                _platform.bodyType = RigidbodyType2D.Static;

            }
            _player.bodyType = RigidbodyType2D.Static;
            _gameOver.CanvasActive();
        }
    }
}
