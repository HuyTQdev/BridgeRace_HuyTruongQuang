using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Character: MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected CapsuleCollider capsuleCollider;

    [field: SerializeField] public AddBlockScript addBlockScript { get; private set; }
    public DataColor DataColor { get; private set; }
    [SerializeField] float force;
    public bool IsFalling { get; private set; }
    protected bool isEndGame;
    public bool IsOnGround { get; set; }
    public Platform platform { get; private set; }
    private Platform oldPlatform;

    public static Collider GetColliderByDataColor(string nameColor)
    {
        foreach (Character character in FindObjectsOfType<Character>())
        {
            if (character.DataColor.name == nameColor) return character.capsuleCollider;
        }
        return null;
    }
    public virtual void Generate(DataColor dataColor, Platform platform)
    {
        isEndGame = false;
        this.platform = platform;
        this.DataColor = dataColor;
        IsFalling = false;
        IsOnGround = true;
        addBlockScript.Generate(dataColor);
        mesh.material = dataColor.charMat;
        EventManager.Instance.StartListening("EndGame", EndGame);
        platform.Register(this, capsuleCollider);
    }

    private void OnDisable()
    {
        if (!EventManager.CheckNull()) EventManager.Instance.StopListening("EndGame", EndGame);
    }

    private void LateUpdate()
    {
        if (IsFalling || isEndGame) StopMoving();
    }

    public virtual void Pass()
    {
        _rigidbody.AddForce(0, 2, 3, ForceMode.Impulse);
    }

    public virtual void StopMoving()
    {

    }
    public virtual void Wake()
    {
        IsFalling = false;
        capsuleCollider.enabled = true;
    }

    public virtual void ChangePlatform(Platform platform)
    {
        if (this.platform == null) return;
        oldPlatform = this.platform;
        this.platform = platform;
        oldPlatform.UnRegister(this);
        platform.Register(this, capsuleCollider);
        Pass();
    }

    public void Splash(Vector3 direction)
    {
        IsFalling = true;
        capsuleCollider.enabled = false;
        StopMoving();
        _rigidbody.AddForce((direction.normalized + Vector3.up) * force, ForceMode.Impulse);
      //  _rigidbody.isKinematic = true;
        addBlockScript.BrickSplash();
        StartCoroutine(WaitAnim());
    }
    IEnumerator WaitAnim()
    {
        _animator.SetBool("isFall", true);
        yield return new WaitForSeconds(2f);
        _animator.SetBool("isFall", false);
        yield return new WaitForSeconds(2f);
        Wake();
    }

    protected virtual void EndGame(object[] parameters)
    {
        isEndGame = true;
        if (parameters.Length > 0 && parameters[0] is Character)
        {
            addBlockScript.EndGame();
            StopMoving();
            if ((Character)parameters[0] == this)
            {
                _animator.SetInteger("Result", 2);
            }
            else
            {
                _animator.SetInteger("Result", 1);
            }
        }
    }

}
