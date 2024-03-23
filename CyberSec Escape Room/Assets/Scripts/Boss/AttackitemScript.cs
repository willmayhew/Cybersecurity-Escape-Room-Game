using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AttackitemScript : MonoBehaviour
{
    private AttackItemSpawner spawner; // Reference to the AttackItemSpawner
    private LogicManager logic;
    private PlayerMovement player;
    private Boss boss;
    private EnemyAI enemyAI;

    public float amplitude = 2;
    public float speed = 1.5f;

    public float despawnTimer = 5f;
    private float timer;

    private SpriteRenderer spriteRenderer;
    public float fadeOutDuration = 1;

    private bool challengeStarted = false;
    private bool isBeingDestroyed = false;

    private GameObject challengeCanvas;
    private SecurityPolicyScript policyScript;

    void Start()
    {
        logic = LogicManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        enemyAI = GameObject.FindGameObjectWithTag("EnemyAI").GetComponent<EnemyAI>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spawner = FindObjectOfType<AttackItemSpawner>(); // Find the AttackItemSpawner in the scene
    }

    void Update()
    {
        Vector3 p = transform.position;
        float movement = amplitude * Mathf.Cos(Time.time * speed) * Time.deltaTime;
        transform.position = new Vector3(p.x, p.y + movement, p.z);

        if (!challengeStarted)
        {
            timer += Time.deltaTime;

            if (timer >= despawnTimer)
            {
                StartCoroutine(fadeOut());
            }
        }
    }

    IEnumerator fadeOut()
    {
        float counter = 0;
        //Get current color
        Color spriteColor = spriteRenderer.material.color;

        while (counter < fadeOutDuration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / fadeOutDuration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }

        if (!challengeStarted)
        {
            Destroy(gameObject);
        }
        
    }

    public void SetCanvasObject(GameObject obj)
    {
        challengeCanvas = obj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!challengeStarted && collision.CompareTag("Player") && !isBeingDestroyed)
        {
            policyScript = challengeCanvas.GetComponent<SecurityPolicyScript>();
            StartChallenge();
        }
    }

    private void StartChallenge()
    {
        challengeStarted = true;
        spawner.ToggleSpawning(false);
        player.ToggleMovement(false);

        enemyAI.ToggleMovement(false);
        logic.ToggleImmunity(true);

        policyScript.StartGame();
        policyScript.SetAttackItem(gameObject);
    }

    public void ChallengeCompleted(bool success)
    {
        isBeingDestroyed = true;

        logic.ToggleImmunity(false);

        if (success)
        {
            if (!boss.TakeDamage())
            {
                spawner.ToggleSpawning(true);
                enemyAI.ToggleMovement(true);
                player.ToggleMovement(true);
            };
        }
        else
        {
            logic.decrementLife();
            spawner.ToggleSpawning(true);
            enemyAI.ToggleMovement(true);
            player.ToggleMovement(true);
        }

        challengeStarted = false;

        Destroy(gameObject);
    }

}
