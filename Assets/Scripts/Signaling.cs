using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private bool IsEnter = false;
    private AudioSource _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (!IsEnter)
            {
                StopCoroutine(SoundReducer());
                _alarm.Play();
                IsEnter = true;
                _sprite.color = Color.black;
                StartCoroutine(SoundMagnifier());
            }
            else
            {
                StopCoroutine(SoundMagnifier());
                IsEnter = false;
                _sprite.color = Color.white;
                StartCoroutine(SoundReducer());
            }
        }
    }

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
    }

    private IEnumerator SoundMagnifier()
    {
        var waitForSecond = new WaitForSeconds(0.1f);

        for (float i = 0; i < 1; i += 0.05f)
        {
            _alarm.volume = i;
            yield return waitForSecond;
        }
    }

    private IEnumerator SoundReducer()
    {
        var waitForSecond = new WaitForSeconds(0.1f);

        for (float i = 1f; i > 0.001f; i -= 0.05f)
        {
            _alarm.volume = i;
            if(_alarm.volume < 0.1f)
            {
                _alarm.Stop();
            }
            yield return waitForSecond;
        }
    }
}
