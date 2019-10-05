using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // include so we can load new scenes

public class CharacterController2D : MonoBehaviour
{

    [Range(0.0f, 10.0f)]
    public float moveSpeed = 3f;

    public int playerHealth = 1;

    public LayerMask whatIsGround;

    public Transform groundCheck;

    [HideInInspector]
    public bool playerCanMove = true;

    Transform _transform;
    Rigidbody2D _rigidbody;

    float _vx;
    float _vy;

    bool facingRight = true;

    private bool _openShop = false;

    [SerializeField]
    private GameObject _talkToast;
    [SerializeField]
    private GameObject _shop;

    void Awake()
    {
        _transform = GetComponent<Transform>();

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
            Debug.LogError("Rigidbody2D component missing from this gameobject");

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!playerCanMove || (Time.timeScale == 0f))
            return;

        _vx = Input.GetAxisRaw("Horizontal");
        _vy = _rigidbody.velocity.y;

        if (Input.GetButtonUp("Jump") && _vy > 0f)
        {
            _vy = 0f;
        }
        _rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);


    }

    void LateUpdate()
    {
        Vector3 localScale = _transform.localScale;

        if (_vx > 0)
        {
            facingRight = true;
        }
        else if (_vx < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        _transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }

}
