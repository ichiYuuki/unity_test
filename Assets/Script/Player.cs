﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 4f;
	public float jumpPower = 700; //ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer
	public GameObject mainCamera;
	public GameObject bullet;
	
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded; //着地判定
	private Renderer renderer;
	
	void Start () {
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer>();  
	}
	
	void Update ()
	{
		//Linecastでユニティちゃんの足元に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.07f,
			groundLayer);
		//スペースキーを押し、
		if (Input.GetKeyDown ("space")) {
			//着地していた時、
			if (isGrounded) {
				//Dashアニメーションを止めて、Jumpアニメーションを実行
				anim.SetBool("Dash", false);
				anim.SetTrigger("Jump");
				//着地判定をfalse
				isGrounded = false;
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			}
		}
		//上下への移動速度を取得
		float velY = rigidbody2D.velocity.y;
		//移動速度が0.1より大きければ上昇
		bool isJumping = velY > 0.1f ? true:false;
		//移動速度が-0.1より小さければ下降
		bool isFalling = velY < -0.1f ? true:false;
		//結果をアニメータービューの変数へ反映する
		anim.SetBool("isJumping",isJumping);
		anim.SetBool("isFalling",isFalling);
		
		if (Input.GetKeyDown ("left ctrl")) {
			anim.SetTrigger ("Shot");
			Instantiate (bullet,transform.position + new Vector3(0f,1.2f,0f),transform.rotation);
		}
	}
	
	void FixedUpdate ()
	{
		float x = Input.GetAxisRaw ("Horizontal");
		if (x != 0) {
			rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			anim.SetBool ("Dash", true);
			if (transform.position.x > mainCamera.transform.position.x - 4) {
				Vector3 cameraPos = mainCamera.transform.position;
				cameraPos.x = transform.position.x + 4;
				mainCamera.transform.position = cameraPos;
			}
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
			Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
			transform.position = pos;
		} else {
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			anim.SetBool ("Dash", false);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemy") {
			StartCoroutine ("Damage");
		}
	}
	
	IEnumerator Damage(){
		gameObject.layer = LayerMask.NameToLayer ("PlayerDamage");
		int count = 10;
		while (count > 0) {
			renderer.material.color = new Color(1,1,1,0);
			yield return new WaitForSeconds(0.05f);
			renderer.material.color = new Color(1,1,1,1);
			yield return new WaitForSeconds	(0.05f);
			count--;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
}
