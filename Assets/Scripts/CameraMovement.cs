using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private bool _isMatch = false;

    private void Update()
    {
        if(_player.position.x >= transform.position.x)
        {
            _isMatch = true;
        }

        if (_isMatch)
        {
            Vector3 target = new Vector3(_player.position.x, transform.position.y, transform.position.z);
            transform.position = target;
        }
    }
}
