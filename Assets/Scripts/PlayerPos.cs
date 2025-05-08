using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            Vector3 playerPos = transform.position;
            playerPos.z = 0; // Garante que o Z esteja correto
            player.position = playerPos;
        }
        else
        {
            Debug.LogWarning("Player com tag 'Player' n�o foi encontrado na cena.");
        }
    }
}