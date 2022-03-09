using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI

public class InteractScript : MonoBehaviour
{
    // KEVIN WRITE YOUR INTERACT CODE HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    Camera camera;
    GameObject player;
    Collider collider;

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Cast a ray towards the center of the screen and grab the
        // interactable which the ray hits

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, collider.radius)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

        // Display interaction prompt of the interactable

        if (Interactable) {
            // Display interaction prompt of the interactable
            Text interactText = GetDescription().toString();

            if (Input.GetKeyDown("e")) {
                
                interactable.Interact();
            }
        }
    }
}
