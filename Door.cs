using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	void Start()
    {

    }

    void Update()
    {

    }
	
    void OnTriggerEnter2D (Collider2D other)
    {
        // Somente com 70 pontos coletados (dois itens do cenario) o personagem poderá passar a fase
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameController.instance.totalScore >= 100)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
    }
}