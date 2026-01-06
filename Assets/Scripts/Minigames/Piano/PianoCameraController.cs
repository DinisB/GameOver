using UnityEngine;
using Unity.Cinemachine;

public class PianoCameraController : MonoBehaviour
{
    // Referencias as cameras do piano e do jogador
    public CinemachineCamera pianoCam;
    public CinemachineCamera playerCam;

    // Foca a camera no piano usando a prioridade das cameras
    public void FocusOnPiano()
    {
        pianoCam.Priority = 20;
        playerCam.Priority = 10;
    }
    // Volta a camera para o jogador usando a prioridade das cameras
    public void ExitPiano()
    {
        pianoCam.Priority = 10;
        playerCam.Priority = 20;
    }
}