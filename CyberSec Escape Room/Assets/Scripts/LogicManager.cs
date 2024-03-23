using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;

    private int lives = 5;
    private bool playerImmune = false;
    public GameObject heartPrefab;
    public GameObject greyHeartPrefab;
    public Transform heartsParent;

    //public GameObject redOverlayObject;
    public GameObject redOverlay;
    private Image redOverlayImage;
    public float fadeDuration = 1f;

    private List<GameObject> heartObjects = new List<GameObject>();

    private List<string> openedDoors = new List<string>();
    private List<string> completedChallenges = new List<string>();

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
        InitializeHearts();
        redOverlay.SetActive(false);
        redOverlayImage = redOverlay.GetComponent<Image>();
    }

    public void ToggleImmunity(bool state)
    {
        playerImmune = state;
    }
    public void decrementLife()
    {

        if (!playerImmune)
        {
            lives--;
            UpdateHearts();
            Debug.Log("Life lost");
            if (lives <= 0)
            {
                Debug.Log("Gameover");
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
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color currentColor = Color.Lerp(Color.red, Color.clear, t);
            redOverlayImage.color = currentColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        redOverlayImage.color = Color.clear;
        redOverlay.SetActive(false);

    }

// Updates the canvas objects of the hearts on the UI
    private void UpdateHearts()
    {
        for (int i = 0; i < heartObjects.Count; i++)
        {
            if (i >= lives)
            {
                Vector3 heartPosition = heartObjects[i].transform.position;
                Destroy(heartObjects[i]);
                GameObject greyHeart = Instantiate(greyHeartPrefab, heartPosition, Quaternion.identity, heartsParent);
                heartObjects[i] = greyHeart;
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

    //public void AltarCompletion(string altarID)
    //{
    //    if (altarID.Equals("altar1"))
    //    {
    //        altar1State = true;
    //    }
    //    else if (altarID.Equals("altar2"))
    //    {
    //        altar2State = true;
    //    }
    //}

    //public bool IsAltarsComplete()
    //{
    //    return altar1State && altar2State;
    //}

    //public bool getAltarState(string altarID)
    //{
    //    if (altarID.Equals("altar1"))
    //    {
    //        return altar1State;
    //    }
    //    else if (altarID.Equals("altar2"))
    //    {
    //        return altar2State;
    //    }

    //    return false;

    //}

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
    public void GameComplete()
    {
        StartCoroutine(SwitchSceneWithDelay());
    }

    IEnumerator SwitchSceneWithDelay()
    {
        yield return new WaitForSeconds(2.0f);

        // Load the target scene
        SceneManager.LoadScene("FinalScene");
    }

}
