using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dad's Space Adventure — StarCollector.cs
/// The 20 lost stars are scattered through space. Fly Dad's rocket
/// into a star to collect it! Stars twinkle and spin so they are
/// easy to spot. Attach to each Star GameObject (with a
/// CircleCollider2D set to Is Trigger, tagged "Star").
/// </summary>
public class StarCollector : MonoBehaviour
{
    [Header("Star sparkle")]
    public float spinSpeed = 90f;
    public float bobAmount = 0.15f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Twinkle: spin and bob gently up and down.
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * 2f) * bobAmount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only Dad's rocket can collect a star.
        if (!other.CompareTag("Player")) return;

        SpaceLevelManager manager = FindFirstObjectByType<SpaceLevelManager>();
        if (manager != null) manager.CollectStar();

        DadRocket dad = other.GetComponent<DadRocket>();
        if (dad != null) dad.SayCheer("Got a star! Shiny!");

        // The star is collected — it disappears with a happy pop.
        Destroy(gameObject);
    }
}
