using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rotateVelocity;
    [SerializeField] private Transform playerGFX;
    internal RaycastHit hit;
    internal BaseCharacter character;
    public bool performAttack = true;
    internal BaseCharacter targetEnemy;
    internal Coroutine basicAttackCoroutine;
    private void Awake()
    {
        character = this.GetComponentInChildren<BaseCharacter>();
    }
    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10) && hit.collider.gameObject.GetComponent<BaseCharacter>().team != character.team)
            {
                targetEnemy = hit.collider.gameObject.GetComponent<BaseCharacter>();
                agent.stoppingDistance = character.mainWeapon != null ? character.mainWeapon.attackRange : 1f;
                agent.SetDestination(hit.collider.transform.position);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
            {
                targetEnemy = null;
                agent.stoppingDistance = 0;
                agent.SetDestination(hit.point);
            }
            Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - this.transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, 0.1f * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
        if (Vector3.Distance(this.transform.position, hit.point) <= agent.stoppingDistance)
        {
            if (performAttack == true && targetEnemy != null)
            {
                StartCoroutine(AttackInterval());
            }
        }
        if (targetEnemy == null && basicAttackCoroutine != null)
        {
            print("stopped Coroutine");
            StopCoroutine(basicAttackCoroutine);
            performAttack = true;
            basicAttackCoroutine = null;
        }
    }
    IEnumerator AttackInterval()
    {
        performAttack = false;
        yield return basicAttackCoroutine = StartCoroutine(character.BasicAttack(targetEnemy, performAttack));
        yield return performAttack = true;
    }
}

