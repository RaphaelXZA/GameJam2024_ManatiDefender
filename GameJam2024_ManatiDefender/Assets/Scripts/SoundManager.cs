using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kelp_eater
{
    public enum SoundType //CATEGORIAS de los efectos de sonido, se puede ampliar a traves del script solo si es necesario.
    {
        PLAYER_MOVE,
        AMBIENT_SOUNDS
    }

    public class SoundManager : MonoBehaviour
    {
        [Header("----------------------Music Settings----------------------")]
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private TrackClass[] musicList;

        [Header("----------------------Sound Settings----------------------")]
        [SerializeField] private AudioSource soundAudioSource;
        [SerializeField] private List<SoundList> soundList;

        private static SoundManager instance;

        private void Awake()
        {
            instance = this;
        }

        //SONIDOS
        public static void PlaySound(SoundType sound, float volume = 1) //Reproduce un sonido al azar dentro de una categoria, sirve para cuando hay mas de un sonido para la misma accion.
        {
            SoundList soundList = instance.soundList.Find(s => s.soundType == sound);
            if (soundList != null)
            {
                AudioClip[] clips = soundList.Sounds;
                AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
                instance.soundAudioSource.PlayOneShot(randomClip, volume);
            }
        }

        public static void PlaySpecificSound(SoundType sound, int clipIndex, float volume = 1) //Para reproducir un sonido especifico dentro de una categoria.
        {
            SoundList soundList = instance.soundList.Find(s => s.soundType == sound);
            if (soundList != null)
            {
                if (clipIndex >= 0 && clipIndex < soundList.Sounds.Length)
                {
                    AudioClip clip = soundList.Sounds[clipIndex];
                    instance.soundAudioSource.PlayOneShot(clip, volume);
                }
                else
                {
                    Debug.LogWarning($"Indice del clip {clipIndex} esta fuera del rango del SoundType {sound}");
                }
            }
            else
            {
                Debug.LogWarning($"No se encontro el SoundType {sound}.");
            }
        }

        //MUSICA
        public static void PlayMusic(int trackNum) //Reproduce musica. 
        {
            Debug.Log("Musica nueva reproducida");
            instance.musicAudioSource.clip = instance.musicList[trackNum].clip;
            instance.musicAudioSource.Play();
        }

        public static void PlayMusicOnLoop(int trackNum) //Reproduce musica en bucle.
        {
            Debug.Log("Musica nueva reproducida");
            instance.musicAudioSource.clip = instance.musicList[trackNum].clip;
            instance.musicAudioSource.loop = true;
            instance.musicAudioSource.Play();
        }

        public static void PlayMusicCropped(int trackNum, float startAudioTime) //Reproduce musica con cierto retraso de segundos.
        {
            Debug.Log("Musica nueva reproducida");
            instance.musicAudioSource.clip = instance.musicList[trackNum].clip;
            instance.musicAudioSource.time = startAudioTime;
            instance.musicAudioSource.Play();
        }

        public static void PlayMusicOnLoopAndCropped(int trackNum, float startAudioTime) //Reproduce musica en bucle Y con cierto retraso de segundos.
        {
            Debug.Log("Musica nueva reproducida");
            instance.musicAudioSource.clip = instance.musicList[trackNum].clip;
            instance.musicAudioSource.loop = true;
            instance.musicAudioSource.time = startAudioTime;
            instance.musicAudioSource.Play();
        }


        public static void StopMusic()
        {
            instance.musicAudioSource.Stop();
        }

        public static void StopSound()
        {
            instance.soundAudioSource.Stop();
        }

        public static void PauseMusic()
        {
            instance.musicAudioSource.Pause();
        }

        public static void UnpauseMusic()
        {
            instance.musicAudioSource.UnPause();
        }



#if UNITY_EDITOR
        private void OnValidate()
        {
            Array enumValues = Enum.GetValues(typeof(SoundType));
            for (int i = 0; i < enumValues.Length; i++)
            {
                SoundType soundType = (SoundType)enumValues.GetValue(i);
                if (!soundList.Exists(s => s.soundType == soundType))
                {
                    soundList.Add(new SoundList { soundType = soundType });
                }
            }

            // Remove any soundList elements that are no longer in the enum
            soundList.RemoveAll(s => !Enum.IsDefined(typeof(SoundType), s.soundType));
        }
#endif
    }

    [Serializable]
    public class SoundList
    {
        public SoundType soundType;
        public AudioClip[] Sounds { get => sounds; }
        [SerializeField] public AudioClip[] sounds;
    }

}
