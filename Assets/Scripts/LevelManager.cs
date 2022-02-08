using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public bool started;
    public int currentCoinCount;
    public int coinCountInLevel;
    public bool pickedUpIceCream;
    public float timeAttackTime;
    public CinemachineVirtualCamera cineMachineCamera;
    public GameObject playerPrefab;
    public GameObject coinsPrefab;
    public GameObject currentCoinGameObject;
    public GameObject iceCreamPrefab;
    public GameObject currIceCream;

    [Header("Camera Animation")]
    public float cameraWaitTime;
    public float cameraMoveTime;

    Vector3 playerStartPos;
    Quaternion playerStartRotation;
    LevelTimer levelTimer;
    Vector3 cameraStartPos;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStartPos = player.transform.position;
        playerStartRotation = player.transform.rotation;
        levelTimer = player.GetComponent<LevelTimer>();
        cameraStartPos = cineMachineCamera.transform.position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.PlayMusic();
    }

    public void RespawnNewPlayer(GameObject cameraFollowPoint)
    {
        started = false;
        GameObject player = Instantiate(playerPrefab, playerStartPos, playerStartRotation);
        cameraFollowPoint.transform.parent = player.transform;
        cameraFollowPoint.transform.rotation = Quaternion.identity;
        player.GetComponent<PlayerState>().cameraFollowPoint = cameraFollowPoint;

        SetCameraDamping(0);
        currentCoinCount = 0;
        Destroy(currentCoinGameObject);
        currentCoinGameObject = Instantiate(coinsPrefab);
        

        pickedUpIceCream = false;
        Destroy(currIceCream);
        currIceCream = Instantiate(iceCreamPrefab);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(cameraFollowPoint.transform.DOLocalMove(cameraFollowPoint.transform.localPosition, cameraWaitTime));
        sequence.Append(cameraFollowPoint.transform.DOLocalMove(Vector3.zero, cameraMoveTime).SetEase(Ease.InOutQuart));
        sequence.AppendCallback(() =>
        {
            started = true;
            levelTimer.startTime = Time.time;
            SetCameraDamping(0.3f);
        });
    }

    void SetCameraDamping(float value)
    {
        cineMachineCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = value;
        cineMachineCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = value;
        cineMachineCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = value;
    }
}
