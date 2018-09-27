using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	Rigidbody2D rigid2D;
  Animator animator;
	float jumpForce = 780.0f;
	float walkForce = 30.0f;
  float maxWalkSpeed = 2.0f;
  float threshold = 0.2f;

	// Use this for initialization
	void Start () {
		this.rigid2D = GetComponent<Rigidbody2D>();
    this.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
    // 移動速度
    float speedx = Mathf.Abs(this.rigid2D.velocity.x);

		// jump
		if((Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0) || (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)) {
      // if(Input.GetMouseButtonDown(0) &&
      // this.rigid2D.velocity.y ==0) {
        this.animator.SetTrigger("JumpTrigger");
        this.rigid2D.AddForce(transform.up * this.jumpForce);
        this.animator.speed = speedx / 1.0f;
      }

		// walk
		int key = 0;
		if(Input.acceleration.x > this.threshold || Input.GetKey(KeyCode.RightArrow)) key = 1;
    if(Input.acceleration.x < -this.threshold || Input.GetKey(KeyCode.LeftArrow)) key = -1;


    // スピード制限
    if(speedx < this.maxWalkSpeed) {
      this.rigid2D.AddForce(transform.right * key * this.walkForce);
    }

    // 向きの変更
    if(key != 0) {
      transform.localScale = new Vector3(key, 1, 1);
    }

    // アニメーション速度制御
    if(this.rigid2D.velocity.y == 0) {
      this.animator.speed = speedx / 1.0f;
    } else {
      this.animator.speed = speedx / 2.0f;
    }

    // 画面外に出たときの処理
    if (transform.position.y < -10) {
        SceneManager.LoadScene("GameScene");
    }
	}

  // ゴール
  void OnTriggerEnter2D(Collider2D other) {
      Debug.Log("ゴール");
      SceneManager.LoadScene("ClearScene");
  }
}
