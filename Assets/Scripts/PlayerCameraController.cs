using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCameraController : MonoBehaviour
{
    private Camera m_Camera;
    private bool m_MouseLocked;
    public bool LockSprinting;
    public bool Sprinting;
    private float m_Fov = 60.0f;
    private float m_FovTarget = 70.0f;
    private DateTime m_FovAnimationStartTime;
    private bool m_DoFovAnimation;
    public Shader DepthShader;
    private static readonly float MAIN_CAM_DELTA_TIME = 1.0f / 60.0f;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        InvokeRepeating(nameof(UpdateCamera), 0.0f, MAIN_CAM_DELTA_TIME);
    }

    private void UpdateCamera()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.Escape))
        {
            m_MouseLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(0))
        {
            m_MouseLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        Vector4 input = GetUserInput();

        if (Math.Abs(input.w - 1.0f) < 0.01f || LockSprinting)
        {
            if (!Sprinting)
            {
                Sprinting = true;
                // start fov animation
                m_FovAnimationStartTime = DateTime.Now;
                m_DoFovAnimation = true;
            }

            if (m_DoFovAnimation)
            {
                TimeSpan deltaTime = DateTime.Now - m_FovAnimationStartTime;
                if (deltaTime.Milliseconds < 75)
                {
                    // do animation
                    m_Camera.fieldOfView = Mathf.Lerp(m_Fov, m_FovTarget, deltaTime.Milliseconds / 100.0f);
                }
                else
                {
                    m_DoFovAnimation = false;
                }
            }
            else
            {
                m_Camera.fieldOfView = m_FovTarget;
            }

        }
        else
        {
            if (Sprinting)
            {
                Sprinting = false;
                m_Camera.fieldOfView = m_Fov;
                // start fov animation
                m_FovAnimationStartTime = DateTime.Now;
                m_DoFovAnimation = true;
            }

            if (m_DoFovAnimation)
            {
                TimeSpan deltaTime = DateTime.Now - m_FovAnimationStartTime;
                if (deltaTime.Milliseconds < 75)
                {
                    // do animation
                    m_Camera.fieldOfView = Mathf.Lerp(m_FovTarget, m_Fov, deltaTime.Milliseconds / 100.0f);
                }
                else
                {
                    m_DoFovAnimation = false;
                }
            }
            else
            {
                m_Camera.fieldOfView = m_Fov;
            }
        }

        input *= 25.0f * MAIN_CAM_DELTA_TIME;
        if (Sprinting)
        {
            input *= 2.0f;
        }

        if (m_MouseLocked)
        {
            m_Camera.transform.Rotate(Vector3.up, 300.0f * MAIN_CAM_DELTA_TIME * Input.GetAxis("Mouse X"));
            m_Camera.transform.Rotate(Vector3.right, -300.0f * MAIN_CAM_DELTA_TIME * Input.GetAxis("Mouse Y"));

            m_Camera.transform.Translate(new Vector3(0.0f, input.y, 0.0f), Space.World);
            m_Camera.transform.Translate(Vector3.ProjectOnPlane(m_Camera.transform.forward, Vector3.up).normalized * input.z, Space.World);
            m_Camera.transform.Translate(Vector3.ProjectOnPlane(m_Camera.transform.right, Vector3.up).normalized * input.x, Space.World);
        }
        m_Camera.transform.rotation = Quaternion.Euler(m_Camera.transform.rotation.eulerAngles.x,
            m_Camera.transform.rotation.eulerAngles.y, 0.0f);
    }

    private Vector4 GetUserInput()
    {
        Vector4 input = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
        {
            input.z = 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.z = -1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x = 1.0f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            input.y = 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            input.y = -1.0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            input.w = 1.0f;
        }

        return input;
    }
}