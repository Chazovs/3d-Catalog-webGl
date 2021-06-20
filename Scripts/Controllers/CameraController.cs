using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start()
    {
        /*offset = target.transform.position - transform.position;*/
        offset = transform.position;
    }

    void LateUpdate()
    {
        /*var horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);
        
        var desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);*/

        transform.position = target.transform.position;
    }
}
