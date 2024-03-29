using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScarewareScript : MonoBehaviour
{
    public LogicManager logic;
    public PlayerMovement player;
    public ScarewareManager scarewareManager;

    public float challengeTime = 10f;
    private float currentTime;
    private bool stopTimer = false;
    public Slider countdownBar;

    

    void Start()
    {
        logic = LogicManager.Instance;
        scarewareManager = GameObject.FindGameObjectWithTag("Scareware").GetComponent<ScarewareManager>();

        StartGame();
    }

    public void StartGame()
    {
        currentTime = challengeTime;
        stopTimer = false;
        countdownBar.maxValue = challengeTime;
        countdownBar.value = challengeTime;
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (!stopTimer)
        {
            currentTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);


            if (currentTime <= 0)
            {
                stopTimer = true;
            }

            if (!stopTimer)
            {
                countdownBar.value = currentTime;
            }

        }

        ChoiceMade(false);

    }

    public void ChoiceMade(bool success)
    {
        stopTimer = false;
        gameObject.SetActive(false);
        scarewareManager.ScarewareFinish(success);
    }

    public void NoEffectChoice()
    {
        stopTimer = false;
        gameObject.SetActive(false);
        scarewareManager.ScarewareFinishNoEffect();
    }

}
