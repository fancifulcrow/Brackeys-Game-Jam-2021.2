using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Vector2 initialPosition;
    private bool isLaunched;
    public float launchSpeed = 300f;
    public float maxDragDistance;
    public float timeBeforeLosing = 10f;
    public GameObject GameUI;
    public GameObject LoseMenu;
    public AudioSource audiosource;
    public AudioClip launchSound;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseUp()
    {
        Vector2 directionToInitialPosition = initialPosition - new Vector2(transform.position.x, transform.position.y);

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * launchSpeed);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        isLaunched = true;
        audiosource.PlayOneShot(launchSound);
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = newPosition;

        float distance = Vector2.Distance(desiredPosition, initialPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - initialPosition;
            direction.Normalize();
            desiredPosition = initialPosition + (direction * maxDragDistance);
        }

        transform.position = desiredPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(LoseGame());
    }

    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(timeBeforeLosing);
        if (GameUI.activeSelf == true)
        {
            GameUI.SetActive(false);
            LoseMenu.SetActive(true);
            Debug.Log("You have lost");
        }
    }
}
