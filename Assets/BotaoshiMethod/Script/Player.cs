using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (GameManager.Instance.IsGame)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            _rb.velocity = new Vector2(x, y) * _moveSpeed;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IAction>().Action();
    }
}
