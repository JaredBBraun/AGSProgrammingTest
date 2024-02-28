using UnityEngine;

public class RotatorView : MonoBehaviour, IRotatorView
{
    [SerializeField]
    private bool isRotating;
    [SerializeField]
    private float rotationSpeed = -1.5f;

    public void ChangeRotationDirection(bool enabled)
    {
        if (!enabled)
        {
            rotationSpeed *= -1;
        }
    }

    public void Reset()
    {
        isRotating = false;
        transform.rotation = Quaternion.identity;
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
    void Reset();
    void ToggleRotation();
    void ChangeRotationDirection(bool enabled); //simpler function with no bool? since it always just flips
}