[System.Serializable]
public class SaveState 
{
	public int classicHighScore = 0;
	public int gapHighScore = 0;
	public int hoopHighScore = 0;
	public int protectHighScore = 0;
    public int boostHighScore = 0;

	public bool sound = true;
	public bool googleService = false;
	public bool noAd = false;
	public bool doubleStar = false;

	public bool broughtAds = false;
	public bool broughtDoubledStars;

	public bool LineType = true;

	public int star = 1000;

	public int gamesPlayedChecker;

	public int currentLineSkinIndex = 0;
	public long lineSkinAvailablility = 1;

	public int currentBallSkinIndex = 0;
	public long ballSkinAvailablility = 1;

	public int gamesPlayed;

	public int ballsBrought;
	public int linesBrought;

	public int ballIndex;
	public int lastIndex;
	
	public int lineIndex;
	public int lastLineIndex;

	public bool firstLogin;

    public bool firstPlay = true;

	//Ball Bools
	#region
	public bool item1;
	public bool item2;
	public bool item3;
	public bool item4;
	public bool item5;
	public bool item6;
	public bool item7;
	public bool item8;
	public bool item9;
	public bool item10;
	public bool item11;
	public bool item12;
	public bool item13;
	public bool item14;
	public bool item15;
	public bool item16;
	public bool item17;
	public bool item18;
	public bool item19;
	public bool item20;
	public bool item21;
	public bool item22;
	public bool item23;
	public bool item24;
	public bool item25;
	#endregion

	//Line Bools
	#region

	//Single
	public bool lineItem1;
	public bool lineItem2;
	public bool lineItem3;
	public bool lineItem4;
	public bool lineItem5;
	public bool lineItem6;
	public bool lineItem7;
	public bool lineItem8;
	public bool lineItem9;
	public bool lineItem10;
	public bool lineItem11;
	public bool lineItem12;
	public bool lineItem13;
	public bool lineItem14;
	public bool lineItem15;
	public bool lineItem16;
	public bool lineItem17;
	public bool lineItem18;
	public bool lineItem19;
	public bool lineItem20;
	public bool lineItem21;
	public bool lineItem22;

	//Double
	public bool lineItem23;
	public bool lineItem24;
	public bool lineItem25;
	public bool lineItem26;
	public bool lineItem27;
	public bool lineItem28;
	public bool lineItem29;
	public bool lineItem30;
	public bool lineItem31;
	public bool lineItem32;
	public bool lineItem33;
	public bool lineItem34;
	public bool lineItem35;
	public bool lineItem36;
	public bool lineItem37;
	public bool lineItem38;
	public bool lineItem39;
	public bool lineItem40;
	public bool lineItem41;
	public bool lineItem42;
	public bool lineItem43;
	#endregion
}
