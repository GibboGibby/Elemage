using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GibAudio
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip audio;
    [SerializeField] public float volume;
}

public class SpellAudio : MonoBehaviour
{
    struct AudioStruct
    {
        public AudioClip audioClip;
        public float volume;

        public AudioStruct(AudioClip clip, float vol)
        {
            audioClip = clip;
            volume = vol;
        }
    }


    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Transform playerPos;

    [SerializeField] private List<GibAudio> audioClips = new List<GibAudio>();

    private Dictionary<string, AudioStruct> m_audioClips = new Dictionary<string, AudioStruct>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in  audioClips)
        {
            m_audioClips.Add(item.name, new AudioStruct(item.audio, item.volume));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundName)
    {
        //AudioSource.clip = m_audioClips[soundName];
        if (!m_audioClips.ContainsKey(soundName)) { Debug.Log("spell audio not found"); return; }
        AudioSource.PlayClipAtPoint(m_audioClips[soundName].audioClip, playerPos.position, m_audioClips[soundName].volume);
    }
}
