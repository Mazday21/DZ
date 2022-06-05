using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPoint;
    private Animator _animator;

    private void Start()
    {
        _points = new Transform[_path.childCount];
        _animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];

        var direction = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }
        SwitchAnimator(target);
    }

    private void SwitchAnimator(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            _animator.SetBool("run_left", false);
            _animator.SetBool("run_right", true);
        }
        else if (target.position.x < transform.position.x)
        {
            _animator.SetBool("run_right", false);
            _animator.SetBool("run_left", true);
        }
        else
        {
            _animator.SetBool("run_left", false);
            _animator.SetBool("run_right", false);
        }
    }
}


