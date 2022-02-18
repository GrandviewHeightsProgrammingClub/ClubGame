using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    // KEVIN WRITE YOUR INTERACT CODE HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    // Start is called before the first frame update
    void Start()
    {
      camera = player camera
      collider = player collider

    }

    // Update is called once per frame
    void Update()
    {
        // if the player is looking at an interactable, obtain it.
        Interactable = shoot ray from camera towards cursor stopping at collider

        // prompt the player to press 'e' in order to interact with the
        // interactable
        if Interactable exists:
            print('Interact(E)') under the cursor
            if key 'e' is pressed:
                execute code of Interactable
    }
}
