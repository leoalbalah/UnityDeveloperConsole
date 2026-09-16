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

There's a difference between what the **console itself** needs and what **this repo's dev project** happens to include — most people only care about the former.

### Package dependencies (what your project needs to use the console)

* `com.unity.inputsystem` — the Input System package. The console's key bindings and the Tools menu's auto-generated EventSystem both run on it.
* `com.unity.ugui` (Unity 6+, TextMeshPro is bundled in) — or `com.unity.textmeshpro` + TMP Essential Resources on older Unity versions.

That's it — `Assets/DevTools` doesn't reference URP, Timeline, Visual Scripting, or anything else below.

### Development project (this repo's `Unity/UnityDeveloperConsole`)

Built with **Unity 6000.6.0f1** on the Universal Render Pipeline template, which pulls in extra packages the console doesn't actually use — `com.unity.render-pipelines.universal`, `com.unity.timeline`, `com.unity.visualscripting`, `com.unity.ai.navigation`, `com.unity.test-framework`, `com.unity.collab-proxy` — plus the sample scene and URP settings that ship with the template. These only matter if you're building/testing the console in this repo, not if you're consuming the released package.

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

## Status & Support

This is a personal tool I built for my own projects and use as-is — I don't plan on offering active support. That said, if you run into a bug or have a feature request, feel free to open a [GitHub Issue](https://github.com/leoalbalah/UnityDeveloperConsole/issues); for a bigger idea or a question, a [Discussion](https://github.com/leoalbalah/UnityDeveloperConsole/discussions) is a better fit. I can't promise a timeline, but it's the right place to reach me.

## License

[MIT](./LICENSE)
