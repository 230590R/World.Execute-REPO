using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float height = 5f;  
    public float speed = 2f;  
    public string playerTag = "Player";  
    public KeyCode InputKey = KeyCode.E;  

    private bool isMoving = false;
    private bool movingUp = true;

    private Vector3 startPosition;
    private Vector3 topPosition;
    private bool playerInTrigger = false;

    private Transform playerTransform;

    void Start()
    {
        startPosition = transform.position;
        topPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(InputKey) && !isMoving)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            MoveElevator();
            if (playerInTrigger && playerTransform != null)
            {
                playerTransform.SetParent(transform); 
            }
            else if (!playerInTrigger && playerTransform != null)
            {
                playerTransform.SetParent(null); 
            }
        }
    }

    void MoveElevator()
    {
        Vector3 targetPosition = movingUp ? topPosition : startPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
            movingUp = !movingUp;

            if (playerTransform != null)
            {
                playerTransform.SetParent(null); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInTrigger = true;
            playerTransform = other.transform;
            playerTransform.SetParent(transform); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInTrigger = false;
            if (playerTransform != null)
            {
                playerTransform.SetParent(null); 
                playerTransform = null;
            }
        }
    }
}
