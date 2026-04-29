using UnityEngine;

// ============================================================
// CONTROLADOR DE PERSONAJE 2D
// ============================================================
// 
// REQUISITOS: El personaje debe tener:
// - Sprite Renderer (para verse)
// - Rigidbody2D (para física)
// - Box Collider 2D (para colisiones)
// ============================================================

public class ControladorPersonaje2D : MonoBehaviour
{
    // ==================== VARIABLES EXPUESTAS EN EL INSPECTOR ====================
    // Los [Header] organizan las variables en el Inspector de Unity
    // [SerializeField] permite ver y editar la variable privada desde el Inspector
    
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 7f;
    // ↑ Velocidad horizontal del personaje (unidades por segundo)
    
    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 10f;
    // ↑ Fuerza hacia arriba aplicada al saltar (mayor = salta más alto)
    
    [SerializeField] private Transform puntoSuelo;
    // ↑ Objeto vacío ubicado en los pies del personaje para detectar el suelo
    
    [SerializeField] private float radioSuelo = 0.2f;
    // ↑ Radio del círculo imaginario que detecta el suelo (ajustar al tamaño del pie)
    
    [SerializeField] private LayerMask capaSuelo;
    // ↑ Capa (Layer) que identifica qué objetos son considerados "suelo"
    //   Solo los objetos en esta capa serán detectados como superficie sólida
    
    [Header("Estados")]
    [SerializeField] private bool estaEnSuelo;
    // ↑ Bandera que indica si el personaje está tocando el suelo (visible en Inspector)
    
    // ==================== VARIABLES PRIVADAS ====================
    
    private Rigidbody2D rb;
    // ↑ Referencia al componente Rigidbody2D (gravedad, física)
    
    private SpriteRenderer sr;
    // ↑ Referencia al SpriteRenderer (para cambiar colores o efectos)
    
    private float movimientoHorizontal;
    // ↑ Valor de entrada horizontal: -1 (izquierda), 0 (quieto), 1 (derecha)
    
    private bool mirandoDerecha = true;
    // ↑ Estado que indica hacia dónde mira el personaje (para girar el sprite)
    
    // ==================== MÉTODOS DE UNITY ====================
    
    /// <summary>
    /// Start se ejecuta UNA VEZ al iniciar el juego, antes del primer Update
    /// Aquí se obtienen referencias a componentes y se configuran valores iniciales
    /// </summary>
    void Start()
    {
        // Obtener el componente Rigidbody2D del mismo GameObject
        // ¡Importante! El Rigidbody2D debe estar agregado al personaje
        rb = GetComponent<Rigidbody2D>();
        
        // Obtener el componente SpriteRenderer del mismo GameObject
        sr = GetComponent<SpriteRenderer>();
        
        // Si el estudiante olvidó asignar el PuntoSuelo en el Inspector,
        // este código lo crea automáticamente en la posición de los pies
        if (puntoSuelo == null)
        {
            // Crear un GameObject vacío llamado "PuntoSuelo"
            GameObject punto = new GameObject("PuntoSuelo");
            
            // Hacer que el punto sea hijo del personaje (para que se mueva con él)
            punto.transform.parent = transform;
            
            // Posicionar el punto en la base del personaje (0.5 unidades hacia abajo)
            // Nota: Si el personaje mide 1 unidad de alto, los pies están en Y = -0.5
            punto.transform.localPosition = new Vector3(0, -0.5f, 0);
            
            // Asignar el punto creado a la variable
            puntoSuelo = punto.transform;
            
            // Avisar al estudiante en la consola
            Debug.Log("PuntoSuelo creado automáticamente en la base del personaje");
        }
    }
    
    /// <summary>
    /// Update se ejecuta CADA FRAME (~30-60 veces por segundo)
    /// Aquí se procesan: entrada del usuario, detección de suelo, lógica general
    /// NO se debe mover físicas aquí (para eso está FixedUpdate)
    /// </summary>
    void Update()
    {
        // ========== 1. LEER INPUT DEL TECLADO ==========
        // Input.GetAxisRaw devuelve:
        // -1 cuando se presiona A o ←
        //  0 cuando no hay tecla presionada
        //  1 cuando se presiona D o →
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        
        // ========== 2. DETECTAR SI ESTÁ EN EL SUELO ==========
        // Physics2D.OverlapCircle crea un círculo imaginario en los pies
        // Si ese círculo toca un objeto que está en la capa "Suelo",
        // entonces la función devuelve true (está tocando el suelo)
        estaEnSuelo = Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo);
        
