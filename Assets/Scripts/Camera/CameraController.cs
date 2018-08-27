﻿using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

namespace Wolfpack
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] Vector3 positionOffset = Vector3.zero;

        [Header("Speed Settings")]
        [SerializeField] float smoothSpeed = 0.0001f;

        [Header("Triggers")]
        [SerializeField] bool lookAtTarget = true;
    
        Transform target;
        Animator animator;
        Vector3 velocity = Vector3.zero;
        Camera controlledCamera;
        [Inject] IGameState gameState;
        
        void Awake()
        {
            controlledCamera = Camera.main;
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            Wolf.WolfAppeared += SetTarget;
            gameState.StateChanged += OnStateChanged;
        }

        void SetTarget(Transform target)
        {
            if (target != null)
                this.target = target;
        }

        void LateUpdate()
        {
            if (!target) return;
            var targetPos = target.position + positionOffset;
            controlledCamera.transform.position = Vector3.SmoothDamp(controlledCamera.transform.position, targetPos, ref velocity, smoothSpeed);
            if (lookAtTarget)
                controlledCamera.transform.LookAt(target);
        }

        void OnStateChanged()
        {
            var status = gameState.Value.Status;
            Debug.Log(status);
            
            if (status == GameStatus.Intro)
                StartCoroutine(PlayIntroAnimation());
        }

        IEnumerator PlayIntroAnimation()
        {
            animator.Play("Camera@Intro");
            yield return new WaitForSeconds(animator.GetClipLength("Camera@Intro"));
            gameState.SetGameStatus(GameStatus.Menu);
        }
    }
}
