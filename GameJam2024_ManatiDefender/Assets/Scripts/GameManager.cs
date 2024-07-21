using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kelp_eater
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject videoPanel;
        public GameObject pausePanel; //Referencia al panel de pausa en el Canvas
        public bool isPaused = false; //Variable para controlar el estado de pausa
        private MonoBehaviour playerMoveScript;

        void Start()
        {
            playerMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }

        public void GameOverAndContinue() //Metodo que pausa el juego para colocar el panel de Game Over
        {
            isPaused = !isPaused; //Invierte el estado de pausa

            if (isPaused)
            {
                Time.timeScale = 0f; //Pausa el tiempo en el juego
                SoundManager.StopMusic();
                playerMoveScript.enabled = false;
                if (pausePanel != null)
                {
                    pausePanel.SetActive(true);
                }
            }
            else
            {
                Time.timeScale = 1f; //Reanuda el tiempo en el juego
                if (pausePanel != null)
                {
                    pausePanel.SetActive(false);
                }
            }
        }

        //METODOS DE BOTONES QUE APARECEN EN EL PANEL DE GAME OVER
        public void VerVideo() //Boton para ver un video para obtener una vida extra.
        {
            videoPanel.SetActive(true);
            pausePanel.SetActive(false);
            ScreenCalculateInstance screenCalculateInstance = Camera.main.GetComponent<ScreenCalculateInstance>();
            screenCalculateInstance.ArrangeObjects();

        }

        public void Reiniciar() //Boton para reiniciar la partida.
        {
            ScoreCounter scoreCounter = FindAnyObjectByType<ScoreCounter>();
            ScreenCalculateInstance screenCalculateInstance = Camera.main.GetComponent<ScreenCalculateInstance>();
            PlayerMove playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
            scoreCounter.newScore = 0;
            scoreCounter.score = 0;
            scoreCounter.elapsedTime = 0;
            scoreCounter.UpdateScoreText();
            playerMove.transform.position = Vector3.up;
            screenCalculateInstance.ArrangeObjects();
            Time.timeScale = 1f;
            SoundManager.PlayMusic(0);
            playerMoveScript.enabled = true;
            pausePanel.SetActive(false);
            
        }

        public void VideoEnd() //Boton que aparece luego de ver el video, para continuar la partida.
        {
            Time.timeScale = 1f;
            SoundManager.PlayMusic(0);
            playerMoveScript.enabled = true;
            isPaused = false;
            videoPanel.SetActive(false);    
        }


    }
}
