using UnityEngine;

public class CameraPositioner : ICameraPositioner
{
    private readonly Transform _cameraTransform;
    public CameraPositioner(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    public void CenterCamera(int gridRows, int gridColumns)
    {
        var newPos = _cameraTransform.position;
        newPos.x = gridColumns / 2f;
        newPos.y = gridRows / 2f;
        _cameraTransform.position = newPos;
    }
}