using UnityEngine;

public class SticksMovement : MonoBehaviour
{
    public float DragSpeed;


    private const float kVelocityMultiplier = 2;

    private Vector2 _defaultPosition;

    private float _velocityY;
    private float _recentYPosition;
    private void Awake()
    {
        _defaultPosition = transform.localPosition;
    }
    public void OnMouseDrag()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Clamp(mousePos.y-transform.parent.position.y, -Screen.height / 2, Screen.height));

        DragSpeed = GetMouseY();
    }
    public void OnMouseUp()
    {
        transform.localPosition = _defaultPosition;
    }

    private void LateUpdate()
    {
        _velocityY = transform.position.y - _recentYPosition;
        _recentYPosition = transform.position.y;

    }

    private float GetMouseY()
    {
        return Input.GetAxis("Mouse Y");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = collision.transform.GetComponent<Rigidbody2D>();
        if (rigidbody2D)
        {
            Vector2 forceDirection = rigidbody2D.velocity.normalized;
            rigidbody2D.AddForce(forceDirection * (1 + _velocityY) * kVelocityMultiplier, ForceMode2D.Impulse);
        }
    }
}
