using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    bool canBob = true;
    float walkBobSpeed = 14f;
    float walkBobAmount = 0.1f;

    float defaultYPos = 0;
    float timer;
    private Camera playerCamera;
    private Vector3 lastPosition;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        defaultYPos = playerCamera.transform.localPosition.y;
    }


    private void Update()
    {
        if (lastPosition != gameObject.transform.position && canBob)
        {
            HandleHeadBob();
        }
        lastPosition = gameObject.transform.position;
    }

    private void HandleHeadBob()
    {
        timer += Time.deltaTime * walkBobSpeed;
        float a = playerCamera.transform.localPosition.x;
        float b = defaultYPos + Mathf.Sin(timer) * walkBobAmount;
        float c = playerCamera.transform.localPosition.z;
        playerCamera.transform.localPosition = new Vector3(a, b, c);
    }
}
