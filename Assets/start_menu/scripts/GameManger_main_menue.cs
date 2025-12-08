using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManger_main_menue : MonoBehaviour
{

    public static string Status;

    public void startGame()
    {

        SceneManager.LoadScene(1);
    }

   


    public void resumeGame()
    {
        Status = "resumeGame";
        SceneManager.LoadScene(1);
 
    }


    
    public void exitGame()
    {
        Application.Quit();
    }
}
