using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private Health _playerHealth;
    private float _damage = 2f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _playerHealth = _player.GetComponent<Health>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Collider2D>().CompareTag("Player"))
        {
            _playerHealth.TakeHit(_damage);
            StartCoroutine(Knockback(0.02f, 150, _player.transform.position));
        }
    }

    private IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        if(_player.bodyType != RigidbodyType2D.Static)
        {
            float timer = 0;
            _player.velocity = new Vector2(_player.velocity.x, 0);

            while (knockDur > timer)
            {

                timer += Time.deltaTime;

                _player.AddForce(new Vector3(knockbackDir.x * Random.Range(-50, 50), knockbackDir.y + knockbackPwr, transform.position.z));

            }

            yield return 0;
        }
       
    }
}
