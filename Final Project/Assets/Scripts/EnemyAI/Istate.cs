using UnityEngine;

public interface Istate
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
    GameObject GetHp();
    void SetUp(GameObject hp);
}