using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickAndKeyBoard : MonoBehaviour
{
    public enum DotType
    {
        Click,
        ClickAndKey,
        Key,
        DoubleClick
    }

    
    public KeyCode MyKeyCode;
    public DotType Type;

    private TextMeshProUGUI _tmpComponent;
    private bool _isKeyDown = false;
    private int _times = 0;

    private void OnMouseDown()
    {
        Debug.Log(Type);
        switch (Type)
        {
            case DotType.Click:
                //ebug.Log(Type);
                SuccessGot();
                break;
            case DotType.ClickAndKey:
                if (_isKeyDown)
                {
                    SuccessGot();
                }

                break;
            case DotType.DoubleClick:
                _times++;
                //变色
                this.transform.localScale = new Vector3(0.8F,0.8f,0.8f);
                
                if (_times == 2)
                {
                    SuccessGot();
                }

                break;
        }
    }

    private void OnMouseOver()
    {
        if (this.Type == DotType.Key)
        {
            if (Input.GetKeyDown(MyKeyCode))
            {
                SuccessGot();
            }
        }
    }

    private void OnMouseExit()
    {
        
    }

    private void SuccessGot()
    {
        //加分
        //消失
        Destroy(this.gameObject);
    }

    private void FailedGot()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        _tmpComponent = GetComponentInChildren<TextMeshProUGUI>();
        
        if (Type == DotType.Key || Type == DotType.ClickAndKey)
        {
            _tmpComponent.text = MyKeyCode.ToString();            
        }
        else
        {
            _tmpComponent.text = "";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(MyKeyCode))
        {
            _isKeyDown = true;
        }
        
        if (Input.GetKeyUp(MyKeyCode))
        {
            _isKeyDown = false;
        }
    }
}