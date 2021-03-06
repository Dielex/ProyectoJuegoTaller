﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static UnityEngine.UI.Text scoreText;
	public  UnityEngine.UI.Text _scoreText;
	public static UnityEngine.UI.Image chainTimerImage;
	public  UnityEngine.UI.Image _chainTimerImage;
	public UnityEngine.UI.Image _chainImage;
	public UnityEngine.UI.Image _lifeImage;
	public UnityEngine.Sprite[] chainSprites;
	public UnityEngine.Sprite[] lifeSprites;
	public static int score = 0;
	static int chain = 0;
	int initialLife;
	static int life;
	static float chainTimer = 0;
	static float maxChainTime = 1.5f;
	static int[] chainMultipliers =  {1,2,4,8,16,32}; 
	public GameObject[] powerups;
	static GameObject[] _powerups;
	static GameObject player;
	public Transform spawnPointPlayer;


	// Use this for initialization
	void Start () {
		if (player == null) {
			score = 0;
			player = (GameObject)Instantiate (playerSelectionController.playerActive, spawnPointPlayer.position, spawnPointPlayer.rotation);
			DontDestroyOnLoad (player);
		} else {
			scoreText.text = "" + score;
			player.transform.position = spawnPointPlayer.position;
			PlayerShip._healthPlayer = PlayerShip._healthPlayerMax;
		}
		scoreText = _scoreText;
		chainTimerImage = _chainTimerImage;
		_powerups = powerups;
	}
	
	// Update is called once per frame
	void Update () {
	}

	static int enemiesKilled = 0;
	public static int enemiesForPowerup = 10;

	public static void SetPowerUp(Vector3 pos){
		enemiesKilled++;
		if (enemiesKilled >= enemiesForPowerup) {
			enemiesKilled = 0;
			GameObject.Instantiate(_powerups[Mathf.Min (_powerups.Length - 1, Random.Range(0,_powerups.Length))] ,pos,Quaternion.identity);
		}
		}

	void FixedUpdate () {
		life = PlayerShip._healthPlayer;
		initialLife = PlayerShip._healthPlayerMax;
		if (chain > 0) {
			CheckChain ();
		}

		switch (chain) {
		case 0:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [0]);
			break;
		case 1:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [1]);
			break;
		case 2:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [2]);
			break;
		case 3:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [3]); 
			break;
		case 4:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [4]);
			break;
		case 5:
			_chainImage.sprite = (UnityEngine.Sprite)Instantiate (chainSprites [5]);
			break;
		default:
			break;
		}

		chainTimerImage.fillAmount = 1 - chainTimer / maxChainTime;

		if (initialLife == 3) {
			switch (life) {
			case 3:
				_lifeImage.sprite =lifeSprites[3];
				break;
			case 2:
				_lifeImage.sprite =lifeSprites[5];
				break;
			case 1:
				_lifeImage.sprite =lifeSprites[6];
				break;
			case 0:
				_lifeImage.sprite =lifeSprites[0];
				break;
			default:
				_lifeImage.sprite =lifeSprites[0];
				break;
			}
		} else if (initialLife == 2) {
			switch (life) {
			case 2:
				_lifeImage.sprite = lifeSprites[2];
				break;
			case 1:
				_lifeImage.sprite =lifeSprites[4];
				break;
			case 0:
				_lifeImage.sprite =lifeSprites[0];
				break;
			default:
				_lifeImage.sprite =lifeSprites[0];
				break;	
			}
		} else if (initialLife == 1) {
			switch (life) {
			case 1:
				_lifeImage.sprite =lifeSprites[1];
				break;
			case 0:
				_lifeImage.sprite =lifeSprites[0];
				break;
			default:
				_lifeImage.sprite =lifeSprites[0];
				break;		
			}
		}


	}

	public static void CheckChain (){
		chainTimer += Time.deltaTime;
		if(chainTimer > maxChainTime)
		{
			chainTimer = 0;
			ResetChain();
		}
	}

	public static void AddChain (){
		chain++;
		if (chain > 5)
			chain = 5;
		chainTimer = 0;
	}

	public static void ResetChain (){
		chain = 0;
		chainTimer = 0;
	}

	public static void AddScore(int scoreValue){
		chainTimer = 0;
		score += scoreValue * chainMultipliers[chain];
		scoreText.text = "" + score;
		ScoreShower.Show (scoreValue,chain,PlayerShip.playerPosition);
	}
	
}
