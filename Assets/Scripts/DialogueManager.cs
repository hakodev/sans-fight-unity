using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_FontAsset dtmMono;
    [SerializeField] TMP_FontAsset comicSans;
    [SerializeField] Color charaTextColor;
    [field: SerializeField] public GameObject DialogueBox { get; set; }
    [field: SerializeField] public TextMeshProUGUI DialogueText { get; set; }
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip textAudioClip;
    [SerializeField] AudioClip fightAudioClip;
    [SerializeField] AudioClip sansAudioClip;
    private Queue<string> dialogueQueue;
    private bool typeFinished = false;
    private Dialogue dialogue;

    private void Start() {
        dialogueQueue = new Queue<string>();
        dialogue = new Dialogue();
    }

    private void Update() {
        if (typeFinished && playerController.InputInteract.IsPressed()) {
            DisplayNextSentence(dialogue.speaker);
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        OpenDialogueBox();
        dialogueQueue.Clear();

        foreach (string sentence in dialogue.sentences) {
            dialogueQueue.Enqueue(sentence);
        }

        DisplayNextSentence(dialogue.speaker);
    }

    private void DisplayNextSentence(Speaker speaker) {
        dialogue.speaker = speaker;
        if (dialogueQueue.Count == 0) {
            CloseDialogueBox();
            return;
        }

        string currentDialogue = dialogueQueue.Dequeue();
        StartCoroutine(TypeDialogue(currentDialogue, speaker));
    }

    private IEnumerator TypeDialogue(string sentence, Speaker speaker) {
        typeFinished = false;

        DialogueText.font = speaker switch {
            Speaker.Sans => comicSans,
            _ => dtmMono
        };

        DialogueText.color = speaker switch {
            Speaker.Chara => charaTextColor,
            _ => Color.white
        };

        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            if (playerController.InputCancel.IsPressed()) {
                DialogueText.text = sentence;
                break;
            }
            else {
                DialogueText.text += letter;

                audioSource.PlayOneShot(
                    speaker switch {
                    Speaker.Fight => fightAudioClip,
                    Speaker.Sans => sansAudioClip,
                    _ => textAudioClip
                });

                yield return new WaitForSeconds(1/45f);
            }
        }

        typeFinished = true;
    }

    public void OpenDialogueBox() {
        DialogueBox.SetActive(true);
        playerController.InputMove.Disable();
    }

    public void CloseDialogueBox() {
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        playerController.InputMove.Enable();
    }
}
