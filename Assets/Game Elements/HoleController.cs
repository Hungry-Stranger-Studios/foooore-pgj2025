using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public delegate void Win();
    public event Win onWin;

    [SerializeField] Color winColor;    //upon the ball hitting the hole, its beacon will change to this color
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clapClip;
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();  

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPersistentClapSound(); // Play clapping sound across scenes
            GetComponentInChildren<Renderer>().material.color = winColor;
            onWin?.Invoke();
        }
    }

    private void PlayPersistentClapSound()
    {
        GameObject audioObject = new GameObject("PersistentClapSound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clapClip;
        audioSource.Play();
        
        DontDestroyOnLoad(audioObject); // Keep playing after scene change
        Destroy(audioObject, clapClip.length); // Remove object after sound finishes
    }
}
