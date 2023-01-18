using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftDoorBehaviour : MonoBehaviour
{
    //unfinished

    [SerializeField] TextMeshProUGUI textComponent;
    [TextArea(3, 10)]
    [SerializeField] string[] sentences;
    [SerializeField] int textSpeed;
    [SerializeField] AudioClip textSound;
    [SerializeField] PlayerController playerController;

    private int index;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        textComponent.text = string.Empty;
    }

    private void StartDialogue() {
        index = 0;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue() {
        foreach (char letter in sentences[index].ToCharArray()) {
            textComponent.text += letter;
            audioSource.PlayOneShot(textSound);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            StartDialogue();
            playerController.InputMove.Disable();
        }
    }
}
