using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer characterRenderer;
    [SerializeField] private List<Color> levels;

    private int currentLevel = 1;

    private void Start()
    {
        ApplyColor();
    }

    public void LevelUp()
    {
        if (currentLevel < levels.Count)
        {
            currentLevel++;
            ApplyColor();
        }
    }

    private void ApplyColor()
    {
        characterRenderer.material.color = levels[currentLevel - 1];
    }
}