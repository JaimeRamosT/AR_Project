using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void QuitGame()
    {
        // Salir de la aplicaci�n
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}