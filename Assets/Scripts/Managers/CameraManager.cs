using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private Color gizmosColor;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            myCamera.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.player.transform;

            myCamera.SetActive(true);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            myCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
            myCamera.SetActive(false);            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(polygonCollider.bounds.center, polygonCollider.bounds.size);
    }
}
