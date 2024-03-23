using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecurityPolicyScript : MonoBehaviour
{
    public Policy[] policies;

    public float challengeTime = 10f;
    private float currentTime;
    private bool stopTimer = false;
    public Slider countdownBar;

    private int currentIndex = 0;

    public Button policy1Button;
    public Button policy2Button;
    public Button policy3Button;

    private TextMeshProUGUI policy1;
    private TextMeshProUGUI policy2;
    private TextMeshProUGUI policy3;

    private int selectedButton = -1;

    private LogicManager logic;

    private GameObject attackItem;
    private AttackitemScript attackitemScript;

    void Start()
    {
        logic = LogicManager.Instance;

        //policy1 = policy1Button.GetComponentInChildren<TextMeshProUGUI>();
        //policy2 = policy2Button.GetComponentInChildren<TextMeshProUGUI>();
        //policy3 = policy3Button.GetComponentInChildren<TextMeshProUGUI>();

        policy1Button.onClick.AddListener(() => selectedButton = 0);
        policy2Button.onClick.AddListener(() => selectedButton = 1);
        policy3Button.onClick.AddListener(() => selectedButton = 2);
    }

    public void SetAttackItem(GameObject obj)
    {
        attackItem = obj;
        attackitemScript = obj.GetComponent<AttackitemScript>();
    }

    public void StartGame()
    {

        policy1 = policy1Button.GetComponentInChildren<TextMeshProUGUI>();
        policy2 = policy2Button.GetComponentInChildren<TextMeshProUGUI>();
        policy3 = policy3Button.GetComponentInChildren<TextMeshProUGUI>();

        DisplayPolicies();

        gameObject.SetActive(true);

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
            

            if(currentTime <= 0)
            {
                stopTimer = true;
            }

            if (!stopTimer)
            {
                countdownBar.value = currentTime;
            }

        }

        CorrectChoice(false);
    }

    public void CheckAnswer()
    {
        if (policies[currentIndex].badPolicy == selectedButton)
        {
            currentIndex++;
            CorrectChoice(true);
        }
        else if (selectedButton != -1)
        {
            CorrectChoice(false);
        }

        selectedButton = -1;
    }

    private void DisplayPolicies()
    {
        if (policies.Length == 0)
        {
            Debug.LogError("Policies have not been set");
            return;
        }

        if(currentIndex >= policies.Length)
        {
            currentIndex = 0;
        }

        policy1.text = policies[currentIndex].policy[0];
        policy2.text = policies[currentIndex].policy[1];
        policy3.text = policies[currentIndex].policy[2];
    }

    private void CorrectChoice(bool correct)
    {

        stopTimer = false;

        gameObject.SetActive(false);

        if (correct)
        {
            attackitemScript.ChallengeCompleted(true);
        } else
        {
            attackitemScript.ChallengeCompleted(false);
        }

    }

}
