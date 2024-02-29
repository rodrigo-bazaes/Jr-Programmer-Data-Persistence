using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{

    
    private TextMeshProUGUI bestScoreText;
    private TMP_InputField nameInputField;
    private Button startButton;
    private Button exitButton;
    private TextMeshProUGUI pleaseEnterNameText;

    // Start is called before the first frame update
    void Start()
    {
        SetScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartNew()
    {
        if (nameInputField.text.Trim().Length > 0)
        {
            MenuManager.Instance.currentPlayer = nameInputField.text;

            SceneManager.LoadScene(1);
        }
        else
        {
            pleaseEnterNameText.gameObject.SetActive(true);

        }

    }

    private void SetScene()
    {

        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        bestScoreText = GameObject.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        nameInputField = GameObject.Find("EnterName").GetComponent<TMP_InputField>();
        pleaseEnterNameText = GameObject.Find("PleaseEnterNameText").GetComponent<TextMeshProUGUI>();

        startButton.onClick.AddListener(StartNew);
        exitButton.onClick.AddListener(MenuManager.Instance.Exit);

        pleaseEnterNameText.gameObject.SetActive(false);

        bestScoreText.text = "Best Score: " + MenuManager.Instance.bestPlayer + ", " + MenuManager.Instance.bestScore;
    }
}
