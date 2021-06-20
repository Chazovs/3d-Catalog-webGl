using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;
    public GameObject MainCamera;
    private float graviryForce;

    private CharacterController chController;

    public float sensitivyHor = 9f;

    private void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivyHor, 0);

        GamingGravity();
    }


    private void CharacterMove()
    {
        var moveVector = Vector3.zero;
        moveVector.y = graviryForce;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += MainCamera.transform.forward * speedMove * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= MainCamera.transform.forward * speedMove * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += MainCamera.transform.right * speedMove * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector -= MainCamera.transform.right * speedMove * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && chController.isGrounded)
        {
            graviryForce = jumpPower;
            moveVector.y = graviryForce;
        }

        chController.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!chController.isGrounded)
        {
            graviryForce -= 20f * Time.deltaTime;
        }
        else
        {
            graviryForce = -1f;
        }
    }
}