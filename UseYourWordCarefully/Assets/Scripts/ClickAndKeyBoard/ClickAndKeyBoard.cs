using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

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

    private Image _image;
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
                //碎裂!
                _image.color = Color.grey;
                //this.transform.localScale = new Vector3(0.8F, 0.8f, 0.8f);

                if (_times == 2)
                {
                    SuccessGot();
                }

                break;
        }
    }

    private void OnMouseOver()
    {
        this.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        
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
        this.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
    }

    private void SuccessGot()
    {
        //加分
        //消失
        var RectT = this.GetComponent<RectTransform>();
        ParticleEffectGenerator.Instance.OnClickKeyEffect(RectT.localPosition);
       // Debug.Log(RectT.position);
        CameraShakeManager.Instance.Play("CameraShakes/SlightShake");
        Destroy(this.gameObject);
    }

    private void FailedGot()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _image = GetComponent<Image>();
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
    
    public void Initialize(DotType type, KeyCode keyCode)
    {
        _image = GetComponent<Image>();
        _tmpComponent = GetComponentInChildren<TextMeshProUGUI>();
        this.Type = type;
        this.MyKeyCode = keyCode;
        
        if (Type == DotType.Key || Type == DotType.ClickAndKey)
        {
            _tmpComponent.text = MyKeyCode.ToString();
        }
        else
        {
            _tmpComponent.text = "";
        }
    }

    public void HeartBeatEffect()
    {
        ParticleEffectGenerator.Instance.OnSequenceClickEffect(this.transform.localPosition);
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