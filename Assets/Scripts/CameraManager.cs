using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] Transform player;

    private void Update() {
        ProcessOverworldCamera();
    }

    private void ProcessOverworldCamera() {
        const float playerStart = -2f;
        const float playerEnd = 52f;

        if (player.position.x >= playerStart && player.position.x <= playerEnd) {
            Camera.main.transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
}
