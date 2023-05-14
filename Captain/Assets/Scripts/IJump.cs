using UnityEngine;

namespace Captain.Command
{
    public interface IJump
    {
        void Execute(GameObject gameObject);
        void countingTime();
    }
}
