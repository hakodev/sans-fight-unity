using UnityEngine;
using TMPro;

public class PlayTime : MonoBehaviour {
    [field: SerializeField] public SaveData Data { get; private set; }

    private float playMinutes;
    private float playSeconds;
    private TextMeshProUGUI playTimeText;

    private void Awake() {
        playTimeText = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        playMinutes = Data.PlayMinutes;
        playSeconds = Data.PlaySeconds;

        playTimeText.text = playMinutes + ":" + playSeconds;
    }
}
