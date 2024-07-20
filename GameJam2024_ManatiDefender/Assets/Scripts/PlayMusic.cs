using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kelp_eater
{
    public class PlayMusic : MonoBehaviour
    {
        public int MusicTrackNumber; //Indice de la pista de musica a reproducir
        public bool playMusicOnLoop = false; //Bool para reproducir musica en bucle
        public bool playMusicCropped = false; //Bool para reproducir musica desde un segundo en especifico
        public float CroppedMusicTime; //Recorta el tiempo en el que inicia el clip

        void Start()
        {
            if (playMusicOnLoop == false && playMusicCropped == false)
            {
                PlaySceneMainMusic();
                Debug.Log("Reproduce musica con normalidad");
            }

            if (playMusicOnLoop == true && playMusicCropped == true)
            {
                PlaySceneMainMusicOnLoopWithRetardedTime();
                Debug.Log("Reproduce musica con bucle y retardo");
            }

            if (playMusicOnLoop == true && playMusicCropped == false)
            {
                PlaySceneMainMusicOnLoop();
            }

            if (playMusicOnLoop == false && playMusicCropped == true)
            {
                PlaySceneMainMusicWithRetardedTime();
            }

        }

        public void PlaySceneMainMusic()
        {
            SoundManager.PlayMusic(MusicTrackNumber);
        }

        public void PlaySceneMainMusicOnLoopWithRetardedTime()
        {
            SoundManager.PlayMusicOnLoopAndCropped(MusicTrackNumber, CroppedMusicTime);
        }

        public void PlaySceneMainMusicOnLoop()
        {
            SoundManager.PlayMusicOnLoop(MusicTrackNumber);
        }

        public void PlaySceneMainMusicWithRetardedTime()
        {
            SoundManager.PlayMusicCropped(MusicTrackNumber, CroppedMusicTime);
        }

    }
}
