using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioSource audioSource;
    public AudioClip[] soundButton;
    public AudioClip soundLabelOff;
    public AudioClip soundLabelOn;
    public AudioClip soundOrbitView;
    public AudioClip[] soundSelect;

    [Header("Audio Variables")]
    public float volumeSFX;

    public void PlaySoundButton()
    {
        audioSource.PlayOneShot(soundButton[Random.Range(0, soundButton.Length)], volumeSFX);
    }

    public void PlaySoundLabelOff()
    {
        audioSource.PlayOneShot(soundLabelOff, volumeSFX);
    }

    public void PlaySoundLabelOn()
    {
        audioSource.PlayOneShot(soundLabelOn, volumeSFX);
    }

    public void PlaySoundOrbitView()
    {
        audioSource.PlayOneShot(soundOrbitView, volumeSFX);
    }

    public void PlaySoundSelect()
    {
        audioSource.PlayOneShot(soundSelect[Random.Range(0, soundButton.Length)], volumeSFX);
    }
}
