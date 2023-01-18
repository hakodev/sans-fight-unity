using UnityEngine;

[System.Serializable]
public class Dialogue {
    public Speaker speaker;

    [TextArea(3, 10)]
    public string[] sentences;
}

public enum Speaker {
    Neutral,
    Fight,
    Chara,
    Sans
}
