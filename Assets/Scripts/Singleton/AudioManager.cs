using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct Sound
{
    public string name;
    public List<AudioClip> sounds;
}

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup soundEffectMixer;

    [SerializeField] private List<Sound> musics;

    private int musicIndex = 0;
    private Dictionary<string, List<AudioClip>> allMusics = new Dictionary<string, List<AudioClip>>();

    private string currentMusicIndex;
    public void Init()
    {
        foreach (Sound music in musics)
        {
            allMusics.Add(music.name, music.sounds);
        }
        audioSource.clip = allMusics["Menu"][0];
        audioSource.Play();
    }

    private void Update()
    {
        /*if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }*/
    }

    private void PlayNextSong()
    {
        musicIndex = Random.Range(0, allMusics[currentMusicIndex].Count);
        audioSource.clip = allMusics[currentMusicIndex][musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject TempGO = new GameObject("TempAudio");
        TempGO.transform.position = pos;
        AudioSource audio = TempGO.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.outputAudioMixerGroup = soundEffectMixer;
        audio.Play();
        Destroy(TempGO, clip.length);
        return audioSource;
    }

    public void PlayMusic(string musicName)
    {
        if (allMusics.ContainsKey(musicName))
        {
            audioSource.Stop();
            audioSource.clip = allMusics[musicName][Random.Range(0, allMusics[musicName].Count)];
            currentMusicIndex = musicName;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"{musicName} wasn't found, check the list of all musics");
        }
    }

}






















/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{


    public void PlayMusic(string nameOfMusic, Vector3 position)
    {
        if (allMusics.ContainsKey(nameOfMusic))
        {
            allMusics[nameOfMusic][Random.Range(0, allMusics[nameOfMusic].Count)]
            allMusics[nameOfMusic][Random.Range(0, allMusics[nameOfMusic].Count)].Play();
        } else
        {
            Debug.LogWarning($"{nameOfMusic} wasn't found, check the list of all musics");
        }
    }

    public void PlayClip(string nameOfClip, Vector3 position)
    {
        if (allAudioClip.ContainsKey(nameOfClip))
        {
            allAudioClip[nameOfClip][Random.Range(0, allAudioClip[nameOfClip].Count)].transform.position = position;
            allAudioClip[nameOfClip][Random.Range(0, allAudioClip[nameOfClip].Count)].Play();
        }
        else
        {
            Debug.LogWarning($"{nameOfClip} wasn't found, check the list of all musics");
        }
    }
}


*/