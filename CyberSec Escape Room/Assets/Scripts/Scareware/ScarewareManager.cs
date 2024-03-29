using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarewareManager : MonoBehaviour
{
    public static ScarewareManager Instance;
    public LogicManager logic;
    public PlayerMovement player;
    public Canvas gameCanvas;

    public GameObject[] scarewarePopup;
    private GameObject scarewarePopupInstance;

    public Vector2 minPosition; // Minimum position for the popup
    public Vector2 maxPosition; // Maximum position for the popup

    public bool isScarewareActive { get; private set; }

    //private void Awake()
    //{
    //    if (Instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    Instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    // Start is called before the first frame update
    void Start()
    {
        logic = LogicManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(TriggerScareware());
    }

    IEnumerator TriggerScareware()
    {

        while (true)
        {

            if(logic.IsBossFightStarted())
            {
                yield break;
            }

            // Wait for a random amount of time
            float randomDelay = Random.Range(1f, 2f); // Adjust the range as needed
            yield return new WaitForSeconds(randomDelay);

            while (!player.CanMove())
            {
                yield return new WaitForSeconds(2);
            }

            // Call the scareware function
            StartScareware();

            yield return new WaitUntil(() => !isScarewareActive);

        }
    }

    public void StartScareware()
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        int index = logic.GetScarewareIndex() % scarewarePopup.Length;
        logic.IncrementScarewareIndex();

        player.ToggleMovement(false);
        isScarewareActive = true;
        scarewarePopupInstance = Instantiate(scarewarePopup[index], randomPosition, Quaternion.identity, gameCanvas.transform);
    }

    public void ScarewareFinish(bool success)
    {
        //player.ToggleMovement(true);
        //isScarewareActive = false;
        //Destroy(scarewarePopupInstance);

        if (success)
        {
            logic.AddLife();
        } else
        {
            logic.decrementLife();
        }

        player.ToggleMovement(true);
        isScarewareActive = false;
        Destroy(scarewarePopupInstance);

    }

    public void ScarewareFinishNoEffect()
    {
        player.ToggleMovement(true);
        isScarewareActive = false;
        Destroy(scarewarePopupInstance);
    }
}
