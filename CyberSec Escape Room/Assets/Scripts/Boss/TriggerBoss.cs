using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerBoss : MonoBehaviour
{
    public EnemyAI EnemyAI;
    public AttackItemSpawner spawner;

    public Tilemap entraceWallBlocker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !EnemyAI.IsAIStarted)
        {
            Debug.Log("AI Started");
            entraceWallBlocker.gameObject.SetActive(true);
            EnemyAI.StartAI();
            spawner.ToggleSpawning(true);
        }
        
    }

}
