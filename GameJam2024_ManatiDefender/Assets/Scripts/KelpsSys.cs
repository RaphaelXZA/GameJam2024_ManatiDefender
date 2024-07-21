using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace kelp_eater
{
    public class KelpsSys : MonoBehaviour
    {
        [SerializeField] Button eatButton;
        [SerializeField] private kelp_eater.PlayerMove player;
        [SerializeField] private GameObject[] kelpArray;
        [SerializeField] private float yIncreaseAmount = 1f;
        [SerializeField] private float timer;
        [SerializeField] private float maxTimer = 2f;
        [SerializeField] private float timerMinimunRange = 0.5f;
        [SerializeField] private float timerMaximunRange = 2f;
        [SerializeField] private float timerDecreaseAmount = 0.1f;

        [SerializeField] private GameObject kelpParticle;

        public float downAmount = 1;
        public float eatInterval = 0.6f;

        ScoreCounter scoreScript;

        private bool usePattern1 = true;
        private int patternIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<kelp_eater.PlayerMove>();
            scoreScript = GameObject.Find("ScoreManager").GetComponent<kelp_eater.ScoreCounter>();

            if (player == null)
            {
                Debug.LogError("PlayerManatee not found or PlayerMove component missing.");
                return;
            }

            StartCoroutine(KelpDown());
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= maxTimer)
            {
                UpKelp();
                timer = 0;
            }

            if (timerMaximunRange <= 0.01f)
            {
                timerMaximunRange = 0.01f;
            }

            if (timerMinimunRange <= 0.01f)
            {
                timerMinimunRange = 0.01f;
            }
        }

        void UpKelp()
        {
            maxTimer = Random.Range(timerMinimunRange, timerMaximunRange);

            if (usePattern1)
            {
                int[] pattern1 = { 0, 2, 4, 1, 3 };
                patternIndex = (patternIndex + 1) % pattern1.Length;
                GameObject kelp = kelpArray[pattern1[patternIndex]];
                kelp.transform.position = Vector3.Lerp(kelp.transform.position,
                    new Vector3(kelp.transform.position.x, kelp.transform.position.y + yIncreaseAmount, kelp.transform.position.z), 0.4f);
            }
            else
            {
                int[] pattern2 = { 1, 3, 0, 4, 2 };
                patternIndex = (patternIndex + 1) % pattern2.Length;
                GameObject kelp = kelpArray[pattern2[patternIndex]];
                kelp.transform.position = Vector3.Lerp(kelp.transform.position,
                    new Vector3(kelp.transform.position.x, kelp.transform.position.y + yIncreaseAmount, kelp.transform.position.z), 0.4f);
            }

            usePattern1 = !usePattern1;
        }

        IEnumerator KelpDown()
        {
            PlayerMove playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
            while (true)
            {
                for (int i = 0; i < kelpArray.Length; i++)
                {
                    kelpArray[i].transform.position = new Vector3(kelpArray[i].transform.position.x, Mathf.Clamp(kelpArray[i].transform.position.y, -13, -5), kelpArray[i].transform.position.z);
                }

                switch (player.currentZoneIndex)
                {
                    case 0:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 1, 0.6f);
                        kelpArray[0].transform.position = Vector3.Lerp(kelpArray[0].transform.position,
                            new Vector3(kelpArray[0].transform.position.x, kelpArray[0].transform.position.y - downAmount,
                            kelpArray[0].transform.position.z), 0.4f);
                        break;

                    case 1:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 1, 0.6f);
                        kelpArray[1].transform.position = Vector3.Lerp(kelpArray[1].transform.position,
                            new Vector3(kelpArray[1].transform.position.x, kelpArray[1].transform.position.y - downAmount,
                            kelpArray[1].transform.position.z), 0.4f);
                        break;

                    case 2:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 1, 0.6f);
                        kelpArray[2].transform.position = Vector3.Lerp(kelpArray[2].transform.position,
                            new Vector3(kelpArray[2].transform.position.x, kelpArray[2].transform.position.y - downAmount,
                            kelpArray[2].transform.position.z), 0.4f);
                        break;

                    case 3:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 1, 0.6f);
                        kelpArray[3].transform.position = Vector3.Lerp(kelpArray[3].transform.position,
                            new Vector3(kelpArray[3].transform.position.x, kelpArray[3].transform.position.y - downAmount,
                            kelpArray[3].transform.position.z), 0.4f);
                        break;

                    case 4:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 1, 0.6f);
                        kelpArray[4].transform.position = Vector3.Lerp(kelpArray[4].transform.position,
                            new Vector3(kelpArray[4].transform.position.x, kelpArray[4].transform.position.y - downAmount,
                            kelpArray[4].transform.position.z), 0.4f);
                        break;

                    default:
                        break;
                }

                yield return new WaitForSeconds(eatInterval);
            }
        }

        public void DecreaseTimerRange()
        {
            timerMinimunRange = Mathf.Max(0.01f, timerMinimunRange - timerDecreaseAmount * 0.5f);
            timerMaximunRange = Mathf.Max(0.01f, timerMaximunRange - timerDecreaseAmount * 0.5f);
            downAmount += 0.1f;
        }

    }
}
