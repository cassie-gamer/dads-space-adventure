using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Dad's Space Adventure — SpaceLevelManager.cs
/// Dad is the hero astronaut! Fly his rocket through 3 long levels
/// and collect all 20 lost stars, then land on the Moon to win.
///
///   Level 1 — Earth Orbit:   collect 5 stars
///   Level 2 — Asteroid Belt: collect 7 stars (dodge the rocks!)
///   Level 3 — Moon:          collect 8 stars, then land on the
///                            glowing Moon pad to win!
///
/// Total: 5 + 7 + 8 = 20 lost stars.
/// Winning shows: "Best Dad in the Galaxy!"
///
/// Attach to an empty GameObject called SpaceLevelManager in each
/// level scene. Set levelNumber (1, 2 or 3) in the Inspector.
/// </summary>
public class SpaceLevelManager : MonoBehaviour
{
    [Header("This level")]
    public int levelNumber = 1;
    public string levelName = "Earth Orbit";

    [Header("Stars in this level")]
    public int starsNeeded = 5;   // 5 in Level 1, 7 in Level 2, 8 in Level 3

    [Header("Moon landing (Level 3 only)")]
    public GameObject moonPad;    // The glowing landing pad — turn it on when all stars are collected

    [Header("HUD")]
    public Text starsText;        // e.g. "Stars: 3 / 5"
    public Text levelText;        // e.g. "Level 1: Earth Orbit"
    public Text winText;          // Hidden until Dad wins

    private int starsCollected = 0;
    private bool levelComplete = false;

    public static int totalStarsCollected = 0;  // Carried across all 3 levels

    void Start()
    {
        // Stars needed for each level, in case the Inspector was not set.
        if (levelNumber == 1) { starsNeeded = 5; levelName = "Earth Orbit"; }
        if (levelNumber == 2) { starsNeeded = 7; levelName = "Asteroid Belt"; }
        if (levelNumber == 3) { starsNeeded = 8; levelName = "Moon"; }

        if (levelNumber == 1) totalStarsCollected = 0;

        if (moonPad != null) moonPad.SetActive(false);
        if (winText != null) winText.gameObject.SetActive(false);

        UpdateHUD();
    }

    /// <summary>Called by StarCollector when Dad grabs a star.</summary>
    public void CollectStar()
    {
        starsCollected++;
        totalStarsCollected++;
        UpdateHUD();

        if (starsCollected >= starsNeeded && !levelComplete)
        {
            levelComplete = true;
            LevelStarsDone();
        }
    }

    void LevelStarsDone()
    {
        if (levelNumber < 3)
        {
            // Next level, please! (Scene names: "Level1", "Level2", "Level3")
            DadRocket dad = FindFirstObjectByType<DadRocket>();
            if (dad != null)
                dad.SayCheer("All the stars in " + levelName + "! On to the next adventure!");

            Invoke(nameof(NextLevel), 2f);
        }
        else
        {
            // Level 3: all 8 Moon stars are collected — the Moon pad lights up!
            if (moonPad != null) moonPad.SetActive(true);
            DadRocket dad = FindFirstObjectByType<DadRocket>();
            if (dad != null)
                dad.SayCheer("All 20 stars! Now land on the glowing Moon pad!");
        }
    }

    /// <summary>Called when Dad's rocket touches the glowing Moon pad (Level 3).</summary>
    public void LandOnMoon()
    {
        if (levelNumber != 3 || !levelComplete) return;

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.text = "🌟 Best Dad in the Galaxy! 🌟\n" +
                           "You collected all 20 lost stars\n" +
                           "and landed safely on the Moon!";
        }

        DadRocket dad = FindFirstObjectByType<DadRocket>();
        if (dad != null) dad.SayCheer("We did it! Best Dad in the Galaxy!");
    }

    /// <summary>Called by DadRocket when all shields are gone.</summary>
    public void DadCrashed()
    {
        DadRocket dad = FindFirstObjectByType<DadRocket>();
        if (dad != null) dad.SayCheer("Oh no! Let's try that level again!");
        Invoke(nameof(RestartLevel), 2f);
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level" + (levelNumber + 1));
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateHUD()
    {
        if (starsText != null)
            starsText.text = "⭐ Stars: " + starsCollected + " / " + starsNeeded +
                             "   (Total: " + totalStarsCollected + " / 20)";
        if (levelText != null)
            levelText.text = "Level " + levelNumber + ": " + levelName;
    }
}
