using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Referance Player")]
    [SerializeField] GameObject playerPrefab;
    PlayerMovement player;
    EnemyController enemyController;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Elements")]
    public Slider healthBar;

    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 120f; // 2 minutes
    private float timeRemaining;
    private bool isGameOver = false;
    [SerializeField] TMP_Text timerText; 

    [Header("UI Referance")]
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TMP_Text EndScoor;
    [SerializeField] GameObject gameWinUI;
    [SerializeField] TMP_Text scoreText;
  
    [SerializeField] GameObject panelEnd;
    
    private int score = 0;
    private Vector3 orgPosition;
    private Color originalScoreColor;
    private Color originalLifeColor;

    AudioManager audioManager;

    void Start()
    {
        // Initialize UI
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        EndScoor.gameObject.SetActive(false);
        panelEnd.SetActive(false);

        originalScoreColor = scoreText.color;
        
        currentHealth = maxHealth;

        UpdateHealthUI();
        //UpdateTimerUI();

        player = FindObjectOfType<PlayerMovement>();
        enemyController = FindObjectOfType<EnemyController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        orgPosition = scoreText.transform.localPosition;
        timeRemaining = timeLimit;
    }

    private void Update() 
    {
        if (!isGameOver) 
        {
            if (timeRemaining > 0)
            {
                //timeRemaining -= Time.deltaTime;
                //UpdateTimerUI();
            }
            else if (timeRemaining <= 0 && !isGameOver)
            {
                //UpdateTimerUI();
                //timerText.text = ("00:00");
                //isGameOver = true;
                //player.Die();
                //Win();
            }
        }
    }

    // void UpdateTimerUI()
    // {
    //     int minutes = Mathf.FloorToInt(timeRemaining / 60);
    //     int seconds = Mathf.FloorToInt(timeRemaining % 60);
    //     //timerText.text = "Time: "+string.Format("{0:00}:{1:00}", minutes, seconds);
    // }

    // --------------------Player Health System -----------------------
    public void PlayerTakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            isGameOver = true;
            player.Die();
            //enemyController.EnemyDie();
            GameOver();
        }
    }

    public void PlayerHeal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }



    public void IncreaseScore()
    {
        score++;
        scoreText.text = "SCORE: "+score.ToString();
        scoreText.color = Color.green;
        EndScoor.text = "Final Score: " + score.ToString();
        ShakeAndFlashScore();
        StartCoroutine(ResetColor());
    }

    private void GameOver()
    {
        Debug.Log("Game Over triggered!");
        panelEnd.SetActive(true);
        EndScoor.text = "Final Score: " + score.ToString();
        EndScoor.gameObject.SetActive(true);
        gameOverUI.SetActive(true);

        audioManager.StopBk();
        audioManager.PlayOnce2(audioManager.DieClip);
        
        Invoke("LoadEndScene", 10);
    }

    public void Win()
    {
        Debug.Log("Win triggered!");
        panelEnd.SetActive(true);
        EndScoor.text = "Final Score: " + score.ToString();
        EndScoor.gameObject.SetActive(true);
        gameWinUI.SetActive(true);

        Invoke("LoadEndScene", 10);
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector3(-1, -2, 0), Quaternion.identity);
    }

    // ----------- Color Effects -------------
    public void ShakeAndFlashScore()
    {
        StartCoroutine(ShakeTextCoroutine());
        StartCoroutine(FlashTextColor());
    }

    private IEnumerator FlashTextColor()
    {
        Color orginalColor = scoreText.color;
        scoreText.color = Color.green;

        yield return new WaitForSeconds(0.3f);
        scoreText.color = orginalColor;
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.5f);
        scoreText.color = originalScoreColor;
    }

    private IEnumerator ShakeTextCoroutine()
    {
        float elapsedTime = 0f;
        float duration = 0.3f;
        float magnitude = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float offsetY = Mathf.Sin(elapsedTime * Mathf.PI * 3) * magnitude;
            scoreText.transform.localPosition = orgPosition + new Vector3(0f, offsetY, 0f);
            yield return null;
        }
        scoreText.transform.localPosition = orgPosition;
    }

    public void UpdateWave(int wave)
    {
        timerText.text = $"WAVE: {wave}";
    }

    // CircleHealth to increase player health

}
