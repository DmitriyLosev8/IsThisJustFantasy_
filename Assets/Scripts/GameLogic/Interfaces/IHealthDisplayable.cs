using System;

namespace Assets.Scripts.GameLogic.Interfaces
{
    internal interface IHealthDisplayable
    {
        public event Action<float> HealthValueChanged;
    }
}