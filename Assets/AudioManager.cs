using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject audioManagerObject = new GameObject("AudioManager");
                instance = audioManagerObject.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }
}