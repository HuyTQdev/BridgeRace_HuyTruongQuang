using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;

    public override void Generate(DataColor dataColor, Platform platform)
    {
        base.Generate(dataColor, platform);
        _joystick = UI.UIManager.Instance.GetUI<UI.CanvasGamePlay>().GetJoystick();
    }
    private void FixedUpdate()
    {
        if (_joystick == null || isEndGame) return;
        _rigidbody.velocity = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized * _moveSpeed + Vector3.up * Mathf.Min(1, Mathf.Max(-1, _rigidbody.velocity.y));
        if (_joystick.Direction.sqrMagnitude <= 0.1f || IsFalling) StopMoving();
        if (_rigidbody.velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetFloat("velocity", 1);
        }
        else
        {
            _animator.SetFloat("velocity", 0);
        }
    }

    public override void StopMoving()
    {
        base.StopMoving();
        _rigidbody.velocity = Vector3.zero;
    }

    public override void ChangePlatform(Platform platform)
    {
        base.ChangePlatform(platform);
        platform.OpenBlocked();
    }

    protected override void EndGame(object[] parameters)
    {
        base.EndGame(parameters);
        if (parameters.Length > 0 && parameters[0] is Character)
        {
            UI.UIManager.Instance.CloseAll();
            UI.UIManager.Instance.OpenUI<UI.CanvasEndGame>();
            UI.UIManager.Instance.GetUI<UI.CanvasEndGame>().SetTitle((Character)parameters[0] == this);
        }
    }
}
