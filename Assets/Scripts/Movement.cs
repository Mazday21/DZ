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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionDir = collision.transform.position - transform.position;
        float angleCollision = Vector3.Angle(collisionDir, transform.forward);

        if (angleCollision > 45f && angleCollision < 135f)
        {
            _grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            _grounded = false;
    }

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _animator.SetBool("run_right", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * _inversion, 0, 0);
            _animator.SetBool("run_left", true);
        }
        else
        {
            _animator.SetBool("run_left", false);
            _animator.SetBool("run_right", false);
        }

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }
}
