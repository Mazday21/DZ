using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private bool _isEnter = false;
    private AudioSource _alarm;
    private Coroutine _volumeChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (!_isEnter)
            {
                StopCoroutine();
                _volumeChange = StartCoroutine(SoundIncrease());
                _alarm.Play();
                _isEnter = true;
                _sprite.color = Color.black;
            }
            else
            {
                StopCoroutine();
                _volumeChange = StartCoroutine(SoundDecrease());
                _isEnter = false;
                _sprite.color = Color.white;
            }
        }
    }

    private void StopCoroutine()
    {
        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }
    }

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = 0;
    }

    private IEnumerator SoundIncrease()
    {
        var waitForSecond = new WaitForSeconds(0.1f);

        for (float i = _alarm.volume; i < 1; i += 0.05f)
        {
            _alarm.volume = i;
            yield return waitForSecond;
        }
    }

    private IEnumerator SoundDecrease()
    {
        var waitForSecond = new WaitForSeconds(0.1f);

        for (float i = _alarm.volume; i > 0.001f; i -= 0.05f)
        {
            _alarm.volume = i;
            if (_alarm.volume < 0.1f)
            {
                _alarm.Stop();
            }
            yield return waitForSecond;
        }
    }
}