        // ========== 3. DETECTAR SALTO ==========
        // Input.GetButtonDown("Jump") se activa SOLO en el frame exacto
        // que se presiona la tecla de salto (Espacio por defecto)
        // La condición && estaEnSuelo evita el "doble salto" o salto infinito
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            Saltar();  // Llamar al método que aplica la física del salto
        }
        
        // ========== 4. GIRAR EL SPRITE SEGÚN DIRECCIÓN ==========
        GirarSprite();
    }
    
    /// <summary>
    /// FixedUpdate se ejecuta a INTERVALOS FIJOS (50 veces por segundo)
    /// Es el lugar CORRECTO para aplicar fuerzas y mover físicas
    /// NO se debe leer input aquí (usar Update para eso)
    /// </summary>
    void FixedUpdate()
    {
        // Aplicar el movimiento horizontal (física)
        Mover();
    }
    
    // ==================== MÉTODOS PERSONALIZADOS ====================
    
    /// <summary>
    /// Aplica el movimiento horizontal al personaje usando Rigidbody2D
    /// IMPORTANTE: Se usa linearVelocityX (Unity 6) en lugar de velocity.x
    /// </summary>
    void Mover()
    {
        // linearVelocityX establece directamente la velocidad horizontal
        // Multiplicar por velocidadMovimiento hace que se mueva más rápido
        // No se modifica la velocidad vertical (Y) para que la gravedad actúe normalmente
        rb.linearVelocityX = movimientoHorizontal * velocidadMovimiento;
        
        // Explicación: Si movimientoHorizontal = 1 y velocidadMovimiento = 7
        // Entonces rb.linearVelocityX = 7 (se mueve a 7 unidades por segundo a la derecha)
    }
    
    /// <summary>
    /// Aplica una fuerza hacia arriba para que el personaje salte
    /// Solo debe llamarse cuando está en el suelo (verificado en Update)
    /// </summary>
    void Saltar()
    {
        // Primero: Resetear la velocidad vertical a cero
        // Esto evita que un salto anterior afecte al nuevo salto
        // Sin esto, si el personaje está cayendo, el salto sería más bajo
        rb.linearVelocityY = 0;
        
        // Segundo: Aplicar una fuerza instantánea hacia arriba (eje Y positivo)
        // ForceMode2D.Impulse aplica la fuerza de golpe (como un impulso)
        // El personaje sale disparado hacia arriba y luego la gravedad lo devuelve
        rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        
        // Mostrar mensaje en consola para depuración
        // Los estudiantes pueden ver en la Consola si el salto se ejecuta
        Debug.Log("¡Salto ejecutado! Fuerza aplicada: " + fuerzaSalto);
    }
    
    /// <summary>
    /// Gira el sprite del personaje según la dirección del movimiento
    /// Esto se logra invirtiendo la escala en X (valores negativos)
    /// </summary>
    void GirarSprite()
    {
        // Si se mueve hacia la derecha (valor positivo) Y no está mirando derecha
        if (movimientoHorizontal > 0 && !mirandoDerecha)
        {
            // Escala normal (1,1,1) - mira a la derecha
            transform.localScale = new Vector3(1, 1, 1);
            mirandoDerecha = true;
        }
        // Si se mueve hacia la izquierda (valor negativo) Y está mirando derecha
        else if (movimientoHorizontal < 0 && mirandoDerecha)
        {
            // Escala negativa en X (-1,1,1) - invierte el sprite (mira izquierda)
            transform.localScale = new Vector3(-1, 1, 1);
            mirandoDerecha = false;
        }
        
        // Nota: No se modifica la escala Y para que el personaje no se estire
    }
    
    /// <summary>
    /// OnDrawGizmosSelected se ejecuta en el editor (NO en el juego)
    /// Dibuja una esfera verde en la posición del punto suelo para ayudar a depurar
    /// Los estudiantes pueden ver visualmente el área de detección
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // Solo dibujar si el puntoSuelo existe (para evitar errores)
        if (puntoSuelo != null)
        {
            // Configurar el color de la línea/forma (verde)
            Gizmos.color = Color.green;
            
            // Dibujar un círculo alambre (wire sphere) en la posición del punto suelo
            // Esto ayuda a visualizar qué área detecta el suelo
            Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
        }
    }
}