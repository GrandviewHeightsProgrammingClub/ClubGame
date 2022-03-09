using UnityEngine;

// The base class for all interactables. Any specific interactables should be
// implemented through a derived class with it's own InteractionPrompt and
// Interact methods.

public abstract class Interactable : MonoBehaviour {

    public abstract string InteractionPrompt();
    public abstract void Interact();
}
