using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;
    public PlayerMovement player;

    public GameObject playerUI;

    public int maxLives = 10;
    public int maxNormalLives = 5;
    public int lives;
    private bool playerImmune = false;
    public GameObject heartPrefab;
    public GameObject greyHeartPrefab;
    public GameObject additionalHeartPrefab;
    public Transform heartsParent;

    public GameObject deathObject;

    //public GameObject redOverlayObject;
    public GameObject redOverlay;
    private Image redOverlayImage;
    public float redFadeDuration = 1f;

    private List<GameObject> heartObjects = new List<GameObject>();

    private List<string> openedDoors = new List<string>();
    private List<string> completedChallenges = new List<string>();
    private Dictionary<string,ChallengeStats> challengeStats = new Dictionary<string, ChallengeStats>();

    private bool isBossFightStarted;

    private int scarewareIndex = 0;

    private int doorPrevious = 0; //The index of the door the player came from in the previous scene
    private int currentDoor = 0; //The index of the door the player just walked through in the current scene

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        lives = maxNormalLives;
        InitializeHearts();

        deathObject.SetActive(false);

        redOverlay.SetActive(false);
        redOverlayImage = redOverlay.GetComponent<Image>();
    }

    public int GetLives()
    {
        return lives;
    }

    public void ToggleImmunity(bool state)
    {
        playerImmune = state;
    }
    public virtual void decrementLife()
    {

        if (!playerImmune)
        {
            lives--;
            UpdateHearts();
            Debug.Log("Life lost");
            if (lives <= 0)
            {
                Debug.Log("Gameover");
                deathObject.SetActive(true);
                redOverlay.SetActive(false);
                Time.timeScale = 0;
            } else
            {
                redOverlay.SetActive(true);
                StartCoroutine(DamageTaken());
            }
        }
    }

    private IEnumerator DamageTaken()
    {
        float elapsedTime = 0f;
        while (elapsedTime < redFadeDuration)
        {
            float t = elapsedTime / redFadeDuration;
            Color currentColor = Color.Lerp(Color.red, Color.clear, t);
            redOverlayImage.color = currentColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        redOverlayImage.color = Color.clear;
        redOverlay.SetActive(false);

    }

    private void UpdateHearts()
    {

        Debug.Log("Current lives: " + lives);

        // Clear all existing hearts
        foreach (var heartObject in heartObjects)
        {
            Destroy(heartObject);
        }
        heartObjects.Clear();

        // Determine the number of red hearts to display
        int redHeartsCount = Mathf.Min(lives, maxNormalLives);

        // Add red hearts
        for (int i = 0; i < redHeartsCount; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            RectTransform rt = heart.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(-180 + i * rt.rect.width, 0, 0f);
            heartObjects.Add(heart);
        }

        if(lives > maxNormalLives)
        {
            int additionalHeartsCount = lives - maxNormalLives;
            for (int i = 0; i < additionalHeartsCount; i++)
            {
                GameObject additionalHeart = Instantiate(additionalHeartPrefab, heartsParent);
                RectTransform rt = additionalHeart.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(-180 + (i) * rt.rect.width, 0, 0f);
                heartObjects.Add(additionalHeart);
            }
        }

        if(lives < maxNormalLives)
        {
            int greyHeartsCount = maxNormalLives - lives;
            for (int i = 0; i < greyHeartsCount; i++)
            {
                Debug.Log(greyHeartPrefab);
                Debug.Log(heartsParent);
                GameObject greyHeart = Instantiate(greyHeartPrefab, heartsParent);
                RectTransform rt = greyHeart.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(-180 + (redHeartsCount + i) * rt.rect.width, 0, 0f);
                heartObjects.Add(greyHeart);
            }
        }
    }


    public void InitializeHearts()
    {

        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            RectTransform rt = heart.GetComponent<RectTransform>();

            rt.localPosition = new Vector3(-180 + i * rt.rect.width, 0, 0f);

            heartObjects.Add(heart);
        }
    }

    public virtual void AddLife()
    {
        if (lives < maxLives)
        {
            lives++;
            UpdateHearts();
            Debug.Log("Life added");
        }

    }

    public void AddOpenDoor(string doorName)
    {
        if (!openedDoors.Contains(doorName))
        {
            openedDoors.Add(doorName);
        }
    }

    public bool isDoorOpen(string doorName)
    {
        return openedDoors.Contains(doorName);
    }

    public void doorEntered(int doorIndex)
    {
        doorPrevious = currentDoor;
        currentDoor = doorIndex;
    }

    public int getDoorIndex()
    {
        return doorPrevious;
    }

    public void AddCompletedChallenge(string challengeName)
    {
        if (!completedChallenges.Contains(challengeName))
        {
            completedChallenges.Add(challengeName);
        }
    }

    public bool isChallengeComplete(string challengeName)
    {
        return completedChallenges.Contains(challengeName);
    }

    public void StoreChallengeStatistics(string challengeName, bool correct, int index)
    {
        if (!challengeStats.TryGetValue(challengeName, out ChallengeStats stats))
        {
            // If the challenge name doesn't exist in the dictionary, create a new entry
            stats = new ChallengeStats();
            challengeStats.Add(challengeName, stats);
        }

        // Add the index to the appropriate list based on whether the answer was correct
        if (correct)
        {
            stats.correctAnswerIndices.Add(index);
        }
        else
        {
            stats.incorrectAnswerIndices.Add(index);
        }
    }

    public void GameComplete()
    {
        //foreach(string challenge in completedChallenges)
        //{
        //    Debug.Log(challenge);
        //}
        //Debug.Log("-----------------");
        //foreach (string challenge in challengeStats.Keys)
        //{
        //    Debug.Log("-----------------");
        //    Debug.Log(challenge);
        //    foreach (int index in challengeStats[challenge].correctAnswerIndices)
        //    {
        //        Debug.Log("Correct " + index);
        //    }
        //    foreach (int index in challengeStats[challenge].incorrectAnswerIndices)
        //    {
        //        Debug.Log("Incorrect " + index);
        //    }
        //}
        //Debug.Log("-----------------");


        //playerUI.SetActive(false);
        StartCoroutine(FinalSceneWithDelay());
    }

    IEnumerator FinalSceneWithDelay()
    {
        // Wait for 2 seconds before starting the fade
        yield return new WaitForSeconds(2.0f);

        foreach (Transform child in playerUI.transform)
        {
            child.gameObject.SetActive(false);
            Debug.Log(child);
        }

        // Utilise redOverlay to show death fade (Set to black)
        redOverlay.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < 2.0f)
        {
            float t = elapsedTime / redFadeDuration;
            Color currentColor = Color.Lerp(Color.clear, Color.black, t);
            redOverlayImage.color = currentColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        redOverlayImage.color = Color.black;

        redOverlay.SetActive(false);

        // Load the target scene
        SceneManager.LoadScene("FinalScene");
    }


    public void BossFightStarted(bool started)
    {
        isBossFightStarted = started;
    }

    public bool IsBossFightStarted()
    {
        return isBossFightStarted;
    }

    public int GetScarewareIndex()
    {
        return scarewareIndex;
    }

    public void IncrementScarewareIndex()
    {
        scarewareIndex++;
    }

    public Dictionary<string, ChallengeStats> GetChallengeStats()
    {
        return challengeStats;
    }

}