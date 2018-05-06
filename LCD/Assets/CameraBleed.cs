using UnityEngine;
using System.Collections;

public class CameraBleed : MonoBehaviour
{
    public Material mat;
    public RenderTexture back;
    public void Start()
    {

    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (back == null)
        {
            back = new RenderTexture(dest);
        }
        mat.SetTexture("_New", src);
        mat.SetTexture("_Old", back);
        Graphics.Blit(src, dest, mat);
        //back = new RenderTexture(dest);
        Graphics.Blit(dest, back); //save last frame
    }
}