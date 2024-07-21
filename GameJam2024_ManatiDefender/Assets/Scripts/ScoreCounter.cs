using UnityEngine;
using TMPro;  

namespace kelp_eater
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private float moreEatSpeed;
        public float elapsedTime; 
        public int score;
        public int newScore;
        private const int pointsPerMinute = 45;

        KelpsSys kelpSystemScript;
        private bool hasDecreasedTimer;

        void Start()
        {
            kelpSystemScript = GameObject.Find("KelpSystem").GetComponent<kelp_eater.KelpsSys>();

            if (scoreText == null)
            {
                Debug.LogError("El objeto de texto no esta asignado.");
            }

            
            score = 0;
            UpdateScoreText();
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;

            // Calcula el nuevo puntaje basado en el tiempo transcurrido
            // Puntos por segundo = puntos por minuto / 60
            newScore = Mathf.FloorToInt(elapsedTime / 60f * pointsPerMinute);

            if (newScore != score)
            {
                score = newScore;
                UpdateScoreText();
            }

            //Aumenta la velocidad de las algas cuando el puntaje llega a un multiplo de 10.
            if (score > 0 && score % 20 == 0 && !hasDecreasedTimer)
            {
                kelpSystemScript.eatInterval = Mathf.Max(0.1f, kelpSystemScript.eatInterval - moreEatSpeed);
                kelpSystemScript.DecreaseTimerRange();
                hasDecreasedTimer = true;
            }
            else if (score % 20 != 0)
            {
                hasDecreasedTimer = false;
            }
        }

        public void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Puntaje: " + score.ToString();
            }
        }
    }
}

