using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] Transform player;
    [SerializeField] GameObject fightTrigger;
    [SerializeField] Vector3 cameraEndPosition;
    //[SerializeField] SansBehaviour sansBehaviour;

    private void Update() {
        if (fightTrigger.activeSelf) {
            ProcessOverworldCamera();
        }
        else {
            // Stop Frisk and move camera toward Sans
            //sansBehaviour.gameObject.SetActive(true);
            StartCoroutine(MoveCamera());
        }
    }

    private void ProcessOverworldCamera() {
        const float playerStart = -2f;
        const float playerEnd = 52f;

        if (player.position.x > playerStart && player.position.x < playerEnd) {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    private IEnumerator MoveCamera() {
        Application.targetFrameRate = 60;
        yield return new WaitForSeconds(1.6f);
        transform.position = Vector3.MoveTowards(transform.position, cameraEndPosition, Time.deltaTime * 1.6f);
        yield return new WaitForSeconds(4.6f);
        //sansBehaviour.GenerateSansDialogue();
    }
}
