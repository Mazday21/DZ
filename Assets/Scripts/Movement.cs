using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce = 0.002f;

    private bool _grounded;
    private int _inversion = -1;
    private float _firstAngleDown = 45f;
    private float _secondAngleDown = 135f;
    private AnimationSwitcher _animationSwitcher;
    private bool _rightRun = false;
    private bool _leftRun = false;

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

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

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
        if (collision.collider.TryGetComponent<Ground>(out Ground ground))
        {
            Vector3 collisionDir = collision.transform.position - transform.position;
            float angleCollision = Vector3.Angle(collisionDir, transform.forward);

            if (angleCollision > _firstAngleDown && angleCollision < _secondAngleDown)
            {
                return true;
            }
        }
        return false;
    }
}
