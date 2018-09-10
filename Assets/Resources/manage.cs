using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : MonoBehaviour {

	private bool _keyLock;
	
	void Start () {
		_keyLock = false;
		Debug.Log("サーバへ接続");

		// サーバへ接続
		PhotonNetwork.ConnectUsingSettings(null);
	}
	
	// ロビーへ入室した
	void OnJoinedLobby() {
		Debug.Log("ロビーへ入室しました");
		// どこかのルームへ接続
		PhotonNetwork.JoinRandomRoom();
	}
	
	// ルームへ入室した
	void OnJoinedRoom() {
		Debug.Log("ルームへ入室しました");

		// キーロック解除
		_keyLock = true;
	}
	
	// ルームの入室へ失敗
	void OnPhotonRandomJoinFailed() {
		Debug.Log("ルーム入室が失敗");
		// 自分でルームを作成して入室
		PhotonNetwork.CreateRoom(null);
	}

	void FixedUpdate() {
		// 左クリックが押されたらオブジェクト読み込み
		if (Input.GetMouseButtonDown(0) && _keyLock) {
			GameObject mySyncObj = PhotonNetwork.Instantiate("Cube", new Vector3(9.0f, 0f, 0f), Quaternion.identity, 0);
			
			// 動きを加える
			Rigidbody mySyncObjRB = mySyncObj.GetComponent<Rigidbody>();
			mySyncObjRB.isKinematic = false;
			float rndPow = Random.Range(1.0f, 5.0f);
			mySyncObjRB.AddForce(Vector3.left * rndPow, ForceMode.Impulse);
		}
	}

	void Update () {
		
	}
}
