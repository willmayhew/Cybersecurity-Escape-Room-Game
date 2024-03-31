using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScreenResults : MonoBehaviour
{

    private LogicManager logic;
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        logic = LogicManager.Instance;
        ShowResults();

    }

    private void ShowResults()
    {
        Dictionary<string, ChallengeStats> challengeStats = logic.GetChallengeStats();

        // Custom names for challenges
        Dictionary<string, string> challengeNames = new Dictionary<string, string>()
    {
        { "Phishing", "Phishing Letters" },
        { "NPC1", "Romance and Friendship Scam Dialogue" },
        { "NPC2", "Romance and Friendship Scam Dialogue2" },
        { "PasswordIdentifier", "Strong Password Identifier" },
        { "PasswordWorkshop", "Password Workshop" },
        { "Policy", "Cyber Security Policies" },
        { "Scareware", "Scareware" }
    };

        float totalChallengeScore = 0f;
        //int totalQuestions = 0;

        // Calculate total challenge score
        foreach (string challengeName in challengeStats.Keys)
        {
            ChallengeStats stats = challengeStats[challengeName];
            int questions = stats.correctAnswerIndices.Count + stats.incorrectAnswerIndices.Count;
            float score = questions > 0 ? (float)stats.correctAnswerIndices.Count / questions * 100f : 0f;

            totalChallengeScore += score;
            //totalQuestions += questions;
        }

        float averageChallengeScore = challengeStats.Count > 0 ? totalChallengeScore / challengeStats.Count : 0f;

        float lifeScore = (float)logic.GetLives() / logic.maxLives * 100f;
        float challengeWeight = 0.8f; // 80% weight for challenge score
        float lifeWeight = 0.2f; // 20% weight for life score

        float overallScore = (averageChallengeScore * challengeWeight) + (lifeScore * lifeWeight);
        string formattedOverallScore = overallScore.ToString("F1");

        string resultsString = "Total Lives Left: " + logic.GetLives() + "\n";
        resultsString += "Average Challenge Score: " + averageChallengeScore.ToString("F1") + "/100\n";

        // Breakdown of challenge scores
        resultsString += "\n\nBreakdown by Challenge:\n";

        // Track scores for combined NPC challenges
        float totalNPCScore = 0f;
        int npcCount = 0;

        foreach (string challengeName in challengeStats.Keys)
        {
            ChallengeStats stats = challengeStats[challengeName];
            int questions = stats.correctAnswerIndices.Count + stats.incorrectAnswerIndices.Count;
            float score = questions > 0 ? (float)stats.correctAnswerIndices.Count / questions * 100f : 0f;
            string customName = challengeNames.ContainsKey(challengeName) ? challengeNames[challengeName] : challengeName;

            if (challengeName == "NPC1" || challengeName == "NPC2")
            {
                totalNPCScore += score;
                npcCount++;
            }
            else
            {
                resultsString += customName + ": " + score.ToString("F1") + "/100\n";
            }
        }

        // Calculate average score for NPC challenges
        if (npcCount > 0)
        {
            float averageNPCScore = totalNPCScore / npcCount;
            resultsString += "Romance and Friendship Scam Dialogue: " + averageNPCScore.ToString("F1") + "/100\n";
        }

        //resultsString += "\nTotal Lives Left: " + logic.GetLives() + "\n";

        finalScoreText.text = "Overall Score:\n" + formattedOverallScore;

        resultsText.text = resultsString;
    }

}
