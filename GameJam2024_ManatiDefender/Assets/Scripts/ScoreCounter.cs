using UnityEngine;
using TMPro;  

namespace kelp_eater
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;  

        private float elapsedTime; 
        private int score;        
        private const int pointsPerMinute = 45;

        void Start()
        {
            
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
            int newScore = Mathf.FloorToInt(elapsedTime / 60f * pointsPerMinute);

            if (newScore != score)
            {
                score = newScore;
                UpdateScoreText();
            }
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Puntaje: " + score.ToString();
            }
        }
    }
}

