using Peculia.Time;
using UnityEngine;

public class OnGuiSampleController : MonoBehaviour {
	public int waitSeconds = 3 * 60;  // Interval for free gift: 3 minutes.

	void OnGUI() {
		if (textStyle == null)
			SetUpStyles();

		GUILayout.BeginHorizontal(GUI.skin.label, GUILayout.Width(Screen.width));
		GUILayout.Label("FreeGift:", textStyle);
		// Main routine: Control Free Gift.
		if (FreeGiftTimer.IsValid()) {
			if (FreeGiftTimer.CanSend()) {
				// Can get a free gift.
				if (GUILayout.Button("Get", buttonStyle)) {
					FreeGiftTimer.SetNextWait(waitSeconds);
				}
			} else {
				// Cannot get a free gift because the time has not yet come.
				GUILayout.Label("Next: " + FreeGiftTimer.GetRemainingTime() + " sec later", textStyle);
			}
		} else {
			// Cannot get a free gift because the clock is invalid.
			GUILayout.Label("Unavailable", textStyle);
		}
		GUILayout.EndHorizontal();
	}

	private const int LABEL_FONT_SIZE = 24;
	private const int BUTTON_FONT_SIZE = 24;
	private const int BUTTON_PADDING = 8;
	private const int TEXTAREA_FONT_SIZE = 18;

	private GUIStyle textStyle;
	private GUIStyle buttonStyle;
	private GUIStyle toggleStyle;
	private GUIStyle textAreaStyle;

	private void SetUpStyles() {
		textStyle = new GUIStyle(GUI.skin.label);
		textStyle.fontSize = LABEL_FONT_SIZE;
		textStyle.normal.textColor = Color.white;

		buttonStyle = GUI.skin.button;
		buttonStyle.fontSize = BUTTON_FONT_SIZE;
		var padding = buttonStyle.padding;
		padding.left = padding.right = padding.top = padding.bottom = BUTTON_PADDING;
		buttonStyle.padding = padding;

		toggleStyle = GUI.skin.toggle;
		toggleStyle.fontSize = BUTTON_FONT_SIZE;
		var margin2 = toggleStyle.padding;
		margin2.left = margin2.right = margin2.top = margin2.bottom = BUTTON_PADDING;
		toggleStyle.padding = margin2;

		textAreaStyle = new GUIStyle(GUI.skin.textArea);
		textAreaStyle.fontSize = TEXTAREA_FONT_SIZE;
		textAreaStyle.normal.textColor = Color.white;
	}

	static OnGuiSampleController() {
		// Advanced topic:
		// Set custom save/restore delegates (default: use PlayerPrefs).
		string KEY_FREE_GIFT = "freeGift";
		FreeGiftTimer.SetSaveRestoreDelegate(
			// Save delegate
			(long value) => {
				// Replace to your custom save method.
				PlayerPrefs.SetString(KEY_FREE_GIFT, value.ToString());
			},
			// Restore delegate
			() => {
				// Replace to your custom restore method.
				var value = PlayerPrefs.GetString(KEY_FREE_GIFT);
				try {
					return long.Parse(value);
				} catch (System.Exception) {
					// Return 0 when restore is failed.
					return 0;
				}
			});
	}
}
