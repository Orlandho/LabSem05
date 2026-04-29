using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void IrReintentar()
    {
      SceneManager.LoadScene(1);//Lo haremos por indice
    }
      public void IrMenuPrincipal()
    {
      SceneManager.LoadScene(0);//Lo haremos por indice
    }
}