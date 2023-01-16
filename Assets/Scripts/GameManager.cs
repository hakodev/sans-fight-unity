using ProtoBuf;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private readonly bool inGameplay = SceneManager.GetActiveScene().buildIndex == 2 ||
                                       SceneManager.GetActiveScene().buildIndex == 3;

    private string saveFile;
    [field: SerializeField] public SaveData Data { get; private set; }

    private void Awake() {
        saveFile = Path.Combine(Application.persistentDataPath, "GameData.dat");
    }

    private void Start() {
        SetFramerate();
    }

    private void SetFramerate() {
        if (inGameplay) {
            Application.targetFrameRate = 30;
        }
    }

    public void LoadData() {
        if (!File.Exists(saveFile))
            return;

        using var file = File.OpenRead(saveFile);
        using var gzip = new GZipStream(file, CompressionMode.Decompress);
        Data = Serializer.DeserializeWithLengthPrefix<SaveData>(gzip, PrefixStyle.Fixed32BigEndian);
    }

    public void SaveData() {
        using var file = File.Create(saveFile);
        using var gzip = new GZipStream(file, CompressionMode.Compress);
        Serializer.SerializeWithLengthPrefix(gzip, Data, PrefixStyle.Fixed32BigEndian);
    }
}
