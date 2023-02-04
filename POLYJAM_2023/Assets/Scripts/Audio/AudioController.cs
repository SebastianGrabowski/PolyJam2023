using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private float _Volume;
    public float Volume
    {
        get => _Volume;
        set
        {
            value = Mathf.Clamp(value, 0.0f, 1.0f);
            _Volume = value;
            _MusicSource.volume = value;
        }
    }

    [SerializeField]private AudioSource _MusicSource;

    public static AudioController Instance;
    private void Start()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        StartCoroutine(PlayUpdate(clip));
    }

    private IEnumerator PlayUpdate(AudioClip clip)
    {
        var newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.Play();
        yield return new WaitForSeconds(clip.length);
        Destroy(newSource);
    }
}
