using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private float _walkPointRange;
    [SerializeField] private float _sightRange;

    private NavMeshAgent _agent;
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    private Vector3 _walkTarget;
    private Vector3 _direction;
    private Vector3 _vec;
    private bool _walkTargetSet;
    private bool _playerInSightRange;

    public int Health { get; set; }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    void FixedUpdate()
    {
        _vec = Player.Instance.transform.position - transform.position;
        _direction = _vec / _vec.magnitude;

        if (Physics.Raycast(transform.position, _direction, out RaycastHit hit, _sightRange))
        {
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _layerPlayer);

            Debug.DrawRay(transform.position, _direction);
            if (_playerInSightRange && hit.transform.CompareTag("Player")) ChasePlayer();
        }

        if (!_playerInSightRange) Patroling();
    }

    private void Patroling()
    {
        SetColor(Color.green);

        if (!_walkTargetSet) SearchWalkPoint();
        else 
        { 
            _agent.SetDestination(_walkTarget);
        }

        Vector3 distanceToTarget = transform.position - _walkTarget;

        if (distanceToTarget.magnitude < 1f)
        {
            _walkTargetSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        _agent.SetDestination(_walkTarget);
    }

    private void ChasePlayer()
    {
        SetColor(Color.red);
        _agent.SetDestination(Player.Instance.transform.position);
    }

    public void ApplyDamage(int amount)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * 800f, ForceMode.Acceleration);
        Destroy(gameObject, .5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.ApplyDamage(20);
            ApplyDamage(0);
        }
    }

    private void SetColor(Color color)
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propBlock);
    }
}
