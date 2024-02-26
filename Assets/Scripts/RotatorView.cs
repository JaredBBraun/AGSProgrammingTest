using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorView : MonoBehaviour, IRotatorView
{
    [SerializeField]
    private bool isRotating;
    [SerializeField]
    private float rotationSpeed = 1.5f;

    public void ChangeRotationDirection()
    {
        rotationSpeed *= 1;
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
        }
    }
}

public interface IRotatorView
{
    void ToggleRotation();
    void ChangeRotationDirection();
}