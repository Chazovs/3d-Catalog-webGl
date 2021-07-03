using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class CustomerController : MonoBehaviour
{

	public float speed = 1.5f;

	public Transform head;

	public float sensitivity = 5f; // чувствительность мыши
	public float headMinY = -40f; // ограничение угла для головы
	public float headMaxY = 40f;

	public KeyCode jumpButton = KeyCode.Space; // клавиша для прыжка
	public float jumpForce = 10; // сила прыжка
	public float jumpDistance = 1.2f; // расстояние от центра объекта, до поверхности

	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;
	private float rotationY;
    private RaycastHit hit;
 
    void Start()
	{
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}

	void FixedUpdate()
	{
		body.AddForce(direction * speed, ForceMode.VelocityChange);

		// Ограничение скорости, иначе объект будет постоянно ускоряться
		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
		}

		if (Mathf.Abs(body.velocity.z) > speed)
		{
			body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
		}
	}

	bool GetJump() // проверяем, есть ли коллайдер под ногами
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, Vector3.down);

		if (Physics.Raycast(ray, out hit, jumpDistance, layerMask))
		{
			return true;
		}

		return false;
	}

	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// управление головой (камерой)
		float rotationX = head.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		rotationY += Input.GetAxis("Mouse Y") * sensitivity;
		rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
		head.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		// вектор направления движения
		direction = new Vector3(h, 0, v);
		direction = head.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			body.velocity = new Vector2(0, jumpForce);
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			MouseZeroDownAction();
		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			MouseOneDownAction();
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			KeyRAction();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			Application.OpenURL(Main.serverName + Main.confirmOrderUrl);
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			KeyZAction();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			KeyQAction();
		}
	}

    private void KeyQAction()
    {
		Client client = GameObject.Find("Client").GetComponent<Client>();
		client.GetBasket();
    }

    private void KeyZAction()
    {
		Transform transform = GameObject.Find("LeftCanva").transform;

		transform.Find("Title").GetComponent<Text>().text = "";
		transform.Find("PriceTitle").GetComponent<Text>().text = "";
		transform.Find("Summ").GetComponent<Text>().text = "";
		transform.Find("Description").GetComponent<Text>().text =
			"<b>W</b> - вперед \n"+
			"<b>S</b> - назад \n"+
			"<b>A</b> - влево \n"+
			"<b>D</b> - вправо \n"+
			"<b>Q</b> - Показать корзину \n"+
			"<b>Space</b> - прыжок \n"+
			"<b>Левая клавиша мыши</b> - добавить в корзину \n" +
			"<b>Правая клавиша мыши</b> - информация о товаре \n" +
			"<b>R</b> - убрать из корзины \n" +
			"<b>L</b> - оформить заказ";
	}

    private void KeyRAction()
    {
		Interactive? content = GetBaseContainer();

		if (content != null)
		{
			content.KeyRActio();
		}
	}

    private void MouseOneDownAction()
    {
		Interactive? content = GetBaseContainer();

		if (content != null)
		{
			content.MouseOneDown();
		}
	}

	private Interactive? GetBaseContainer()
    {
		GameObject MainCamera = head.gameObject;

		Ray ray = MainCamera
			.GetComponent<Camera>()
			.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		if (!Physics.Raycast(ray, out hit))
		{
			return null;
		}

		GameObject hitObject = hit.collider.gameObject;
		BaseContainer container = hitObject.GetComponent<BaseContainer>();

		if (container == null || container.content == null)
		{
			return null;
		}

		return container.content;
	}

    private void MouseZeroDownAction()
    {
		Interactive? content = GetBaseContainer();

		if (content != null)
        {
			content.MouseZeroDown();
		}
    }

    void OnDrawGizmosSelected() // подсветка, для визуальной настройки jumpDistance
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, Vector3.down * jumpDistance);
	}
}
