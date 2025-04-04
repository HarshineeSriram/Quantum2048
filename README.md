<a id="top"></a>
# Quantum 2048 Code Walkthrough

> 2048 is a puzzle game that was created by Italian web developer Gabriele Cirulli and published on GitHub. The game's objective is to slide numbered tiles on a grid and combine them until they reach the number 2048. It was originally written in JavaScript and CSS over a weekend, and released on March 9th, 2014 as free and open-source software.
> 
> Inspired by the original 2048 game, we've created a Quantum 2048 game that teaches the player some important concepts in quantum computing with the help of quantum-inspired 2048 gameplay.

This guide provides an in-depth overview of the core scripts used in Quantum 2048. It is designed to help developers understand the game’s architecture and serves as a reference for making changes or adding new features.

---

## Table of Contents

1. [GameManager.cs](#gamemanagercs)
   - [Singleton Setup and Lifecycle](#singleton-setup-and-lifecycle)
   - [Serialized Fields and Components](#serialized-fields-and-components)
   - [Game Lifecycle and Score Management](#game-lifecycle-and-score-management)
   - [Fade Coroutine](#fade-coroutine)
2. [AudioManager.cs](#audiomanagercs)
   - [Singleton and Persistence](#singleton-and-persistence)
   - [Audio Source Configuration and Toggle](#audio-source-configuration-and-toggle)
3. [PauseMenu.cs](#pausemenucs)
   - [UI Elements and Initialization](#ui-elements-and-initialization)
   - [Managing Pause and Tutorial Text](#managing-pause-and-tutorial-text)
4. [PopUpSystem.cs](#popupsystemcs)
   - [Popup and UI Element Management](#popup-and-ui-element-management)
   - [Handling Pause via Popups](#handling-pause-via-popups)
5. [RadialProgress.cs](#radialprogresscs)
   - [UI Component Setup](#ui-component-setup)
   - [Progress Bar Updates and Completion](#progress-bar-updates-and-completion)
6. [Tile.cs](#tilecs)
   - [Overview and Purpose](#overview-and-purpose)
   - [Component Initialization](#component-initialization)
   - [Tile Behaviors: Spawning, Moving, and Merging](#tile-behaviors-spawning-moving-and-merging)
7. [TileBoard.cs](#tileboardcs)
   - [Board Initialization and Setup](#board-initialization-and-setup)
   - [Tile Creation, Movement, and Merging](#tile-creation-movement-and-merging)
   - [Tunneling Merges and Progress Updates](#tunneling-merges-and-progress-updates)
   - [Game Over Detection](#game-over-detection)
8. [TileCell.cs](#tilecellcs)
   - [Overview and Properties](#tilecell-overview-and-properties)
9. [TileGrid.cs](#tilegridcs)
   - [Grid Structure and Initialization](#grid-structure-and-initialization)
   - [Cell Retrieval and Navigation](#cell-retrieval-and-navigation)
   - [Random Empty Cell Selection](#random-empty-cell-selection)
10. [TileRow.cs](#tilerowcs)
    - [Row Representation and Initialization](#row-representation-and-initialization)
11. [TileState.cs](#tilestatecs)
    - [Scriptable Object for Tile States](#scriptable-object-for-tile-states)
12. [tutorial.cs](#tutorialcs)
    - [Tutorial Flow and Scene Transition](#tutorial-flow-and-scene-transition)
13. [Making Changes to the Code](#making-changes-to-the-code)
14. [Additional Notes](#additional-notes)
15. [Adding your Unity game to Wordpress](#adding-your-unity-game-to-wordpress)

[Jump to top](#top)

---

<a id="gamemanagercs"></a>
## 1. GameManager.cs

<a id="singleton-setup-and-lifecycle"></a>
### Singleton Setup and Lifecycle
- **Purpose:**  
  Ensures a single instance of the `GameManager` exists throughout the game.
- **Key Implementation:**  
  The `Awake()` method checks for an existing instance and uses `DontDestroyOnLoad` to persist the manager across scenes.

<a id="serialized-fields-and-components"></a>
### Serialized Fields and Components
- **Key Fields:**  
  - **TileBoard (`board`):** Manages game tiles.
  - **CanvasGroup (`gameOver`):** Controls the game over screen's UI.
  - **TextMeshProUGUI (`scoreText`, `hiscoreText`):** Display current score and high score.

<a id="game-lifecycle-and-score-management"></a>
### Game Lifecycle and Score Management
- **NewGame Method:**  
  Resets the score, updates UI, hides the game over screen, clears the board, and spawns two tiles.
- **GameOver Method:**  
  Disables gameplay and triggers a fade-in effect for the game over screen.
- **Score Methods:**  
  `IncreaseScore` and `SetScore` update the score and save the high score using Unity's `PlayerPrefs`.

<a id="fade-coroutine"></a>
### Fade Coroutine
- **Purpose:**  
  Smoothly interpolates the UI's alpha to transition elements like the game over screen.

[Jump to top](#top)

---

<a id="audiomanagercs"></a>
## 2. AudioManager.cs

<a id="singleton-and-persistence"></a>
### Singleton and Persistence
- **Purpose:**  
  Ensures that only one instance of the `AudioManager` persists between scenes.
- **Implementation:**  
  The `Awake()` method uses a singleton pattern with `DontDestroyOnLoad`.

<a id="audio-source-configuration-and-toggle"></a>
### Audio Source Configuration and Toggle
- **Setup:**  
  Retrieves and configures the `AudioSource` component to loop background music.
- **ToggleMusic():**  
  Pauses or resumes music playback based on its current state.

[Jump to top](#top)

---

<a id="pausemenucs"></a>
## 3. PauseMenu.cs

<a id="ui-elements-and-initialization"></a>
### UI Elements and Initialization
- **UI References:**  
  Dynamically locates UI elements such as the Competency Counter, Info Button, and the pause panel.
- **Tutorial Texts:**  
  Initializes arrays of title and description texts based on the current game level (e.g., tunnelling levels).

<a id="managing-pause-and-tutorial-text"></a>
### Managing Pause and Tutorial Text
- **Pause Method:**  
  Activates the pause panel and sets `Time.timeScale` to 0.
- **Continue Method:**  
  Either displays the next set of tutorial texts or resumes gameplay by deactivating the pause panel and restoring `Time.timeScale`.

[Jump to top](#top)

---

<a id="popupsystemcs"></a>
## 4. PopUpSystem.cs

<a id="popup-and-ui-element-management"></a>
### Popup and UI Element Management
- **Purpose:**  
  Isolates popup behavior (e.g., for tunnelling events) into a dedicated script.
- **Implementation:**  
  Finds the "Tunnelling1" popup and "Info Button" within the Canvas and manages their activation.

<a id="handling-pause-via-popups"></a>
### Handling Pause via Popups
- **Tunneling Method:**  
  Activates the tunneling popup and calls the pause functionality.
- **Pause Method:**  
  Adjusts UI elements based on popup status (e.g., disables the Info Button when a popup is active).

[Jump to top](#top)

---

<a id="radialprogresscs"></a>
## 5. RadialProgress.cs

<a id="ui-component-setup"></a>
### UI Component Setup
- **Components:**  
  Manages two radial progress bars along with associated UI elements such as icons and dialogs.
- **Initialization:**  
  Finds and configures UI elements, deactivating secondary progress bars until needed.

<a id="progress-bar-updates-and-completion"></a>
### Progress Bar Updates and Completion
- **Updates:**  
  Adjusts fill amounts based on progress values.
- **Completion Coroutines:**  
  When a progress bar fills, it triggers a coroutine that changes colors, pauses the game, activates subsequent UI elements, and eventually resumes gameplay.

[Jump to top](#top)

---

<a id="tilecs"></a>
## 6. Tile.cs

<a id="overview-and-purpose"></a>
### Overview and Purpose
- **Purpose:**  
  Represents an individual tile on the game board.
- **Functionality:**  
  Each tile holds a state (including number and colors) and is linked to a specific grid cell.

<a id="component-initialization"></a>
### Component Initialization
- **Key Components:**  
  - **Image:** Provides the tile's background.
  - **TextMeshProUGUI:** Displays the tile's number.

<a id="tile-behaviors-spawning-moving-and-merging"></a>
### Tile Behaviors: Spawning, Moving, and Merging
- **Spawn():**  
  Places the tile in a designated cell.
- **MoveTo():**  
  Animates the tile moving smoothly to a new cell.
- **Merge():**  
  Handles merging of tiles (with support for tunneling merges), using a coroutine (`Animate()`) for smooth transitions.
- **Animation:**  
  The `Animate()` coroutine interpolates the tile’s position over a short duration.

[Jump to top](#top)

---

<a id="tileboardcs"></a>
## 7. TileBoard.cs

<a id="board-initialization-and-setup"></a>
### Board Initialization and Setup
- **Purpose:**  
  Manages the entire game board, including tile creation, movement, and merging.
- **Components:**  
  - **TileGrid:** Organizes grid cells.  
  - **Tile List:** Maintains all active tiles on the board.  
  - **UI and Popups:** References for tunnelling popups, info buttons, and progress components.  
    - **Info Button Inactive by Default:** The `infoButton` is disabled in `Awake()` but can be enabled if desired.

<a id="tile-creation-movement-and-merging"></a>
### Tile Creation, Movement, and Merging
- **CreateTile():**  
  Instantiates a new tile in a random empty cell.
- **Move():**  
  Iterates over grid cells to move tiles based on player input.
- **MoveTile():**  
  Handles individual tile movement and checks for merge conditions.
- **MergeTiles() and TunnelingMergeTiles():**  
  Merges tiles and updates their states. Tunneling merges skip over a “blocker” tile (which remains on the board by default) and can trigger popups, particle effects, and progress bar updates.

<a id="tunneling-merges-and-progress-updates"></a>
### Tunneling Merges and Progress Updates
- **Tunneling Logic (tunnelling1 and tunnelling2):**  
  - **‘tunnelling1’:** The first tunneling merge triggers a popup and sound effect (plus an optional particle effect if assigned).  
  - **‘tunnelling2’:** Merges only occur if both tiles exceed a randomly chosen threshold (from 2, 4, 8, or 16).  
  - **Blocker Tile Handling:** By default, the middle “blocker” tile remains after a tunneling merge (though the code includes a commented line to destroy it).
- **UI Updates:**  
  - Tracks the number of tunneling merges (`tunnel_merge`) to update radial progress bars.  
  - Displays popups at key milestones (e.g., first tunneling merge).

<a id="game-over-detection"></a>
### Game Over Detection
- **CheckForGameOver():**  
  Evaluates if no moves or merges remain and calls `GameManager.Instance.GameOver()` if the game is over.
- **WaitForChangesCoroutine():**  
  Waits for all movements/merges to complete before adding a new tile and checking for game over conditions.

[Jump to top](#top)

---

<a id="tilecellcs"></a>
## 8. TileCell.cs

<a id="tilecell-overview-and-properties"></a>
### Overview and Properties
- **Purpose:**  
  Represents an individual cell in the grid.
- **Key Properties:**  
  - **Coordinates:** X and Y positions within the grid.
  - **Tile Reference:** Holds the tile currently occupying the cell.
  - **Convenience Properties:** `Empty` and `Occupied` for quick checks.

[Jump to top](#top)

---

<a id="tilegridcs"></a>
## 9. TileGrid.cs

<a id="grid-structure-and-initialization"></a>
### Grid Structure and Initialization
- **Purpose:**  
  Manages the overall grid layout.
- **Components:**  
  - **Rows:** An array of `TileRow` objects.
  - **Cells:** A flat array of all `TileCell` objects.
- **Initialization:**  
  In `Awake()`, cells are assigned coordinates based on their index and grid dimensions.

<a id="cell-retrieval-and-navigation"></a>
### Cell Retrieval and Navigation
- **GetCell Methods:**  
  Retrieve a cell using either a `Vector2Int` or individual x and y coordinates.
- **GetAdjacentCell():**  
  Returns a cell adjacent to a given cell in a specified direction (with y-axis adjustments for Unity's layout).

<a id="random-empty-cell-selection"></a>
### Random Empty Cell Selection
- **GetRandomEmptyCell():**  
  Searches for an empty cell by iterating from a random starting index and wrapping around if necessary.

[Jump to top](#top)

---

<a id="tilerowcs"></a>
## 10. TileRow.cs

<a id="row-representation-and-initialization"></a>
### Row Representation and Initialization
- **Purpose:**  
  Represents a row in the grid.
- **Implementation:**  
  The `Awake()` method retrieves all `TileCell` components among the row’s child objects and stores them in an array.

[Jump to top](#top)

---

<a id="tilestatecs"></a>
## 11. TileState.cs

<a id="scriptable-object-for-tile-states"></a>
### Scriptable Object for Tile States
- **Purpose:**  
  Defines the state of a tile (number, background color, text color).
- **Usage:**  
  Marked with `[CreateAssetMenu]`, it allows developers to create new tile state assets via the Unity Editor, making it easy to tweak visual and numerical aspects of tiles without modifying code.

[Jump to top](#top)

---

<a id="tutorialcs"></a>
## 12. tutorial.cs

<a id="tutorial-flow-and-scene-transition"></a>
### Tutorial Flow and Scene Transition
- **Purpose:**  
  Manages the tutorial animations and transitions to the main game scene.
- **Key Functionality:**  
  - **Animator Control:**  
    Retrieves the `Animator` component and uses an integer parameter ("Change") to progress through animations.
  - **Input Handling:**  
    Detects any key press to trigger the next animation.
  - **Scene Transition:**  
    Once the "Change" parameter exceeds 5, the script loads the main game scene ("2048").

[Jump to top](#top)

---

<a id="making-changes-to-the-code"></a>
## 13. Making Changes to the Code

- **Extending Functionality:**  
  - To add new tile behaviors, modify or extend methods in **Tile.cs**.
  - For additional game rules or UI changes, adjust **GameManager.cs** and **TileBoard.cs**.
  - Consider improving UI feedback (animations, sounds) by updating **AudioManager.cs**, **RadialProgress.cs**, or adding new UI components.
- **Refactoring:**  
  - Utilize modularity in scripts like **PopUpSystem.cs** and **PauseMenu.cs** to introduce new tutorial or popup sequences.
  - When adding new levels or features, create additional `TileState` assets to manage visual themes.

[Jump to top](#top)

---

<a id="additional-notes"></a>
## 14. Additional Notes

- **Code Structure:**  
  The codebase leverages Unity’s component-based architecture. Each script is focused on a specific aspect of the game (e.g., grid management, tile behavior, UI interactions).
- **Debugging:**  
  Use `Debug.Log` statements within the scripts to trace functionality during development.
- **Asset Management:**  
  ScriptableObjects (like `TileState.cs`) allow you to manage game data externally, reducing the need for hard-coded values.

[Jump to top](#top)

---

<a id="adding-your-unity-game-to-wordpress"></a>
## 15. Adding your Unity game to Wordpress

- The `Docs/` folder contains Step-by-step instructions on how to export your Unity game and add it to a Wordpress site.
- These instructions are available in Markdown (.md) and PDF (.pdf) formats.

[Jump to top](#top)
