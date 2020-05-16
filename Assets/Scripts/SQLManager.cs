// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// //Class for managing SQL Commands
// public class SQLManager : MonoBehaviour
// {
//     private string webPostUrl = "http://scribbleword/DatabaseConnector.php"; // CHANGE THIS TO YOUR WEB ADDRESS>....
//     public string urlResult;
//     public string action;
//     //Currently every 2 seconds.
//     public SQLManager instance;
//     public string urlresult;
//     public int lastInsertId;
//     public List<Score> ScoreList = new List<Score>();
//     [System.Serializable]
//     //THIS IS A LIST OF THE POSSIBLE ACTIONS. 
//     public enum DBCommand
//     {
//         ADD,
//         GET,
    
//     }
//     public DBCommand command;


//     // Start is called before the first frame update
//     void Start()
//     {
//         DontDestroyOnLoad(this.gameObject);
//         instance = this;
//     }


//     public void _GetRows()
//     {
//         action = DBCommand.GET.ToString();
//         StartCoroutine(GetRows());
        
//     }

//     public void _AddScore(Score score)
//     {
//         action = DBCommand.ADD.ToString();
//         StartCoroutine(AddNew(score));
//        // Debug.Log("ADDING USER...");
        
//     }



//     //For getting data. 
//     //I only want this to run once to retrieve data and then the user can "get" the result from UrlResult?
//     IEnumerator GetRows() {
//         List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
//         wwwForm.Add(new MultipartFormDataSection("action", action));
//         UnityWebRequest www = UnityWebRequest.Post(webPostUrl, wwwForm);
//         yield return www.SendWebRequest();
//         if(www.isNetworkError || www.isHttpError) {
//           ShowSQLError(www.error);
//         }
//         else {
//            urlresult = www.downloadHandler.text;      
//         } 
//     }


//     //ADDS A New Score
//     //NOTE THE wwwForm 
//     IEnumerator AddNew(Score score)
//     {
//         List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
//         wwwForm.Add(new MultipartFormDataSection("action", action));
//         wwwForm.Add(new MultipartFormDataSection("uniqueID", Convert.ToString(score.uniqueId)));
//         wwwForm.Add(new MultipartFormDataSection("score", Convert.ToString(score.score)));
//         ///wwwForm.Add(new MultipartFormDataSection(My $_POST['value'], My Value))
//         UnityWebRequest www = UnityWebRequest.Post(webPostUrl, wwwForm);
//         yield return www.SendWebRequest();

//             if(www.isNetworkError || www.isHttpError)
//             {
//                ShowSQLError(www.error);
//             }else{
//                 urlResult = www.downloadHandler.text;
//                // lastInsertId = Convert.ToInt16(www.downloadHandler.text);
//             }

//     }



// //Get the results from the PHP Page?
//     public string getUrlResponse()
//     {
        
//         return urlResult;
//     }

    

//     public List<Score> GetScoreList()
//     {
//         return ScoreList;
//     }

//     public void ShowSQLError(string error)
//     {

//         Debug.Log(error);

//     }

//     public int getLastInsertId()
//     {
//         return lastInsertId;
//     }
    



// }
