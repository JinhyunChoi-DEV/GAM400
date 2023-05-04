using UnityEngine;

public interface IState
{
    void Initialize();
    void Update();
    void Clear();
}