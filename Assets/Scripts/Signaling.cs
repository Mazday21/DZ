using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private bool IsEnter = false;
    private AudioSource _alarm;
    private Coroutine _soundIncreaseCoroutine;
    private Coroutine _soundDecreaseCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (!IsEnter)
            {
                if(_soundDecreaseCoroutine != null)
                {
                    StopCoroutine(_soundDecreaseCoroutine);
                }
                _alarm.Play();
                IsEnter = true;
                _sprite.color = Color.black;
                _soundIncreaseCoroutine = StartCoroutine(SoundIncrease());
            }
            else
            {
                if (_soundIncreaseCoroutine != null)
                {
                    StopCoroutine(_soundIncreaseCoroutine);
                }
                IsEnter = false;
                _sprite.color = Color.white;
                _soundDecreaseCoroutine = StartCoroutine(SoundDecrease());
            }
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
            if(_alarm.volume < 0.1f)
            {
                _alarm.Stop();
            }
            yield return waitForSecond;
        }
    }
}
