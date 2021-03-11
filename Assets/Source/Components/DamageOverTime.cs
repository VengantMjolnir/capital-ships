using UnityEngine;

public class DamageOverTime : MonoBehaviour, IHoldsTarget
{
    public int damage;
    public float interval;
    public GameObject hitEffect;

    private GameObject _hitObject;
    private Health _target;
    private float _timer = 0;

    private void Start()
    {
        _timer = interval;
    }

    public void SetTarget(GameObject target)
    {
        _target = target.GetComponent<Health>();
    }

    void Update()
    {
        if (_target != null)
        {
            _timer = _timer + Time.deltaTime;
            if (_timer > interval)
            {
                _timer = 0;
                _target.SufferDamage(damage);

                if (hitEffect != null && _hitObject == null)
                {
                    Target target = _target.GetComponent<Target>();
                    Vector3 hitPosition = _target.transform.position;
                    if (target != null)
                    {
                        hitPosition = target.GetTargetPoint(gameObject.GetInstanceID());
                    }
                    _hitObject = Instantiate<GameObject>(
                        hitEffect,
                        hitPosition,
                        Quaternion.identity
                    );
                    _hitObject.transform.SetParent(_target.transform, true);
                }
            }
        }
        else if (_hitObject != null)
        {
            Destroy(_hitObject);
        }
    }

    private void OnDestroy()
    {
        if (_hitObject)
        {
            Destroy(_hitObject);
        }
    }
}