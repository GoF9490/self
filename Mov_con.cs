using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_con : MonoBehaviour
{
    public Vector3 _stTouchPos;
    public Vector3 _movTouchPos;
    public float vt = 0;
    public bool _touch = false;

    public Camera camUI;

    public GameObject player;
    public GameObject pointer;
    P_controller _pc;

    private void Start()
    {
        player = GameObject.Find("Player");
        _pc = player.GetComponent<P_controller>();
        camUI = GameObject.Find("CameraUI").GetComponent<Camera>();
    }

    private void Update()
    {
            if (Input.GetMouseButton(0) && _touch == true)
        {
            _stTouchPos = Camera.main.ScreenToWorldPoint(transform.position);
            _stTouchPos.z = 10;

            _movTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _movTouchPos.z = 10;
            //_movTouchPos = camUI.ScreenToWorldPoint(_movTouchPos);

            vt = Quaternion.FromToRotation(Vector3.left, _stTouchPos - _movTouchPos).eulerAngles.z;

            Vector3 _dir = _stTouchPos - _movTouchPos;
            //var _vec = _stTouchPos - _movTouchPos;
            float _drgdir = _dir.sqrMagnitude;

            if (_stTouchPos != _movTouchPos && _drgdir > 1)
            {
                if (0 < vt && vt < 45 || 315 <= vt && vt < 360)
                {
                    //Debug.Log("우");
                    _pc.MoveR();
                    _pc._portal = false;
                    _pc._fall = false;
                }
                else

                if (45 <= vt && vt < 135)
                {
                    _pc._portal = true;
                }
                else

                if (135 <= vt && vt < 225)
                {
                    //Debug.Log("좌");
                    _pc.MoveL();
                    _pc._portal = false;
                    _pc._fall = false;
                }
                else

                if (225 <= vt && vt < 315)
                {
                    _pc._fall = true;
                }

                if (vt == 0)
                {
                    if (_stTouchPos.x < _movTouchPos.x)
                    {
                        _pc.MoveR();
                        _pc._portal = false;
                        _pc._fall = false;
                    }
                    else

                    if (_stTouchPos.x > _movTouchPos.x)
                    {
                        _pc.MoveL();
                        _pc._portal = false;
                        _pc._fall = false;
                    }
                }
            }
            else

            {
                _pc._move = false;
                _pc._portal = false;
                _pc._fall = false;
            }

            if (true)
            {
                Vector2 pointer_pos = new Vector2(_movTouchPos.x - _stTouchPos.x, _movTouchPos.y - _stTouchPos.y);
                pointer_pos = Vector2.ClampMagnitude(pointer_pos, 2);
                pointer.transform.localPosition = pointer_pos * 12;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _touch = false;
            pointer.transform.localPosition = new Vector3(0, 0, 0);
            _pc._move = false;
            _pc._portal = false;
            _pc._fall = false;
        }

    }

    public void Touchdown()
    {
        if(_touch == false)
        {
            _stTouchPos = Camera.main.ScreenToWorldPoint(transform.position);
            _stTouchPos.z = 10;
            //_stTouchPos = camUI.ScreenToWorldPoint(_stTouchPos);
            _touch = true;
        }
    }
}
