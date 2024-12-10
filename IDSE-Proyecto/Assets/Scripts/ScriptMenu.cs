using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{

    public void EmpezarNivel(string nombreNivel) {
        SceneManager.LoadScene(nombreNivel);
    }
    public void Salir() {
        Application.Quit();
    }
}
