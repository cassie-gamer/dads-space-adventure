using UnityEngine;

/// <summary>
/// Dad's Space Adventure — Asteroid.cs
/// Grey space rocks that drift across the Asteroid Belt (Level 2).
/// Bumping into one costs Dad one shield. Hearts repair shields.
/// Attach to each Asteroid GameObject (with a CircleCollider2D
/// set to Is Trigger, tagged "Asteroid").
/// </summary>
public class Asteroid : MonoBehaviour
{
    [Header("Rock movement")]
    public float driftSpeed = 2f;
    public float spinSpeed = 40f;
    public Vector2 driftDirection = Vector2.left;

    void Update()
    {
        transform.Translate(driftDirection.normalized * driftSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

        // Rocks that drift off the left side pop back on the right.
        if (transform.position.x < -12f)
            transform.position = new Vector3(12f, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        DadRocket dad = other.GetComponent<DadRocket>();
        if (dad == null) return;

        dad.HitByRock();

        // The rock breaks apart after the bump, so it cannot hit twice.
        Destroy(gameObject);
    }
}

/// <summary>
/// Friendly comet — ride its tail for a speed boost!
/// Attach to a Comet GameObject (trigger collider, tagged "Comet").
/// </summary>
public class Comet : MonoBehaviour
{
    public float flySpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * flySpeed * Time.deltaTime, Space.World);
        if (transform.position.x < -13f)
            transform.position = new Vector3(13f, Random.Range(-4f, 4f), transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        DadRocket dad = other.GetComponent<DadRocket>();
        if (dad != null) dad.CometBoost();
    }
}

/// <summary>
/// A floating heart — grab it to repair one of Dad's shields.
/// Attach to a Heart GameObject (trigger collider, tagged "Heart").
/// </summary>
public class HeartPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        DadRocket dad = other.GetComponent<DadRocket>();
        if (dad == null) return;
        dad.RepairShield();
        Destroy(gameObject);
    }
}
