using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{

    // This enumeration describes which directions this script should control
    [Flags]
    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    [SerializeField] private RotationDirection rotationDirections;

    [SerializeField] private Vector2 acceleration;

    [SerializeField] private Vector2 sensitivity;

    [SerializeField] private float maxVerticalAngleFromHorizon;

    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity; // The current rotation velocity, in degrees
    private Vector2 rotation; // The current rotation, in degrees
    private Vector2 lastInputEvent; // The last received non-zero input value
    private float inputLagTimer; // The time since the last received non-zero input value

    // When this component is enabled, we need to reset the state
    // and figure out the current rotation
    private void OnEnable()
    {
        // Reset the state
        velocity = Vector2.zero;
        inputLagTimer = 0;
        lastInputEvent = Vector2.zero;

        // Calculate the current rotation by getting the gameObject's local euler angles
        Vector3 euler = transform.localEulerAngles;
        // Euler angles range from [0, 360), but we want [-180, 180)
        if (euler.x >= 180)
        {
            euler.x -= 360;
        }
        euler.x = ClampVerticalAngle(euler.x);
        // Set the angles here to clamp the current rotation
        transform.localEulerAngles = euler;
        // Rotation is stored as (horizontal, vertical), which corresponds to the euler angles
        // around the y (up) axis and the x (right) axis
        rotation = new Vector2(euler.y, euler.x);
    }

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput()
    {
        // Add to the lag timer
        inputLagTimer += Time.deltaTime;
        // Get the input vector. This can be changed to work with the new input system or even touch controls
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );
        // Sometimes at fast framerates, Unity will not receive input events every frame, which results
        // in zero values being given above. This can cause stuttering and make it difficult to fine
        // tune the acceleration setting. To fix this, disregard zero values. If the lag timer has passed the
        // lag period, we can assume that the user is not giving any input, so we actually want to set
        // the input value to zero at that time.
        // Thus, save the input value if it is non-zero or the lag timer is met
        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }

    private void Update()
    {
        // The wanted velocity is the current input scaled by the sensitivity
        // This is also the maximum velocity
        Vector2 wantedVelocity = GetInput() * sensitivity;

        // Zero out the wanted velocity if this controller does not rotate in that direction
        if ((rotationDirections & RotationDirection.Horizontal) == 0)
        {
            wantedVelocity.x = 0;
        }
        if ((rotationDirections & RotationDirection.Vertical) == 0)
        {
            wantedVelocity.y = 0;
        }

        // Calculate new rotation
        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
}