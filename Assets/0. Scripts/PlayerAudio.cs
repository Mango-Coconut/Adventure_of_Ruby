using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    enum States
    {
        Walk,
        Attack,
        Hit
    }
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WalkPlay()
    {
        audioSource.loop = true;
        audioSource.clip = clips[((int)States.Walk)];
        audioSource.Play();
    }
    public void WalkStop()
    {
        audioSource.Stop();
    }
    public void AttackPlay()
    {
        audioSource.PlayOneShot(clips[((int)States.Attack)]);
    }

    public void HitPlay()
    {
        audioSource.PlayOneShot(clips[((int)States.Hit)]);
    }
}
