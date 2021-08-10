using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Rigidbody2D pivot;
    [SerializeField] float spawnDelay;

    private Camera mainCamera;
    private Rigidbody2D currentBall;
    private SpringJoint2D currentSpringJoint;
    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBall == null)
            return;

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            isDragging = true;
            currentBall.bodyType = RigidbodyType2D.Kinematic;
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            currentBall.position = worldPosition;
        }
        else
        {
            if (isDragging)
                LaunchBall();

            isDragging = false;
        }


    }

    private void SpawnNewBall()
    {
        GameObject birdToSpawn = GameController.instance.NextBird();
        if (birdToSpawn == null)
            return;

        GameObject ballInstance = Instantiate(birdToSpawn, pivot.position, Quaternion.identity);
        currentBall = ballInstance.GetComponent<Rigidbody2D>();
        currentSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentSpringJoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        currentBall.bodyType = RigidbodyType2D.Dynamic;
        currentBall.gameObject.GetComponent<Projectile>().Launch();
        currentBall = null;
        Invoke(nameof(DetachBall),0.15f);


    }

    private void DetachBall()
    {
        currentSpringJoint.enabled = false;
        currentSpringJoint = null;

        Invoke(nameof(SpawnNewBall), spawnDelay);
    }

    
}