using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    private readonly float _firstAngleDown = 45f;
    private readonly int _inversion = -1;
    private readonly float _secondAngleDown = 135f;
    private AnimationSwitcher _animationSwitcher;

    private bool _grounded;
    private bool _leftRun;
    private bool _rightRun;

    private void Start()
    {
        _animationSwitcher = gameObject.GetComponent<AnimationSwitcher>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _leftRun = false;
            _rightRun = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * _inversion, 0, 0);
            _rightRun = false;
            _leftRun = true;
        }
        else
        {
            _rightRun = false;
            _leftRun = false;
        }

        if (Input.GetKey(KeyCode.Space) && _grounded) _rigidbody2D.velocity = Vector2.up * _jumpForce;

        if (_rigidbody2D.velocity.y < 0)
            _rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * Time.deltaTime * (_fallMultiplier - 1));

        else if (_rigidbody2D.velocity.y > 0)
            _rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * Time.deltaTime * (_fallMultiplier - 1));

        _animationSwitcher.SwitchAnimator(_rightRun, _leftRun);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _grounded = ToCheckAngleGround(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _grounded = !ToCheckAngleGround(collision);
    }

    private bool ToCheckAngleGround(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            var collisionDir = collision.transform.position - transform.position;
            if (transform != null)
            {
                var angleCollision = Vector3.Angle(collisionDir, transform.forward);

                if (angleCollision > _firstAngleDown && angleCollision < _secondAngleDown) return true;
            }
        }

        return false;
    }
}