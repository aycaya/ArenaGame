using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    Camera sceneCamera;
    //Transform criminalList;
    //[SerializeField] Vector3 upgradeCameraPos;
    //[SerializeField] Vector3 upgradeCameraAngles;
    Vector3 desiredPosition;
    Vector3 desiredAngles;
    Vector3 initialCameraPos;
    [SerializeField] float initialFOV = 60f;
    [SerializeField] float FOVIncrement = 5f;
    [SerializeField] float FOVLimit = 100f;
    [SerializeField] Vector3 defaultEulerAngles = new Vector3(60f, 0f, 0f);
    [SerializeField] Vector3 zoomedEulerAngles = new Vector3(60f, 0f, 0f);
    [SerializeField] Vector3 zoomedPosOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] float zoomedFov = 60f;
    [SerializeField] InputAction zoomButton;
    public bool isZoomOn = false;
    bool isZoomingToSpecificPoint = false;
    [SerializeField] Vector3 zoomOffsetForABTest;
    [SerializeField] float[] timers;

    private void Awake()
    {
        sceneCamera = Camera.main;
        // criminalList = GameObject.FindGameObjectWithTag("Criminals").transform;
        initialCameraPos = transform.position;
    }

    private void OnEnable()
    {
        zoomButton.Enable();
    }

    private void OnDisable()
    {
        zoomButton.Disable();
    }

    private void Start()
    {
        zoomButton.performed += (ctx) =>
        {
            isZoomOn = !isZoomOn;
        };
    }

    void LateUpdate()
    {
        float desiredFOV;
        if (isZoomingToSpecificPoint)
        {
            return;
        }
        //if (isZoomOn)
        //{
        //    desiredFOV = zoomedFov;
        //    desiredPosition = target.position + zoomedPosOffset;
        //    desiredAngles = zoomedEulerAngles;
        //}
        //else
        //{
        //    desiredFOV = Mathf.Clamp(initialFOV + criminalList.childCount * FOVIncrement, 0f, FOVLimit);
        //    desiredPosition = target.position + offset;
        //    desiredAngles = defaultEulerAngles;
        //}
        desiredPosition = target.position + offset;
        desiredAngles = defaultEulerAngles;
        // sceneCamera.fieldOfView = Mathf.Lerp(sceneCamera.fieldOfView, desiredFOV, smoothSpeed * Time.deltaTime);
        //Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        Vector3 smoothedAngle = Vector3.Lerp(transform.eulerAngles, desiredAngles, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.eulerAngles = smoothedAngle;
    }

    //public IEnumerator ZoomToSpecificPoint(Transform location, float timer1,float timer2,float timer3, GameObject areaToUnlock)
    //{
    //    PlayerController playerController = FindObjectOfType<PlayerController>();
    //    playerController.disablePlayerControl = true;
    //    isZoomingToSpecificPoint = true;
    //    float timer = 0f;
    //    float normalizedTimer;
    //    Vector3 currentPos = transform.position;
    //    while (timer < timer1)
    //    {
    //        timer += Time.deltaTime;
    //        normalizedTimer = timer / timer1;
    //        transform.position = Vector3.Lerp(currentPos, location.transform.position, normalizedTimer);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    areaToUnlock.SetActive(true);
    //    timer = 0f;
    //    while (timer < timer2)
    //    {
    //        timer += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    timer = 0f;
    //    while (timer < timer3)
    //    {
    //        timer += Time.deltaTime;
    //        normalizedTimer = timer / timer3;
    //        transform.position = Vector3.Lerp(location.transform.position, currentPos, normalizedTimer);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    isZoomingToSpecificPoint = false;
    //    playerController.disablePlayerControl = false;
    //}

    //public IEnumerator ZoomControl(GameObject areaGameObject)
    //{
    //    RoofHeightHandler roofHeightHandler = areaGameObject.GetComponentInChildren<RoofHeightHandler>();
    //    float timer1 = timers[0];
    //    float timer2 = timers[1];
    //    float timer3 = timers[2];
    //    isZoomingToSpecificPoint = true;
    //    PlayerController playerController = FindObjectOfType<PlayerController>();
    //    playerController.disablePlayerControl = true;
    //    float timer = 0f;
    //    float normalizedTimer;
    //    while (timer < timer1)
    //    {
    //        timer += Time.deltaTime;
    //        normalizedTimer = timer / timer1;
    //        transform.position = Vector3.Lerp(target.position + offset, target.position + zoomOffsetForABTest, normalizedTimer);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    timer = 0f;
    //    if (roofHeightHandler != null)
    //    {
    //        roofHeightHandler.isIn = true;
    //    }
    //    while (timer < timer2)
    //    {
    //        timer += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    if (roofHeightHandler != null)
    //    {
    //        roofHeightHandler.isIn = false;
    //    }
    //    timer = 0f;
    //    while (timer < timer3)
    //    {
    //        timer += Time.deltaTime;
    //        normalizedTimer = timer / timer3;
    //        transform.position = Vector3.Lerp(target.position + zoomOffsetForABTest, target.position + offset, normalizedTimer);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    playerController.disablePlayerControl = false;
    //    isZoomingToSpecificPoint = false;
    //}
}
