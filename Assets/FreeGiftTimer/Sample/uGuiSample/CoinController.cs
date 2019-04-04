using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour {
	private static readonly string KEY_COIN_COUNT = "coinCount";

	public Text coinText;
	public GameObject coinPrefab;
	public Transform coinRoot;
	public Image giftBoxImage;

	private int coinCount;
	private int dispCount;

	void Awake() {
		coinCount = dispCount = RestoreCoinCount();
	}

	void Start() {
		UpdateCoinText();
	}

	public void Earn(int count, Action callback) {
		coinCount += count;
		SaveCoinCount(coinCount);
		StartCoroutine(DoDemo(count, callback));
	}

	private IEnumerator DoDemo(int count, Action callback) {
		// Animate gift box.
		giftBoxImage.gameObject.SetActive(true);
		giftBoxImage.color = Color.white;
		float T = 2.0f;
		for (var time = 0.0f; time < T; time += Time.deltaTime) {
			yield return null;
			var t = time / T;
			var s = 1.0f - t * 0.25f;
			var angle = Mathf.Sin(t * 2 * Mathf.PI * 100) * (1.0f - t) * 10;
			giftBoxImage.transform.localScale = new Vector3(s, s, 1);
			giftBoxImage.transform.localEulerAngles = new Vector3(0, 0, angle);
		}
		giftBoxImage.gameObject.SetActive(false);


		float R = 250;
		GameObject[] coins = new GameObject[count];
		for (var i = 0; i < count; ++i) {
			var go = Instantiate(coinPrefab);
			go.transform.SetParent(coinRoot, false);
			go.transform.localPosition = new Vector3(UnityEngine.Random.Range(-R, R), UnityEngine.Random.Range(-R, R), 0);
			coins[i] = go;
		}

		yield return new WaitForSeconds(1.0f);

		for (var i = 0; i < count; ++i) {
			++dispCount;
			UpdateCoinText();
			Destroy(coins[i]);
			yield return null;
		}

		yield return new WaitForSeconds(1.0f);

		callback();
	}

	private void UpdateCoinText() {
		coinText.text = string.Format("{0}", dispCount);
	}

	private static int RestoreCoinCount() {
		return int.Parse(PlayerPrefs.GetString(KEY_COIN_COUNT, "0"));
	}

	private static void SaveCoinCount(int coinCount) {
		PlayerPrefs.SetString(KEY_COIN_COUNT, coinCount.ToString());
	}
}
