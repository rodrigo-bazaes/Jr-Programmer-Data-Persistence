using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    private int maxLines = 11;
    

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    [SerializeField] private TextMeshProUGUI bestScoreText;


   
    private List<int> pointCountList;



    private float xAxisTopLeft = -1.5f;
    private float brickJumpBetweenLines = 0.3f;
    private float yAxisTopUp = 4f;

    private int totalPoints;

    private Vector3 paddleInitialPos = new Vector3(0f, 0.7f, 0f);
    private Vector3 ballRelativeInitialPos = new Vector3(0f, 1.5f, 0f);

    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject ball;

    private Rigidbody ballRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
        bestScoreText.text = "Best Score: " + MenuManager.Instance.bestPlayer + ", " + MenuManager.Instance.bestScore;
        SetLevel();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.transform.SetParent(null);
                ballRigidbody.isKinematic = false; // enables physics interactions
                ballRigidbody.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    // Set bricks when a level is finished or when started a new game
    private void SetLevel()
    {
        
        pointCountList = Enumerable.Range(1, LineCount).ToList();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        
        // Bricks are created from the top instead of from the bottom
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(xAxisTopLeft + step * x, yAxisTopUp - i * brickJumpBetweenLines, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);

                // bricks at the top have more points 
                brick.PointValue = pointCountList[LineCount-1-i];
                totalPoints += brick.PointValue;
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        LineCount++;
        
    }


    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        // if we win the level, start a new level (as soon as you can continue playing)
        if(m_Points == totalPoints)
        {

            // reset paddle and ball positions and set ball velocity to 0;
            paddle.transform.position = paddleInitialPos;
            ball.transform.SetParent(paddle.transform);
            ball.transform.localPosition = ballRelativeInitialPos;

            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.isKinematic = true; // Disables physics interactions
            m_Started = false;

            if(LineCount < maxLines)
            {
                SetLevel();
            }
            else
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        UpdateBestScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
        
    }


    private void UpdateBestScore()
    {
        
        if (m_Points > MenuManager.Instance.bestScore)
        {
            MenuManager.Instance.bestPlayer = MenuManager.Instance.currentPlayer;
            MenuManager.Instance.bestScore = m_Points;
            
            
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
