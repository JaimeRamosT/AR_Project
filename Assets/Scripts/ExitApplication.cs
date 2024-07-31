using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void QuitGame()
    {
        // Salir de la aplicación
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}