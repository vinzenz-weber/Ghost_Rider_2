using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingSwitcher : MonoBehaviour
{
    public VolumeProfile mainMenuProfile; // Das Volume Profile für das Hauptmenü
    public VolumeProfile gameplayProfile; // Das Volume Profile für das Spiel

    private UniversalRenderPipelineAsset urpAsset;

    private void Start()
    {
        // Hole das aktuelle URP Render Pipeline Asset
        urpAsset = GraphicsSettings.defaultRenderPipeline as UniversalRenderPipelineAsset;

        if (urpAsset == null)
        {
            Debug.LogError("Kein URP Asset gefunden. Stelle sicher, dass URP aktiviert ist.");
        }
    }

    public void SwitchToMainMenuProfile()
    {
        if (urpAsset != null && mainMenuProfile != null)
        {
            urpAsset.volumeProfile = mainMenuProfile;
        }
    }

    public void SwitchToGameplayProfile()
    {
        if (urpAsset != null && gameplayProfile != null)
        {
            urpAsset.volumeProfile = gameplayProfile;
        }
    }
}