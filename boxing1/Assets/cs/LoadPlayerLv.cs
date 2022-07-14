using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPlayerLv : MonoBehaviour {

	public static int playerLv;
	public static int playerExp;
	public static int playerExpMax;
	public static int playerQuaLv;
	public  GameObject player;
	
	public static Text playerText;
	public Slider HpSlider;

	void Start () {
		//PlayerPrefs.DeleteAll();
		playerText = GetComponent<Text>();
		
		playerLv = PlayerPrefs.GetInt("playerLvData",1);
		playerExp = PlayerPrefs.GetInt("playerExpData",0);
		playerQuaLv = PlayerPrefs.GetInt("playerQuaLvData",1);
		UpdatePlayerLv(0);
		UpdatePlayerExp(0);
	}
	
	public static void UpdatePlayerLv(int lv) {
		
		playerLv += lv;
		
		GameObject player = GameObject.FindWithTag("Player");
		
		UpdatePlayerExp(-player.GetComponent<CharLv>().charExpMax);
		player.GetComponent<CharLv>().charAtkValue = playerLv*2;
		player.GetComponent<CharLv>().charLv=playerLv;
		player.GetComponent<HPControl>().MaxHP=playerLv*50;
		player.GetComponent<HPControl>().HP=playerLv*50;
		player.GetComponent<CharLv>().charExpMax = playerLv * 25;
		
	}
	
	public static void UpdatePlayerExp(int exp) {
		GameObject player = GameObject.FindWithTag("Player");
		
		playerExp+=exp;
		player.GetComponent<CharLv>().charExp = playerExp;
		UpdatePlayer();
	}
	
	public static void UpdatePlayer() {
		
		PlayerPrefs.SetInt("playerLvData", playerLv);
		PlayerPrefs.SetInt("playerExpData", playerExp);
		PlayerPrefs.Save();
		
		playerText.text= "player lv: " + playerLv.ToString() + "\nplayer Exp: " + playerExp.ToString();
	}
}
