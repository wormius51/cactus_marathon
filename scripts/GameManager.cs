using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public TextMeshProUGUI waveTimerText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public int startingMoney = 100;
    private int Money;

    public int money {
        get {
            return Money;
        }
        set {
            Money = value;
            moneyText.text = "Money: " + Money;
        }
    }

    private int maxScore;

    private int Score;

    public int score {
        get {
            return Score;
        }
        set {
            Score = value;
            if (maxScore == 0) {
                if (LevelManager.instance == null)
                    return;
                maxScore = LevelManager.instance.getMaxScore();
            }
            scoreText.text = "Score: " + Score + "/" + maxScore;
            restartText.text = "Restart + " + Score;
            checkFinished();
        }
    }

    private int MissedScore;
    public int missedScore {
        get {
            return MissedScore;
        }
        set {
            MissedScore = value;
            checkFinished();
        }
    }

    public int passiveIncomeSize = 10;
    public float passiveIncomeInterval = 5;
    public GameObject cactusPrefab;
    public GameObject fatusPrefab;
    public int cactusValue = 1;
    public int fatusValue = 10;
    public bool fatusFinished;
    private float timeSincePassiveIncome;

    private GameObject currentInteraction;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        cactusValue = cactusPrefab.GetComponent<Runner>().value;
        fatusValue = fatusPrefab.GetComponent<Runner>().value;
        money = startingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxScore == 0 && LevelManager.instance != null) {
            score = 0;
        }
        if (Input.GetMouseButtonDown(0))
            interact();

        timeSincePassiveIncome += Time.deltaTime;
        if (timeSincePassiveIncome >= passiveIncomeInterval) {
            timeSincePassiveIncome = 0;
            money += passiveIncomeSize;
        }
    }

    public void setValues() {
        missedScore = 0;
        startingMoney += score;
        money = startingMoney;
        score = 0;
        fatusFinished = false;
    }

    public void restart() {
        setValues();
        SceneManager.LoadScene("level 1");
    }

    public void checkFinished() {
        if (missedScore + score == maxScore) {
            string endingName = "";
            if (score >= maxScore) {
                endingName = "Good Ending";
            } else if (score == 0) {
                endingName = "Bad Ending";
            } else {
                endingName = fatusFinished ? "Neutral Plus Ending" : "Neutral Minus Ending";
            }
            waveTimerText.text = endingName;
            SceneManager.LoadScene(endingName);
        }
    }    

    private void interact() {
        RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null) {
                    hit.collider.gameObject.SendMessage("interact", null, SendMessageOptions.DontRequireReceiver);
                    if (currentInteraction != null && currentInteraction != hit.collider.gameObject) {
                        currentInteraction.SendMessage("unInteract", null, SendMessageOptions.DontRequireReceiver);
                    }
                    currentInteraction = hit.collider.gameObject;
                }
            }
    }

    public void updateWaveTimer(float time) {
        if (time < 0) {
            waveTimerText.text = "Last Wave";
            return;
        }
        int seconds = (int)(Mathf.Floor(time));
        int minutes = seconds / 60;
        seconds = seconds % 60;
        waveTimerText.text = "Next Wave In: " + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    }
}
