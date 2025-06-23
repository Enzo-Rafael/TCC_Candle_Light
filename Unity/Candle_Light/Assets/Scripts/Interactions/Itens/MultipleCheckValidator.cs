using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Verifica se todos os UseRotateInteractable estao "corretos"
/// </summary>
public class MultipleRotationValidator : MonoBehaviour, IMultiple
{
    [SerializeField]
    private UseRotateInteractable[] interactables;

    void Awake()
    {
        interactables = GetComponentsInChildren<UseRotateInteractable>();
    }

    public bool Validator(object additionalInformation)
    {
        foreach (UseRotateInteractable rotInteractable in interactables)
        {
            if (!rotInteractable.CheckCorrect()) return false;
        }
        return true;
    }
}
