using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private GameObject player;
	private int speed = 10;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("UnityChan");
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (speed * player.transform.localScale.x,rigidbody2D.velocity.y);
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		Destroy (gameObject,5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
