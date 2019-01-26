using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour
{
    public Material Effect;
//    public Camera cam;

//    private void Start()
//    {
//        camera = GetComponent<Camera>();
//        UnityEngine.Experimental.U2D.PixelPerfectRendering.pixelSnapSpacing = (float) (
//            camera.orthographicSize * 2.0 / camera.pixelHeight);
//    }

    private void Start()
    {
//        Screen.SetResolution(384,224,FullScreenMode.ExclusiveFullScreen);
//        Display.main.SetRenderingResolution(384,224);
//        Display.main.
        
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, Effect);
    }
}