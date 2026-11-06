# Dad's Space Adventure 🚀⭐

A game for Dad! **Dad is the hero astronaut** in his rocket, with a big smiling face in the window.

The night sky lost its stars! Dad flies his rocket through space to find all **20 lost stars**, bring them home, and light up the sky again.

## The story

One night, all the stars went missing from the sky. Dad puts on his astronaut suit, climbs into his rocket, and says:

> "Don't worry — Dad will find them!"

You help him fly!

## 3 long levels

| Level | Name | Goal |
|-------|------|------|
| 1 | **Earth Orbit** | Collect **5 stars** |
| 2 | **Asteroid Belt** | Collect **7 stars** — dodge the grey rocks! A rock costs 1 shield |
| 3 | **Moon** | Collect **8 stars**, then land on the **glowing Moon pad** |

**Total: 5 + 7 + 8 = 20 lost stars!**

Win the game by landing on the Moon pad in Level 3:
> 🌟 **Best Dad in the Galaxy!** 🌟

## How to play

- **Steer:** Big touch arrows on screen (iPhone / iPad), or arrow keys / WASD on a Windows PC
- **BOOST:** The big BOOST button, or the Space key
- **Stars:** Fly into a glowing star to collect it
- **Rocks:** Grey asteroids drift across Level 2 — bumping one costs 1 shield. Dad starts with 3 shields. If all shields are gone, the level restarts.
- **Hearts:** Grab a floating heart to repair 1 shield
- **Comets:** Friendly comets fly past — touch one for a super speed boost!
- **Dad cheers:** Watch Dad's speech bubble — he cheers you on the whole way!

## Unity scripts (in `Assets/Scripts/`)

- `DadRocket.cs` — Dad's rocket: steering, BOOST, shields, hearts, comet boosts, and Dad's cheering speech bubble
- `StarCollector.cs` — the twinkling, spinning lost stars you collect
- `Asteroid.cs` — drifting rocks that cost a shield, plus friendly `Comet` speed boosts and `HeartPickup` shield repairs
- `SpaceLevelManager.cs` — the 3 levels (5 / 7 / 8 stars), the Moon landing pad, and the "Best Dad in the Galaxy!" win

## How to build it in Unity (on Windows)

1. Open Unity Hub → New project → **2D** → name it `DadsSpaceAdventure`
2. Clone this repo, or copy the `Assets/Scripts/` folder into your project
3. Make 3 scenes called `Level1`, `Level2`, `Level3` and add them in **File → Build Settings**
4. In each scene:
   - Add a Rocket GameObject with a `Rigidbody2D`, a `CircleCollider2D`, the tag `Player`, and the `DadRocket` script. Give it a rocket picture with Dad's smiling face in the window!
   - Add an empty GameObject called `SpaceLevelManager` with the `SpaceLevelManager` script. Set `Level Number` to 1, 2 or 3.
   - Add Star GameObjects (tag `Star`, trigger collider, `StarCollector` script) — 5 in Level 1, 7 in Level 2, 8 in Level 3
   - Level 2: add Asteroid GameObjects (tag `Asteroid`, trigger collider, `Asteroid` script), plus a few hearts and a comet
   - Level 3: add a glowing Moon pad GameObject and wire it to `SpaceLevelManager.moonPad`, plus a trigger that calls `LandOnMoon()` when Dad lands on it
   - Add the HUD: `starsText`, `levelText`, `winText`, and Dad's `speechBubble`
   - Add big touch buttons for Left / Right / Up / Down and BOOST, and wire them to `DadRocket.SetLeft` / `SetRight` / `SetUp` / `SetDown` and `StartBoost`
5. Press **Play** and help Dad collect all 20 stars!

## A playable web version

There is also a playable web version of this game, `dad-s-space-adventure`, so Dad can play it right away in a browser.

Made with love, for the Best Dad in the Galaxy! 💛
