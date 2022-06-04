using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(Ring());
        }
    }

    IEnumerator Ring()
    {
        _audio.Play();
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
