﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform attachedCamera;
    public bool isCursorHidden = true;
    public float minPitch = -80f, maxPitch = 80f;
    public Vector2 speed = new Vector2(120f, 120f);
    public float resolveSpeed = 10f;

    private Vector2 euler; // Current rotation of camera
    private Vector3 targetOffset, currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Is the cursor supposed to be hidden?
        if (isCursorHidden)
        {
            // Lock hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // Invisible
        }
        // Get current camera euler
        euler = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the euler with Mouse movement
        euler.y += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
        euler.x -= Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;

        euler.x = Mathf.Clamp(euler.x, minPitch, maxPitch);

        targetOffset = Vector3.Lerp(targetOffset, Vector3.zero, resolveSpeed * Time.deltaTime);
        currentOffset = Vector3.MoveTowards(currentOffset, targetOffset, resolveSpeed * Time.deltaTime);

        transform.localEulerAngles = new Vector3(0f, euler.y, 0f);
        attachedCamera.localEulerAngles = new Vector3(euler.x, 0f, 0f);
    }

    public void SetTargetOffset(Vector3 offset)
    {
        targetOffset = offset;
    }
}
