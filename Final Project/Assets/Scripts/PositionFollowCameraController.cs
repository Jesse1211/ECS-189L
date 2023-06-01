using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(LineRenderer))]

    public class PositionFollowCameraController : MonoBehaviour
    {

        [SerializeField] private bool DrawLogic;
        [SerializeField] private GameObject Target;

        [SerializeField] private Vector2 topLeft = new Vector2(-25f, 25f);
        [SerializeField] private Vector2 bottomRight = new Vector2(25f, -25f);
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;
        private PlayerControllerData playerController;

        [SerializeField] private float followSpeedFactor = 0.05f;
        [SerializeField] private float leashDistance = 50.0f;
        [SerializeField] private float catchUpSpeed = 25f;
        [NonSerialized] private bool isMoving;
        [NonSerialized] private float playerSpeed;
        [NonSerialized] private Vector3 targetPosition;
        [NonSerialized] private Vector3 cameraPosition;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
            playerController = this.Target.GetComponent<PlayerControllerData>();
        }

        void LateUpdate()
        {
            playerSpeed = playerController.GetCurrentSpeed();

            isMoving = playerController.GetMovementDirection() != new Vector3(0, 0, 0);
            cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);
            // player is too far: match up the player's speed
            if (
                (targetPosition.x <= (cameraPosition.x - leashDistance)
                || targetPosition.x >= (cameraPosition.x + leashDistance)
                || targetPosition.y <= (cameraPosition.y - leashDistance)
                || targetPosition.y >= (cameraPosition.y + leashDistance))
                && isMoving)
            {
                Debug.Log("moving too far, chase with same speed" + playerSpeed);
                transform.position = Vector3.Lerp(transform.position, cameraPosition, playerSpeed * Time.deltaTime);
            }
            // player isn't moving: 
            else if (!isMoving)
            {
                Debug.Log("not moving, chase with catchUpSpeed" + catchUpSpeed);
                transform.position = Vector3.Lerp(transform.position, cameraPosition, catchUpSpeed * Time.deltaTime);
            }
            // player is nearby: speed in the target's speed times the followSpeedFactor
            else
            {
                Debug.Log("close, follow with speed * followSpeedFactor");
                transform.position = Vector3.Lerp(transform.position, cameraPosition, playerSpeed * followSpeedFactor * Time.deltaTime);
            }

            if (this.DrawLogic)
            {
                cameraLineRenderer.enabled = true;
                DrawCameraLogic();
            }
            else
            {
                cameraLineRenderer.enabled = false;
            }
        }
        private void Start()
        {
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        }

        private void Update()
        {
            targetPosition = this.Target.transform.position;
            cameraPosition = managedCamera.transform.position;
        }

        public void DrawCameraLogic()
        {
            var z = this.Target.transform.position.z - this.managedCamera.transform.position.z;

            cameraLineRenderer.positionCount = 7;
            cameraLineRenderer.useWorldSpace = false;
            cameraLineRenderer.SetPosition(0, new Vector3(topLeft.x + (bottomRight.x - topLeft.x) / 2, topLeft.y, z)); // top
            cameraLineRenderer.SetPosition(1, new Vector3(topLeft.x + (bottomRight.x - topLeft.x) / 2, bottomRight.y + (topLeft.y - bottomRight.y) / 2, z)); // mid
            cameraLineRenderer.SetPosition(2, new Vector3(topLeft.x + (bottomRight.x - topLeft.x) / 2, bottomRight.y, z)); // bot
            cameraLineRenderer.SetPosition(3, new Vector3(topLeft.x + (bottomRight.x - topLeft.x) / 2, bottomRight.y + (topLeft.y - bottomRight.y) / 2, z)); // mid
            cameraLineRenderer.SetPosition(4, new Vector3(topLeft.x, bottomRight.y + (topLeft.y - bottomRight.y) / 2, z)); // left
            cameraLineRenderer.SetPosition(5, new Vector3(topLeft.x + (bottomRight.x - topLeft.x) / 2, bottomRight.y + (topLeft.y - bottomRight.y) / 2, z)); // mid
            cameraLineRenderer.SetPosition(6, new Vector3(bottomRight.x, bottomRight.y + (topLeft.y - bottomRight.y) / 2, z)); // right
        }
    }

}
