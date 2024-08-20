using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbInteraction : MonoBehaviour
{
    public GameObject objectToActivate;
    public KeyCode interactionKey = KeyCode.E;
    public string playerTag = "Player";
    public string Scene1;
    public string Scene2;
    private TimeSwapV2 timeSwapV2;
    private bool playerInRange = false;

    public float fadeTime = 0.5f;  // Duration of the fade-out effect
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        timeSwapV2 = FindAnyObjectByType<TimeSwapV2>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            objectToActivate.SetActive(true);
            TimeSwapManager.Instance.currentScene = SceneManager.GetActiveScene().name;
            timeSwapV2.Scene1 = Scene1;
            timeSwapV2.Scene2 = Scene2;
            timeSwapV2.ReAddPlayer();
            CineController.Instance.ShakeCamera(20, 2);
            TimeController.Instance.SlowTime(0.0001f, 2);
            StartCoroutine(FadeAndDestroy());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
        }
    }

    private IEnumerator FadeAndDestroy()
    {
        float timeElapsed = 0f;
        Color startColor = spriteRenderer.color;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0, timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
