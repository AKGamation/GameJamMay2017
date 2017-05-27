using UnityEngine;
using System.Collections;

public class TextureCreater : MonoBehaviour {
    [Range(2,1024)]
    public int resolution = 256;
    private Texture2D texture;
    public float frequency = 1F;
   [Range (1, 3)]
    public int dimentions = 3;
    public NoiseMethodType type;
    public Gradient colouring;
    public GameObject self;
    private void OnEnable()
    {
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "procedural texture";
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Trilinear;
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        FillTexture();
    
    }
    
    public void FillTexture ()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }
        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint (new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));
        NoiseMethod method = noise.noiseMethods[(int)type][dimentions - 1];
        float stepSize = 1f / resolution;
                for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x= 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                float sample = noise.Sum (method, point, frequency);
                if (type != NoiseMethodType.value)
                {
                    sample = sample * 0.5f + 0.5f;
                }
                texture.SetPixel(x, y, colouring.Evaluate(sample));
            }
        }
        texture.Apply();
    }
    private void Update ()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            if(Time.frameCount % 10 == 0)
            {
                FillTexture();
            }
        }
    }
}
