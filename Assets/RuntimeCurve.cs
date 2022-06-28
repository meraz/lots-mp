using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RuntimeCurve : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CurveEntity curveEntity;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "SpriteRenderer cannot be null.");

        curveEntity = GetComponent<CurveEntity>();
        Assert.IsNotNull(curveEntity, "CurveEntity cannot be null.");
        Setup();
    }

    void Setup()
    {
        Texture2D texture = new Texture2D(100, 100);
        
        for(int y = 0; y < 100; ++y)
        {
            for(int x = 0; x < 100; ++x)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }

        for(int x = 0; x < 100; ++x)
        {
            int xVal = x;
            float yVal = curveEntity.curve.Evaluate(x);
            Debug.Log(yVal);
            texture.SetPixel(xVal, (int)Mathf.Floor(yVal), Color.red);
        }

        Sprite sprite = Sprite.Create(texture, new Rect(0,0,100,100), Vector2.zero);
        spriteRenderer.sprite = sprite;
    }
  
}
