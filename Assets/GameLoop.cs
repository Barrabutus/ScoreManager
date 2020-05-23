using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    
    public GameState state;
    public InputField PlayerGuess;
    public InputField PlayerName;
    public InputField NumberToGuess;

    [Header("Game Config")]
    public int RandomNumber;
    public int MinNumber = 10;
    public int MaxNumber = 100;
    public bool DidCheat;
    public Text GuessResultDisplay;
    public int GuessCounter;
    public Button StartGameBtn;
    public Button GuessBtn;
    public List<Score> PlayerScores = new List<Score>();

    [Header("Leaderboard Settings")]
    public GameObject HighscorePrefab;
    public Transform LeaderboardDisplay;

    private WebdataManager DbManager;


    private void Start() {
        //REFERENCE TO OUR PHP SCRIPT TO SEND A HIGHSCORE WHEN GAME IS OVER.
        DbManager = GetComponent<WebdataManager>();
    }
    void Update() 
    {
        //We are using the GameState Enum here to check what state the game is currently at. 

        switch(state)
        {

            case GameState.Prepare:
            break;


            case GameState.Playing:



            break;


            case GameState.GameOver:

                //Upload the highscore to the database. 
                UploadHighScore();
                //Did the Database manager return any response from the request??
                if(DbManager.GetWebResponse() != "")
                {
                    //It seems that the upload went ok
                    //Get all the highscores from the database.
                    GetHighScores();
                    //Reset all the game variables, properties.
                    ResetGame();
                    //Re-enable the StartGameBtn
                    StartGameBtn.interactable = true;
                    //Set the gamestate to Prepare
                    state = GameState.Prepare;
                }


            break;

           

        }

    }

    private void ResetGame()
    {

        //Reset our guess counter.
        GuessCounter = 0;
        //Reset the random number.
        RandomNumber = 0;

    }

    public void OnClickStartGame()
    {
        //If the user clicks the start game button we need to make preparations to tsart the game
        PrepareGame();
    }


    void PrepareGame()
    {
        //Assing the DidCheat boolean to false. 
        DidCheat = false;
        //Get a random number for the playre to guess.
        RandomNumber = UnityEngine.Random.Range(MinNumber, MaxNumber);
        //Reset the PlayerName and PlayerGuess fields.
        PlayerName.text = "";
        PlayerGuess.text = "";


        if(MinNumber == 0 && MaxNumber == 0)
        {

            state = GameState.Prepare;
            
        }else{

            StartGameBtn.interactable = false;
            state = GameState.Playing;
            GuessBtn.interactable = true;

        }

        //Game Preparation is done, Now we can do more check for username empty and such but this example does no error checking.

    }

    public void OnClickCheckGuess()
    {
        int guess = Convert.ToInt16(PlayerGuess.text);
        if(guess == RandomNumber)
        {
            if(!DidCheat)
            {
                GuessResultDisplay.text = "YOU GUESSED THE NUMBER IN " +GuessCounter+ " GUESSES.";
            }else{
                GuessResultDisplay.text = "TRY WITHOUT CHEATING!!";
            }
            state = GameState.GameOver;
            


        }else{
        
            if(guess > RandomNumber)
            {
                GuessResultDisplay.text ="Guess is too High";
            }else{
                GuessResultDisplay.text ="Guess is too low";
            }
            GuessCounter++;
            
        }

    }

    public void OnClickShowNumber()
    {

        NumberToGuess.text = RandomNumber.ToString();
        DidCheat = true;

    }

    public void UploadHighScore()
    {     
        DbManager.AddHighScore(PlayerName.text, GuessCounter);

        if(DbManager.GetWebResponse() == "OK")
        {
            //Debug.Log("DB UPDATED");
            DbManager.ClearWebResponse();
        }else{
            //Debug.Log("DB ERROR");
        }
            DbManager.GetHighScore();


    }


    public void GetHighScores()
    {
       


        string response = DbManager.GetAllHighScores();
        Debug.Log("RESPONSE IS "+ response);
        //DbManager.ClearWebResponse();
        //Send the data we got from the page to be "prepared" for display.
        ParseScoreData(response);
        //Populate the leaderboard.
        PopulateLeaderboard();


    }

    public void PopulateLeaderboard()
    {
        foreach(Score score in PlayerScores)
        {
            //We create a prefab of a players score to display in the list. 
            GameObject PlayerPrefab = Instantiate(HighscorePrefab, LeaderboardDisplay.position, Quaternion.identity);
            //This will access the 2 text components which display name and score.
            Text[] PrefabTexts = PlayerPrefab.GetComponentsInChildren<Text>();
            // Update the prefab text id.
            PrefabTexts[0].text = score.id;
            //Update the prefab score text component and convert to a string.
            PrefabTexts[1].text = score.playerscore.ToString();
            //Parent this to the "leaderboard"
            PlayerPrefab.transform.SetParent(LeaderboardDisplay.transform);
        }
    }

    //This function splits the data we receive from the PHP page into rows.
    public void ParseScoreData(string scoredata)
    {
        //Split all the string by # this will give us a dynamic array of strings. 
        string[] scores = scoredata.Split('#');
        /*
        EXAMPLE :
        id score #  id score # id score 
        will become 
        id score # 
        id score # 
        id score
        */
        foreach(string RowOfData in scores)
                {
                    if(RowOfData != "")
                    {
                        
                        string[] ScoreRow;
                        //Split the RowOfData by -
                        ScoreRow = RowOfData.Split('-');
                        //Create a new score object.
                        Score newScoreData = new Score();
                        //Assign this Score ID to the 1st segment of the row of data this will be the ID from the DB
                        newScoreData.id = ScoreRow[0];
                        //Assign the score to the player and convert it to an integer.
                        newScoreData.playerscore = Convert.ToInt64(ScoreRow[1]);
                        //Save this score in the newScoreData
                        PlayerScores.Add(newScoreData);
                    }
     
                }          
                
    }      

     
      
}



[System.Serializable]
public class Score
{


    public string id;
    public long playerscore;




}
