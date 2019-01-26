using UnityEngine;

[ExecuteInEditMode]
public class RenderColorMask : MonoBehaviour
{
    public Shader ColorMaskShader;
    public string TagId = "Tag";
    public string TextureName = "_TexNameString";
    private Camera cam;

    // Use this for initialization
    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.SetReplacementShader(ColorMaskShader,TagId);
        RenderTexture tex = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 16);
        tex.filterMode = FilterMode.Point;
        cam.targetTexture = tex;
        Shader.SetGlobalTexture(TextureName, tex);
    }
}