#nullable enable

using ArcardeWaveShooter.Exceptions;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Level", menuName = "ArcardeGame/Game Level")]
public class GameLevels : ScriptableObject
{
    [SerializeField]
    private List<GameObject>? levels;

    /// <summary>
    ///     Gets all available levels.
    /// </summary>
    public IReadOnlyList<GameObject> Levels => levels ?? throw new SerializeFieldNotAssignedException();
}
