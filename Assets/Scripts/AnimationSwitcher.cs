using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private string _parameterRight = "run_right";
    private string _parameterLeft = "run_left";

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void SwitchAnimator(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            _animator.SetBool(_parameterLeft, false);
            _animator.SetBool(_parameterRight, true);
        }
        else if (target.position.x < transform.position.x)
        {
            _animator.SetBool(_parameterRight, false);
            _animator.SetBool(_parameterLeft, true);
        }
        else
        {
            _animator.SetBool(_parameterLeft, false);
            _animator.SetBool(_parameterRight, false);
        }
    }

    public void SwitchAnimator(bool rightRun, bool leftRun)
    {
        _animator.SetBool(_parameterLeft, leftRun);
        _animator.SetBool(_parameterRight, rightRun);
    }
}
