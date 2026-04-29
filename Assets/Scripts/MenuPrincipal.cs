using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("EscenaJuego");
    }
      public void SalirJuego() {
   		#if UNITY_EDITOR
       		UnityEditor.EditorApplication.isPlaying = false;
   		#else
       		Application.Quit();
   		#endif
	}
}