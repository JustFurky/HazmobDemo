using UnityEngine;

public class SticksMovement : MonoBehaviour
{
    private const float kVelocityMultiplier = 5;

    private Vector2 _defaultPosition;
    public float DragSpeed;

    private float _velocityY;
    private float _recentYPosition;
    private void Awake()
    {
        _defaultPosition = transform.position;
    }
    public void OnMouseDrag()
    {
       
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(transform.position.x, Mathf.Clamp(mousePos.y, -Screen.height/2, Screen.height));
        
        DragSpeed = GetMouseY();
    }
    public void OnMouseUp()
    {
        transform.position = _defaultPosition;
    }

    private void LateUpdate()
    {
        _velocityY = transform.position.y - _recentYPosition;
        Debug.Log(_velocityY);
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
            Debug.Log(forceDirection);
            rigidbody2D.AddForce(-forceDirection * (1 + _velocityY) * kVelocityMultiplier, ForceMode2D.Impulse);
        }
    }
}
