// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class UnityScene : MonoBehaviour
// {

//     public SQLManager _sqlManager;
//     public List<Score> Scores = new List<Score>();

//     public InputField uniqueIdText; 
//     public InputField scoreInputBox;
//     // Start is called before the first frame update
//     void Start()
//     {
//         _sqlManager = GameObject.Find("SQLManager").GetComponent<SQLManager>();
//         uniqueIdText.text = Random.Range(00000,99999).ToString();
        
//     }

//     public void OnClickGetScores()
//     {

//         _sqlManager._GetRows();
//         Debug.Log(_sqlManager.getUrlResponse());



//     }

//     public void OnClickSaveScore()
//     {
//         Score newScore =new Score();
//         newScore.score = scoreInputBox.text;
//         _sqlManager._AddScore(newScore);
//         Debug.Log(_sqlManager.getUrlResponse());

//     }
// }

// public class Score
// {

//     public int uniqueId;
//     public string score;


// }
