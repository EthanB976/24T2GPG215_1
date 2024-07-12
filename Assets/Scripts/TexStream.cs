using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using System.IO;

public class TexStream : MonoBehaviour
{
    [SerializeField] SpriteRenderer s;
    public string filename = "pSprite";
    public string filetype = ".png";
    private string filepath = Application.streamingAssetsPath;
    private string fullFilePath;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<SpriteRenderer>();
       fullFilePath = Path.Combine(filepath, filename + filetype);
        LoadTex();

    }

    void LoadTex()
    {
        if(File.Exists(fullFilePath))
        {
            byte[] imagebytes = File.ReadAllBytes(fullFilePath);

            Texture2D tex  = new Texture2D(2,2);
            tex.LoadImage(imagebytes);
            Rect r = new Rect(gameObject.transform.position.x,gameObject.transform.position.y,tex.width,tex.height);
           s.sprite = Sprite.Create(tex, r, Vector2.zero);
        }
        else
        {
            {
                Debug.Log("File doesn't exist at " + fullFilePath);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
