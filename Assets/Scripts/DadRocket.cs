using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dad's Space Adventure — DadRocket.cs
/// Dad is the hero astronaut! He flies his rocket with a big smiling
/// face in the window. Works with touch buttons on iPhone / iPad and
/// with arrow keys / WASD on a Windows PC.
///
/// Attach to the Rocket GameObject (with a Rigidbody2D).
/// Drag the big on-screen buttons and the speech-bubble Text into
/// the fields in the Inspector.
/// </summary>
public class DadRocket : MonoBehaviour
{
    [Header("Flying")]
    public float flySpeed = 6f;
    public float boostSpeed = 11f;
    public float boostTime = 2f;
    public Rigidbody2D rb;

    [Header("Shields & Hearts")]
    public int maxShields = 3;
    public int shields = 3;

    [Header("Dad's cheering")]
    public Text speechBubble;              // Dad's speech bubble
    public string[] cheers = {
        "Wheee! Here we go!",
        "You're the best co-pilot ever!",
        "Look at all those stars!",
        "Hold on tight — BOOST!",
        "Best Dad in the Galaxy, coming through!"
    };

    [Header("Touch buttons (set these from your UI)")]
    public bool steerLeft;
    public bool steerRight;
    public bool steerUp;
    public bool steerDown;

    private bool boosting;
    private float boostTimer;
    private float cheerTimer;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        shields = maxShields;
        SayCheer("Ready? Let's fly, superstar!");
    }

    void Update()
    {
        // Keyboard steering (Windows PC)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Touch steering — big buttons set these while held down
        if (steerLeft) x = -1f;
        if (steerRight) x = 1f;
        if (steerUp) y = 1f;
        if (steerDown) y = -1f;

        // BOOST: Space key, or the big BOOST touch button
        if (Input.GetKeyDown(KeyCode.Space)) StartBoost();

        float speed = boosting ? boostSpeed : flySpeed;
        Vector2 move = new Vector2(x, y).normalized * speed;
        rb.linearVelocity = move;

        // Tip the rocket a little towards where it is flying
        if (move.x != 0f)
            transform.rotation = Quaternion.Euler(0f, 0f, -move.x * 12f);

        if (boosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f) boosting = false;
        }

        // Dad cheers every few seconds
        cheerTimer -= Time.deltaTime;
        if (cheerTimer <= 0f)
        {
            SayCheer(cheers[Random.Range(0, cheers.Length)]);
            cheerTimer = 5f;
        }
    }

    /// <summary>Called by the big BOOST touch button.</summary>
    public void StartBoost()
    {
        boosting = true;
        boostTimer = boostTime;
        SayCheer("BOOST! Wheee!");
    }

    /// <summary>Called by a friendly comet flying past.</summary>
    public void CometBoost()
    {
        StartBoost();
        SayCheer("A comet! Catch its tail — super speed!");
    }

    /// <summary>Called when a rock hits the rocket.</summary>
    public void HitByRock()
    {
        shields--;
        SayCheer("Ouch! That rock bumped us!");
        if (shields <= 0)
        {
            // SpaceLevelManager will restart the level.
            FindFirstObjectByType<SpaceLevelManager>()?.DadCrashed();
        }
    }

    /// <summary>Called when Dad grabs a heart.</summary>
    public void RepairShield()
    {
        if (shields < maxShields)
        {
            shields++;
            SayCheer("A heart! Shields fixed!");
        }
        else
        {
            SayCheer("A heart! Feeling super strong!");
        }
    }

    public void SayCheer(string message)
    {
        if (speechBubble != null) speechBubble.text = "Dad: " + message;
    }

    // Touch-button helpers — wire these to PointerDown / PointerUp.
    public void SetLeft(bool held) { steerLeft = held; }
    public void SetRight(bool held) { steerRight = held; }
    public void SetUp(bool held) { steerUp = held; }
    public void SetDown(bool held) { steerDown = held; }
}
