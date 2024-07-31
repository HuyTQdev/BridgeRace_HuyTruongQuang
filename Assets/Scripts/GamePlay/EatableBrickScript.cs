using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class EatableBrickScript:MonoBehaviour
{
    [field: SerializeField]public DataColor DataColor { get; private set; }
    Collider ownerCollider;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] DataColor greyColor;
    Platform platform;
    [SerializeField] Rigidbody _rigidbody;
    Vector3 position;

    public void Generate(DataColor dataColor, Collider ownerCollider, Platform platform)
    {
        position = transform.position;
        this.DataColor = dataColor;
        this.ownerCollider = ownerCollider;
        this.platform = platform;
        mesh.enabled = true;
        mesh.material = dataColor.brickMat;
        boxCollider.enabled = true;
        _rigidbody.useGravity = false;
        EventManager.Instance.TriggerEvent("GENBLOCK" + dataColor.name, position);
    }

    public void Splash(Transform tf)
    {
        transform.position = tf.position;
        transform.rotation = tf.rotation;
        position = tf.position;
        DataColor = greyColor;
        mesh.enabled = true;
        mesh.material = greyColor.brickMat;
        boxCollider.enabled = true;
        boxCollider.isTrigger = false;
        _rigidbody.useGravity = true;
        EventManager.Instance.TriggerEvent("GENBLOCKGREY", position);
        _rigidbody.AddForce(UnityEngine.Random.Range(-1.5f, 1.5f) * Vector3.right
            + UnityEngine.Random.Range(-1.5f, 1.5f) * Vector3.up
            + UnityEngine.Random.Range(.5f, 1f) * Vector3.forward, ForceMode.Impulse);

    }

    public void Hide()
    {
        mesh.enabled = false;
        boxCollider.enabled = false;
        platform.BrickHide(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character") && DataColor == greyColor)
        {
            if (other.TryGetComponent<AddBlockScript>(out AddBlockScript addBlockScript))
            {
                addBlockScript.Add(null);
                EventManager.Instance.TriggerEvent("ADDBLOCKGREY", position);
                gameObject.SetActive(false);
            }

        }
        if (other == ownerCollider && !other.gameObject.GetComponent<Character>().IsFalling)
        {
            EventManager.Instance.TriggerEvent("ADDBLOCK" + DataColor.name, position);
            Hide();
        }
    }

}
