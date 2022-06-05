using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private float _duration = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(Ring());
        }
    }

    IEnumerator Ring()
    {
        WaitForSeconds wait = new WaitForSeconds(_duration);
        _audio.Play();
        yield return wait;
        Destroy(gameObject);
    }
}
