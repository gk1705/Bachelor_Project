using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeResponder : MonoBehaviour
{
    [SerializeField] private GameObject _switcher;
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _duration;

    public void TriggerResponse(string responseText)
    {
        StopAllCoroutines();
        _textMeshPro.text = responseText;
        StartCoroutine(ShowResponse());
    }

    private IEnumerator ShowResponse()
    {
        _switcher.SetActive(true);
        PlayParticles();
        PlayAudio();
        yield return new WaitForSeconds(_duration);
        _switcher.SetActive(false);
    }

    private void PlayParticles()
    {
        if (_particleSystem.isPlaying)
            _particleSystem.Stop();
        _particleSystem.Play();
    }

    private void PlayAudio()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.Play();
    }
}
