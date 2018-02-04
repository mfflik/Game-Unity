using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static List<GameObject> sound, music;

    private static AudioManager _Instance = null;

    public Dictionary<int, AudioClip> SoundList, MusicList;

    public const int SOUND_GAME_START = 0;
    public const int SOUND_BUTTON_CLICK = 1;
    public const int SOUND_GAME_END = 2;
    public const int SOUND_TARGET_SUCCESS = 3;
    public const int SOUND_TARGET_WRONG = 4;
    public const int SOUND_ENERGY_LOW = 5;
    public const int SOUND_NEW_SCORE_1 = 6;
    public const int SOUND_NEW_SCORE_2 = 7;
    public const int SOUND_DAY_AMBULANCE = 8;
    public const int SOUND_DAY_CAT = 9;
    public const int SOUND_DAY_DOG = 10;
    public const int SOUND_NIGHT_CROW = 11;
    public const int SOUND_NIGHT_KUNTI = 12;
    public const int SOUND_NIGHT_OWL = 13;
    public const int SOUND_NIGHT_WOLF = 14;

    public const int AMBIENT_WALKING = 0;
    public const int MUSIC_GAMEPLAY = 1;
    public const int MUSIC_MENU = 2;

    public static AudioManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject ui = new GameObject("AudioManager");
                sound = new List<GameObject>();
                music = new List<GameObject>();
                for (int i = 0; i <= 10; i++)
                {
                    sound.Add(new GameObject("sound"));
                    music.Add(new GameObject("music"));

                    sound[i].transform.SetParent(ui.transform);
                    music[i].transform.SetParent(ui.transform);
                    sound[i].AddComponent<AudioSource>();
                    music[i].AddComponent<AudioSource>();
                    sound[i].GetComponent<AudioSource>().playOnAwake = false;
                    music[i].GetComponent<AudioSource>().playOnAwake = false;
                    music[i].GetComponent<AudioSource>().loop = true;
                }
                ui.AddComponent<AudioManager>();
                DontDestroyOnLoad(ui);

            }

            return _Instance;
        }
    }

    void Awake()
    {


        _Instance = this;
        SoundList = new Dictionary<int, AudioClip>();
        SoundList.Add(0, Resources.Load("s_game_start") as AudioClip);
        SoundList.Add(1, Resources.Load("s_button_click") as AudioClip);
        SoundList.Add(2, Resources.Load("s_game_end") as AudioClip);
        SoundList.Add(3, Resources.Load("s_target_success") as AudioClip);
        SoundList.Add(4, Resources.Load("s_target_wrong") as AudioClip);
        SoundList.Add(5, Resources.Load("s_energy_low") as AudioClip);
        SoundList.Add(6, Resources.Load("s_new_score_1") as AudioClip);
        SoundList.Add(7, Resources.Load("s_new_score_2") as AudioClip);
        SoundList.Add(8, Resources.Load("a_day_ambulance") as AudioClip);
        SoundList.Add(9, Resources.Load("a_day_cat") as AudioClip);
        SoundList.Add(10, Resources.Load("a_day_dog") as AudioClip);
        SoundList.Add(11, Resources.Load("a_night_crow") as AudioClip);
        SoundList.Add(12, Resources.Load("a_night_kunti") as AudioClip);
        SoundList.Add(13, Resources.Load("a_night_owl") as AudioClip);
        SoundList.Add(14, Resources.Load("a_night_wolf") as AudioClip);


        MusicList = new Dictionary<int, AudioClip>();
        MusicList.Add(0,Resources.Load("a_walking") as AudioClip);
        MusicList.Add(1, Resources.Load("m_gameplay") as AudioClip);
        MusicList.Add(2, Resources.Load("m_menu") as AudioClip);

        Resources.UnloadUnusedAssets();

}

    public AudioClip GetSound(int Idx)
    {
        return SoundList[Idx];
    }

    public void PlayMusic(int Idx)
    {
        foreach (GameObject g in music)
        {
            if (!g.GetComponent<AudioSource>().isPlaying)
            {
                Reset(g.GetComponent<AudioSource>());
                g.GetComponent<AudioSource>().clip = MusicList[Idx];
                g.GetComponent<AudioSource>().Play();
                break;
            }
        }
    }

    public void ChangeMusicVolume(int Idx, float volume)
    {
        foreach (GameObject g in music)
        {
            if (g.GetComponent<AudioSource>().clip == MusicList[Idx])
            {
                g.GetComponent<AudioSource>().volume = volume;
                break;
            }
        }
    }

    public void ChangeMusicPitch(int Idx, float pitch)
    {
        foreach (GameObject g in music)
        {
            if (g.GetComponent<AudioSource>().clip == MusicList[Idx])
            {
                g.GetComponent<AudioSource>().pitch = pitch;
                break;
            }
        }
    }

    public bool IsMusicPlaying(int idx)
    {
        foreach (GameObject g in music)
        {
            return (g.GetComponent<AudioSource>().clip == MusicList[idx]) && (g.GetComponent<AudioSource>().isPlaying);
        }
        return false;
    }

    //public void PlayMusicWithWait(int Idx)
    //{
    //    StartCoroutine(WaitForMusicThenPlay(Idx));
    //}

    //IEnumerator WaitForMusicThenPlay(int Idx)
    //{
    //    yield return new WaitUntil(() => !sound.GetComponent<AudioSource>().isPlaying);
    //    music.GetComponent<AudioSource>().clip = MusicList[Idx];
    //    music.GetComponent<AudioSource>().Play();
    //}

    public void StopMusic()
    {
        foreach (GameObject g in music)
        {
            if (g.GetComponent<AudioSource>().isPlaying)
            {
                g.GetComponent<AudioSource>().Stop();
            }
        }
    }

    public void PlaySound(int Idx)
    {
        foreach (GameObject g in sound)
        {
            if (!g.GetComponent<AudioSource>().isPlaying)
            {
                Reset(g.GetComponent<AudioSource>());
                g.GetComponent<AudioSource>().clip = SoundList[Idx];
                g.GetComponent<AudioSource>().Play();
                break;
            }
            else
            {
                if (g.GetComponent<AudioSource>().clip.name == SoundList[Idx].name)
                {
                    g.GetComponent<AudioSource>().Stop();
                    g.GetComponent<AudioSource>().Play();
                    break;
                }
            }
        }
    }

    public void ChangeSoundPitch(int Idx, float pitch)
    {
        foreach (GameObject g in sound)
        {
            if (g.GetComponent<AudioSource>().clip == SoundList[Idx])
            {
                g.GetComponent<AudioSource>().pitch = pitch;
                break;
            }
        }
    }

    public void ChangeSoundVolume(int Idx, float volume)
    {
        foreach (GameObject g in sound)
        {
            if (g.GetComponent<AudioSource>().clip == SoundList[Idx])
            {
                g.GetComponent<AudioSource>().volume = volume;
                break;
            }
        }
    }

    public void ChangeSoundPriority(int Idx, int priority)
    {
        foreach (GameObject g in sound)
        {
            if (g.GetComponent<AudioSource>().clip == SoundList[Idx])
            {
                g.GetComponent<AudioSource>().priority = priority;
                break;
            }
        }
    }

    //public void PlaySoundWithWait(int Idx)
    //{
    //    StartCoroutine(WaitForSoundThenPlay(Idx));
    //}

    //IEnumerator WaitForSoundThenPlay(int Idx)
    //{
    //    yield return new WaitWhile(() => sound.GetComponent<AudioSource>().isPlaying);
    //    sound.GetComponent<AudioSource>().clip = SoundList[Idx];
    //    sound.GetComponent<AudioSource>().Play();
    //}

    public void PlaySoundLoop(int Idx)
    {
        foreach (GameObject g in sound)
        {
            if (!g.GetComponent<AudioSource>().isPlaying)
            {
                Reset(g.GetComponent<AudioSource>());
                g.GetComponent<AudioSource>().loop = true;
                g.GetComponent<AudioSource>().clip = SoundList[Idx];
                g.GetComponent<AudioSource>().Play();
                break;
            }
        }
    }

    public void StopSound()
    {
        foreach (GameObject g in sound)
        {
            if (g.GetComponent<AudioSource>().isPlaying)
            {
                Reset(g.GetComponent<AudioSource>());
                g.GetComponent<AudioSource>().loop = false;
                g.GetComponent<AudioSource>().Stop();
            }
        }
    }

    public void Reset(AudioSource audio)
    {
        audio.priority = 128;
        audio.volume = 1f;
        audio.pitch = 1f;
    }

    //IEnumerator PlayingSound(int Idx)
    //{
    //    Debug.Log(sound.GetComponent<AudioSource>().isPlaying);
    //    while (sound.GetComponent<AudioSource>().isPlaying)
    //    {
    //        yield return null;
    //    }
    //    sound.GetComponent<AudioSource>().clip = SoundList[Idx];
    //    sound.GetComponent<AudioSource>().Play();
    //}




}