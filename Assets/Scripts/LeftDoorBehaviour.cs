using UnityEngine;

public class LeftDoorBehaviour : MonoBehaviour {
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue dialogue;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue() {
        dialogueManager.StartDialogue(dialogue);
    }
}
