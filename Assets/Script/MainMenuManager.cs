using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // โหลดด่านแรก (ตามที่อยู่ใน Build Settings)
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        // ใช้ได้เฉพาะ Build จริง
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}
