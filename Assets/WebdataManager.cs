using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebdataManager : MonoBehaviour
{
    private string WebProcessingPageAddress = "http://localhost:8080/OddOneIn/oddonein.php"; // CHANGE THIS TO YOUR WEB ADDRESS>....
    
    /* 
    <Variable Name> WebResponse
    <Value Returned> String
    <Expected Return>
    The return value of this will be whatever you deem a "success" message to be which will be echoed in the PHP page when you send the data to it.
    <Example Output>
    An example of a success message can be a 0 or a "OK" or whatever you want to indicate a success or error.
    */
    public string WebResponse;

    /* 
    <Variable Name> AllHighScores
    <Value Returned> String
    <Data> This is the data held within the leaderboard table in your "databasename" database.
    <Expected Return>
    Id username score followed by a # so we can seperate the users for parsing and displaying in the leaderboard, 
    Or performing a calculation on scores etc.

    <Example Output>
    Yeti-99999#NewUser-43529#Joe-4242#jdfgjrtywhhdgfh-9#Heeessttttt-7#CommandAndConquer-7#
    */
    //NOTE Ideally you would use this as a class and create a new class for each WebCall
    //For ease of explaining we have just used 2 variables.
    public string AllHighScores;

    private void Start() {
        
       // Debug.Log("TEST");

    }
    public void AddHighScore(string playerName, int score)
    {
        //Send the passed score value to the CoRoutine to be processed.
        StartCoroutine(AddHighScoreToDB(playerName, score));    
       // Debug.Log("FIRED");
        
    }
    public void GetHighScore()
    {
        /*Call the CoRoutine GetAllHighScore() 
        This will query the database and then return a single string of all the values held within the database table.
        The result will be placed in AllHighScores;
        */
        StartCoroutine(GetAllHighScore());    
        //Debug.Log("1FIRED");
        
    }


    IEnumerator AddHighScoreToDB(string playerName, int score)
    {
        //We need a form object. 
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        //We need to add fields to the above form.
        /*
            MultipartFormDataSection(
                1st Argument, The $_POST value expected on the above PHP page
                2nd Argument, The actual value of the above variable.
            )
        */
        wwwForm.Add(new MultipartFormDataSection("id", playerName));
        wwwForm.Add(new MultipartFormDataSection("score", Convert.ToString(score)));

        //Send the Form to the web address to be processed using the POST method. 
        UnityWebRequest www = UnityWebRequest.Post(WebProcessingPageAddress, wwwForm);

        //Return the response from the above WebRequest.
        yield return www.SendWebRequest();

            //If we didnt encounter an error in communicating with the server at the address above.
            if(www.isNetworkError || www.isHttpError)
            {   
               //Assign the response from the PHP Page to the WebResponse variable.
                WebResponse = "ERROR";
            }else{
                WebResponse = www.downloadHandler.text;
            }

    }

    public string GetWebResponse()
    {

        return WebResponse;

    }
    public void ClearWebResponse()
    {

        WebResponse = "";

    }
    public string GetAllHighScores()
    {

        return AllHighScores;

    }


    IEnumerator GetAllHighScore()
    {
        //Request all from the PHP pages. 
        string all;
        all= "all";
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("all", all));

        //We,ll send a blank form just for ease.
        UnityWebRequest www = UnityWebRequest.Post(WebProcessingPageAddress, wwwForm);

        //Return the response from the above WebRequest.
        yield return www.SendWebRequest();

        //If we didnt encounter an error in communicating with the server at the address above.
            if(www.isNetworkError || www.isHttpError)
            {   
               //Assign the response from the PHP Page to the WebResponse variable.
                AllHighScores = "ERROR";
            }else{
                AllHighScores = www.downloadHandler.text;
            }

    }





}
