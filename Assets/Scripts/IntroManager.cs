using ProtoBuf;
using System.Collections;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour {
    [field: SerializeField] public SaveData Data { get; private set; }
    private string saveFile;
    private VideoPlayer video;

    private void Awake() {
        saveFile = Path.Combine(Application.persistentDataPath, "GameData.dat");
        video = GetComponent<VideoPlayer>();
        LoadData();
        video.Prepare();
    }

    private void Start() {
        if (Data.IntroWatched) {
            SceneManager.LoadScene(1);
        }

        StartCoroutine(ManageVideoData());
    }

    private IEnumerator ManageVideoData() {
        video.Play();
        yield return new WaitForSeconds(48f);
        Data.IntroWatched = true;
        SaveData();
        SceneManager.LoadScene(1);
    }

    private void LoadData() {
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
