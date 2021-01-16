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
    // Edit Tommy
    private Animator animator;
    public enum handsAnimationState {Idle, Flying, Walking};
    public handsAnimationState handsAnimation;

    void Start()
    {
        animator = transform.Find("Arms").gameObject.GetComponent<Animator>();
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

            animator.SetTrigger("cast1");
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

            if (input.z != 0 || input.x != 0) 
            {
                handsAnimation = handsAnimationState.Walking;
            } 
            else
            {
                handsAnimation = handsAnimationState.Idle;
            }

            // To implement if you want to see a different flying animation =)       
            bool isOnGround = false;
            // ---------------------
            if ((input.z != 0 || input.x != 0) && isOnGround) {
                handsAnimation = handsAnimationState.Flying;
            }
           

            Rigidbody parentRb = transform.parent.GetComponent<Rigidbody>();
            // reset horizontal (xz) speed to 0
            Vector3 originalVel = parentRb.velocity;
            originalVel.x = 0.0f;
            originalVel.z = 0.0f;
            parentRb.velocity = originalVel;
            transform.parent.Rotate(Vector3.up, 300.0f * MAIN_CAM_DELTA_TIME * Input.GetAxis("Mouse X"));
            transform.parent.Rotate(Vector3.right, -300.0f * MAIN_CAM_DELTA_TIME * Input.GetAxis("Mouse Y"));
            //transform.parent.Translate(new Vector3(0.0f, input.y, 0.0f), Space.World);
            //transform.parent.Translate(Vector3.ProjectOnPlane(transform.parent.transform.forward, Vector3.up).normalized * input.z, Space.World);
            parentRb.velocity += Vector3.ProjectOnPlane(transform.parent.transform.forward, Vector3.up).normalized * input.z * 30;
            //transform.parent.Translate(Vector3.ProjectOnPlane(transform.parent.transform.right, Vector3.up).normalized * input.x, Space.World);
            parentRb.velocity += Vector3.ProjectOnPlane(transform.parent.transform.right, Vector3.up).normalized * input.x * 30;
        }

        UpdateAnimatorStates();
         
        transform.parent.transform.rotation = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x,
            transform.parent.transform.rotation.eulerAngles.y, 0.0f);
    }

    private void UpdateAnimatorStates() {
        switch (handsAnimation) {
            case handsAnimationState.Idle:
                animator.SetBool("idle",true);
                animator.SetBool("walking", false);
                animator.SetBool("flying", false);
                break;
            case handsAnimationState.Walking:
                animator.SetBool("walking", true);
                animator.SetBool("idle", false);
                animator.SetBool("flying", false);
                break;
            case handsAnimationState.Flying:
                animator.SetBool("flying", true);
                animator.SetBool("idle", false);
                animator.SetBool("walking", false);
                break;
        }
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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            input.w = 1.0f;
        }

        return input;
    }
}