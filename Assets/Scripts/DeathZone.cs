using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private GameOver _gameOver;
    private Rigidbody2D _player;

    private void Awake()
    {
        _gameOver = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameOver>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Collider2D>().CompareTag("Player"))
        {
            _player.bodyType = RigidbodyType2D.Static;
            Animator _playerAnimator = _player.gameObject.GetComponentInChildren<Animator>();
            _playerAnimator.SetTrigger("Death");
            _gameOver.CanvasActive();
        }
    }
}
