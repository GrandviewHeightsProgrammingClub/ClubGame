using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractScript : MonoBehaviour
{
    // KEVIN WRITE YOUR INTERACT CODE HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public Camera camera;
    GameObject player;
    public GameObject item;
    public bool inRange = false;
    public bool holding = false;
    public LayerMask interactableLayermask;
    public Rigidbody rb;
    public BoxCollider col;
    public Transform Items;
    public TMP_Text itemText;

    // Update is called once per frame
    void Update()
    {
        // resets item text
        itemText.enabled = false;

        // Cast a ray towards the center of the screen and grab the
        // interactable which the ray hits
        RaycastHit hit;

        // if player is in range, not holding something, and looking at
        if (inRange && !holding && Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 1000f, interactableLayermask)) {
            // show text
            itemText.enabled = true;
            itemText.text = hit.transform.name;
            // if pressed
            if (Input.GetKeyDown("e"))
            {
                // Get game object
                item = hit.transform.gameObject;
                rb = item.GetComponent<Rigidbody>();
                col = item.GetComponent<BoxCollider>();

                // Hold Item
                holding = true;
                rb.isKinematic = true;
                col.isTrigger = true;

                //Make item a child of the camera and move it to default position
                item.transform.SetParent(Items);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.Euler(Vector3.zero);
                item.transform.localScale = Vector3.one;
            }
        }

        if (Input.GetKeyDown("q") && holding) {
            // Drop Item
            holding = false;
            rb.isKinematic = false;
            col.isTrigger = false;

            // not Items child
            item.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Items")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Items")
        {
            inRange = false;
        }
    }
}
