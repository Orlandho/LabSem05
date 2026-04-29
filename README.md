# Laboratorio Semana 05 - Desarrollo de Videojuegos 🎮

Este repositorio contiene el proyecto desarrollado durante la Semana 05 del curso de Desarrollo de Videojuegos en la Universidad Privada del Norte UPN. 

## 🎯 Objetivo de la Sesión
Aplicar los conceptos fundamentales de Unity en un entorno 2D integrando escenas, objetos o GameObjects, físicas y scripts en C# para lograr el avance del videojuego propuesto.

## 📚 Temas Aplicados de la Clase
De acuerdo con los temas teóricos y prácticos de la sesión:
* **Definición de Escenas y Objetos:** Uso de GameObjects para construir los niveles.
* **Física 2D:** Implementación de motores de física con componentes como Rigidbody2D y colliders.
* **Programación de Scripts:** Control de mecánicas de juego y eventos mediante C#.
* **Flujo del Juego:** Transiciones y manejo de estados a través de múltiples escenas.

## 🗂️ Estructura del Proyecto

El proyecto está construido en Unity utilizando el pipeline URP 2D y se divide en tres escenas principales:
* `MenuPrincipal`: Pantalla de inicio y punto de entrada del juego.
* `EscenaJuego`: Nivel principal donde interactúa el jugador con el entorno.
* `GameOver`: Pantalla final que se muestra al perder la partida.

### Scripts Principales
* `ControladorPersonaje2D.cs`: Maneja el movimiento, los inputs y las físicas del avatar del jugador.
* `DetectorCaidaPersonaje.cs`: Evalúa la posición del personaje para detectar caídas y activar el fin del juego.
* `MenuPrincipal.cs` y `GameOver.cs`: Controlan la interfaz de usuario UI y la carga entre las diferentes escenas.

## 🛠️ Tecnologías Utilizadas
* **Motor:** Unity 2D
* **Lenguaje:** C#
* **Control de Versiones:** Git y GitHub

## 👨‍💻 Autor
**Orlando Jesús Dorival Castañeda** *Estudiante de Ingeniería de Sistemas Computacionales*
