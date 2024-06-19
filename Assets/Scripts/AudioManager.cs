using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip GrowClip;
    public AudioClip ShrinkClip;
    public AudioClip DieClip;
    public AudioClip StartPowerClip;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestory()
    {
        if (Instance == this) {
            Instance = null;
        }
    }
}
