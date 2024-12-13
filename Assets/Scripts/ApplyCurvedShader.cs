using UnityEngine;

public class ApplyCurvedShader : MonoBehaviour
{
    public Shader curvedWorldShader;

    [System.Obsolete]
    void Start()
    {
        if (curvedWorldShader == null)
        {
            Debug.LogError("Curved World Shader not assigned!");
            return;
        }

        // Find all renderers in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                mat.shader = curvedWorldShader;
            }
        }

        Debug.Log("Curved World Shader applied to all objects!");
    }
}