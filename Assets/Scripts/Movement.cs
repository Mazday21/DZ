using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private int _jumpForce = 50;

    private Animator _animator;
    private bool _grounded;
    private int _inversion = -1;
    private float _firstAngleDown = 45f;
    private float _secondAngleDown = 135f;
    private string _parameterRight = "run_right";
    private string _parameterLeft = "run_left";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _grounded = ToCheckAngleGround(collision);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        _grounded = !ToCheckAngleGround(collision);
    }


    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _animator.SetBool(_parameterRight, true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * _inversion, 0, 0);
            _animator.SetBool(_parameterLeft, true);
        }
        else
        {
            _animator.SetBool(_parameterLeft, false);
            _animator.SetBool(_parameterRight, false);
        }

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
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
