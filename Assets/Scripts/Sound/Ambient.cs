using UnityEngine;

public class Ambient : MonoBehaviour
{
    [SerializeField]
    TriggerZone zone;
    [SerializeField]
    // should be put on audio listner
    AudioSource ambientSource;
    
    [SerializeField]
    AudioClip ambience;
    public bool IsPlaying { get; private set; }

    public AudioClip Ambience
    {
        set
        {
            ambience = value;
            if (IsPlaying)
            {
                ambientSource.clip = ambience;
                ambientSource.Play();
            }
        }
        get => ambience;
    }

    private void Awake()
    {
        IsPlaying = false;
        zone.OnEnter += enter =>
        {
            if (enter.transform.GetComponentInChildren<AudioListener>() != null)
            {
                IsPlaying = true;
                ambientSource.clip = ambience;
                ambientSource.Play();
            }
        };
        zone.OnExit += enter =>
        {
            if (enter.transform.GetComponentInChildren<AudioListener>() != null)
            {
                IsPlaying = false;
                if (ambientSource.clip == ambience)
                    ambientSource.Stop(); 
            }
        };
    }
}