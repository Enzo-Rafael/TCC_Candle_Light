using System;
using UnityEngine;

[Serializable]
public class PuzzleData
{
    public int indice;
    public bool completed;

    public PuzzleData(ExecuteItemCommand puzzleExe)
    {
        indice = puzzleExe.indexPuzzle;
        completed = puzzleExe.completed;
    }
}