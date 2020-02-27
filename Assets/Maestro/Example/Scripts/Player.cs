using UnityEngine;

public sealed class Player : MonoBehaviour
{
	public Rigidbody playerRigidbody;
	public float speed = 10.0f;

	private void Update()
	{
		var delta = new Vector3(
			Input.GetAxis("Horizontal"),
			0.0f,
			Input.GetAxis("Vertical")
		);

		playerRigidbody.MovePosition(playerRigidbody.position + delta * (speed * Time.deltaTime));
	}
}
