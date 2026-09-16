# UnityDeveloperConsole

Basic Unity Engine **Developer Console** for testing and debugging in the editor or in the build projects.

## Getting started

- **Engine:** Unity `6000.6.0f1` (Universal Render Pipeline)
- **Project location:** [`Unity/UnityDeveloperConsole`](./Unity/UnityDeveloperConsole)

Open the project via Unity Hub, or:

```
unity open Unity/UnityDeveloperConsole
```

## Repository layout

```
UnityDeveloperConsole/
├── Unity/UnityDeveloperConsole/    the Unity project
└── README.md
```

## Features

* Responsive Layout.
* Easy to add scripts.
* Some default commands included.
* FPS displayer command and UI.
* Drag & Drop Prefabs for easy setUp.
* Well documented and easy to understand code.
* Console prints the default unity log.
* <Command> -help displays the command info.
* Clean UI.
* Command history — cycle through previously entered commands with Arrow Up / Arrow Down.
* Tab-completion — press Tab to auto-complete a command, or fill in the longest shared prefix when multiple commands match.
* `DeveloperConsole.ConsoleVisibilityChanged` event fires when the console opens/closes, so other systems can pause input, etc.
* One-click scene setup via **Tools > Developer Console > Setup In Scene**, safe to re-run — it won't duplicate the console or add a second EventSystem.

### Commands Included

* FPS
* Echo
* Commands
* Clear
* Quit

## Dependencies

* Created with **Unity 6000.6.0f1**, Universal Render Pipeline template.
* Uses the new **Input System** (`com.unity.inputsystem`).
* TextMeshPro / TMP Essential Resources (import into your own project if not already present — see setup below).

## Installation & SetUp

1. Get the [Latest Release](https://github.com/leoalbalah/UnityDeveloperConsole/releases).
2. Import the Development Console unitypackage.
3. Import TMP essentials (Window > TextMeshPro > Import TMP Essential Resources), if not already present in your project.
4. Import The new Input System Package, if not already present in your project.
5. Set up the console in your scene — either:
   - **Automatic:** Use the menu **Tools > Developer Console > Setup In Scene**. This adds the console (and FPS counter) prefab, and only adds an EventSystem if the scene doesn't already have one (Unity does not support more than one active EventSystem per scene).
   - **Manual:** Add an Event System to the scene, then drag & drop the `Assets/DevTools/DeveloperConsole/PF_DeveloperConsole` prefab into the scene.
6. Adjust the canvases (in case of needed).
7. All Set. Open the terminal using the **`** character
