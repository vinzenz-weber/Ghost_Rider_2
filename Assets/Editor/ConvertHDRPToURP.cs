using UnityEngine;
using UnityEditor;

public class ConvertHDRPToURP : MonoBehaviour
{
    [MenuItem("Tools/Convert HDRP to URP Materials")]
    public static void ConvertMaterials()
    {
        // Find all materials in the project
        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            // Check if the material uses an HDRP shader
            if (material.shader.name.Contains("HDRP"))
            {
                Debug.Log($"Converting Material: {material.name}");

                // Change shader to URP/Lit
                material.shader = Shader.Find("Universal Render Pipeline/Lit");

                // Reassign textures (example: Albedo and Normal)
                if (material.HasProperty("_BaseColorMap"))
                    material.SetTexture("_BaseMap", material.GetTexture("_BaseColorMap"));

                if (material.HasProperty("_NormalMap"))
                    material.SetTexture("_BumpMap", material.GetTexture("_NormalMap"));

                if (material.HasProperty("_MetallicRemapMin"))
                    material.SetFloat("_Metallic", material.GetFloat("_MetallicRemapMin"));

                // Save changes
                EditorUtility.SetDirty(material);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Material conversion complete.");
    }
}