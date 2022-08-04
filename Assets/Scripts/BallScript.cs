using System;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Transform _gameZone;
    public float BallMass;
    public float BallScale;
    public Color32 BallColor;

    public event Action AddGold;
    public event Action<int> UpgradeHighScore;
    public event Action LevelFailed;

    private int _highScore;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    public void SetProperties(float mass, float size, Color32 color)
    {
        _rigidbody2D.mass = mass;
        transform.localScale = Vector3.one * size;
        GetComponent<SpriteRenderer>().color = color;
    }

    private void FixedUpdate()
    {
        if (transform.position.y > _highScore && this.enabled)
        {
            _highScore = (int)transform.position.y;
            UpgradeHighScore(_highScore);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Coin"))
        {
            AddGold?.Invoke();
        }
        if (collisionGameObject.CompareTag("GameOverTrigger"))
        {
            LevelFailed?.Invoke();
            Destroy(gameObject, 1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("UpperLimit"))
        {
            _gameZone.position = Vector3.Lerp(_gameZone.position, new Vector3(0, _gameZone.position.y + 1, 0), .1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovementStick"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y + collision.gameObject.GetComponent<SticksMovement>().DragSpeed * 5);
        }
    }
}
