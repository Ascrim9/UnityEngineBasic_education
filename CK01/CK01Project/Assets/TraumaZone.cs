using UnityEngine;

public class TraumaZone : MonoBehaviour
{
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.GetComponent<Enemy>() != null)
        {
            if (PlayerManager.Instance.playerStats.curTrauma >= PlayerManager.Instance.playerStats.maxTrauma)
                PlayerManager.Instance.player.stateMachine.ChangeState(null, PlayerManager.Instance.player.deathState,null);
            else
            {
                PlayerManager.Instance.playerStats.curTrauma += collision.GetComponent<EnemyStats>().trauma;

            }
            UI_InGame.Instance.UpdateTrauma(PlayerManager.Instance.playerStats.curTrauma, PlayerManager.Instance.playerStats.maxTrauma);
        }
    }
}