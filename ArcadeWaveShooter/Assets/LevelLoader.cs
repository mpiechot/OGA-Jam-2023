#nullable enable

using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private GameLevels? gameLevelsContainer;

    private GameObject? currentLevelObject;

    private int currentLevel = -1;

    /// <summary>
    ///     Loads the level with the given index from <see cref="GameLevels.Levels"/>.
    /// </summary>
    /// <param name="level">The level index to load.</param>
    /// <remarks>New levels are created below the <see cref="MonoBehaviour"/> this component is on.</remarks>
    public void LoadLevel(int level)
    {
        if (currentLevelObject != null && (currentLevel == level || level < 0 || level >= gameLevelsContainer?.Levels.Count))
        {
            return;
        }

        UnloadCurrentLevel();
        currentLevel = level;

        currentLevelObject = Instantiate(gameLevelsContainer?.Levels[currentLevel], Vector3.zero, Quaternion.identity, transform);
    }

    /// <summary>
    ///     Loads the next level from <see cref="GameLevels.Levels"/>.
    /// </summary>
    /// <remarks>New levels are created below the <see cref="MonoBehaviour"/> this component is on.</remarks>
    public void NextLevel()
    {
        LoadLevel(currentLevel + 1);
    }

    /// <summary>
    ///     Loads the previous level from <see cref="GameLevels.Levels"/>.
    /// </summary>
    /// <remarks>New levels are created below the <see cref="MonoBehaviour"/> this component is on.</remarks>
    public void PreviousLevel()
    {
        LoadLevel(currentLevel - 1);
    }

    /// <summary>
    ///     Unloads the current loaded level.
    /// </summary>
    public void UnloadCurrentLevel()
    {
        Destroy(currentLevelObject);
    }

    /// <summary>
    ///     Reloads the current loaded level.
    /// </summary>
    /// <remarks>New levels are created below the <see cref="MonoBehaviour"/> this component is on.</remarks>
    public void ReloadLoadedLevel()
    {
        UnloadCurrentLevel();
        LoadLevel(currentLevel);
    }

    /// <summary>
    ///     Resets the level loader so when calling <see cref="NextLevel"/>, it will load the first level in <see cref="GameLevels.Levels"/>.
    /// </summary>
    public void ResetLevelLoader()
    {
        currentLevel = -1;
        UnloadCurrentLevel();
    }
}
