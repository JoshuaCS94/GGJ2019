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

//    private void Start()
//    {
//        Screen.SetResolution(384,224,FullScreenMode.ExclusiveFullScreen);
//        Display.main.SetRenderingResolution(384,224);
//        Display.main.
        
//    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, Effect);
    }








//    private RenderTexture _rt;
    
//    void OnPreRender()
//    {
//        // before rendering, setup our RenderTexture
//        int width = 384;
//        int height = 224;
//        _rt = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);           
//        GetComponent<Camera>().targetTexture = _rt;
//    }
//
//    void OnPostRender()
//    {
//        // after rendering we need to clear the targetTexture so that post effect will be able to render 
//        // to the screen
//        GetComponent<Camera>().targetTexture = null;
//        RenderTexture.active = null;
//    }
//    
//    void OnRenderImage(RenderTexture src, RenderTexture dest)
//    {
//        // set our filtering mode and blit to the screen
//        src.filterMode = FilterMode.Point;
//        
//        Graphics.Blit(src, dest, Effect);
//        
//        RenderTexture.ReleaseTemporary(_rt);
//    }
    
    
    
}