using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    Rigidbody2D _rb;
    CinemachineVirtualCamera _camera;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _camera.Follow = this.gameObject.transform;
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _rb.velocity = new Vector2(x, y) * _moveSpeed;
        //_camera.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
    }
}
