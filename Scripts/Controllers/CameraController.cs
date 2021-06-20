using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class CameraController : MonoBehaviour
{
    public GameObject target;

    void LateUpdate()
    {
        transform.position = target.transform.position;
    }
}
