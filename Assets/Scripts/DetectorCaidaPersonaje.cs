using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectorCaidaPersonaje : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float limiteInferior = -5f;
    [SerializeField] private string nombreEscenaGameOver = "GameOver";
    
    private bool yaCayo = false;
    
    void Update()
    {
        // Verificar si el personaje cayó por debajo del límite
        if (transform.position.y < limiteInferior && !yaCayo)
        {
            CaerAlVacio();
        }
    }
    
    void CaerAlVacio()
    {
        yaCayo = true;
        Debug.Log("¡El personaje cayó al vacío! Cargando Game Over...");//prueba de consola para verificar
        
        // Cargar la escena de Game Over
        SceneManager.LoadScene(nombreEscenaGameOver);
    }
}