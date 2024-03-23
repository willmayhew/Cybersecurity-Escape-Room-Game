using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected DialogueTrigger trigger;
    protected bool isPlayerInRange = false;
    public GameObject canvas;
    protected LogicManager logic;
    protected InventoryManager inventory;
    protected PlayerMovement player;

    protected virtual void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        inventory = InventoryManager.Instance;
        logic = LogicManager.Instance;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    protected virtual void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && player.CanMove())
        {
            trigger.TriggerDialogue(GetChallengeCompletionStatus(), gameObject);
        }
    }

    public virtual bool GetChallengeCompletionStatus()
    {
        return false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public virtual void EndOfDialogue()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
}
