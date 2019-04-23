using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FPSViewScript : NetworkBehaviour{

    private Quaternion playerTargetRotation;
    private Quaternion cameraTargetRotation;
    private bool cursorLock = true;

    public float xSens = 2f;
    public float ySens = 2f;
    public bool clampVerticalRotation = true;
    public float minimumX = -90F;
    public float maximumX = 90F;
    public bool lockCursor = true;
    public bool limitRocket;


    public void Initialisation(Transform character, Transform camera)
    {
        if (hasAuthority)
        {
            playerTargetRotation = character.localRotation;
            cameraTargetRotation = camera.localRotation;
        }
        else
        {
            return;
        }
    }


    public void LookRotation(Transform character, Transform camera)
    {
        if (transform.parent == isLocalPlayer)
        {
            float yRot = Input.GetAxis("Mouse X") * xSens;
            float xRot = Input.GetAxis("Mouse Y") * ySens;

            playerTargetRotation *= Quaternion.Euler(0f, yRot, 0f);
            cameraTargetRotation *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
            {
                cameraTargetRotation = ClampRotationAroundXAxis(cameraTargetRotation);
            }

            character.localRotation = playerTargetRotation;
            camera.localRotation = cameraTargetRotation;
            UpdateCursorLock();

        }
        else
        {
            return;
        }
    
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minimumX, maximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}