using RayFire;
using UnityEngine;
using Ami.BroAudio;

public class Barrel : MonoBehaviour, IAttackable
{
    [SerializeField] private SoundID woodFallSoundID;
    [SerializeField] private SoundID hitSoundID;
    [SerializeField] private GameObject effect;
    private RayfireRigid _rayfire;

    private void Awake()
    {
        _rayfire = GetComponent<RayfireRigid>();
        _rayfire.simulationType = SimType.Dynamic;
        _rayfire.demolitionType = DemolitionType.Runtime;
        _rayfire.objectType = ObjectType.Mesh;
        _rayfire.Initialize();
    }
    public void ApplyAttack(float damage, Vector2 direction, Vector2 knockBack, Entity dealer)
    {
        BroAudio.Play(hitSoundID);
        Debug.Log("¾Æ¾ß!");
        Vector3 pos = new Vector3(transform.position.x, transform.position.y +0.5f, transform.position.z);
        _rayfire.Demolish();
        GameObject fx = Instantiate(effect, pos, Quaternion.identity);
        BroAudio.Play(woodFallSoundID);
    }
}
