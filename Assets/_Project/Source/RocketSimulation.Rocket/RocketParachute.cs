using UnityEngine;

namespace RocketSimulation.Rocket
{
    public class RocketParachute : MonoBehaviour, IParachute
    {
        private RocketBase _rocketOwner;
        private RocketData _rocketData;
        private Transform _parachuteObjectRef;

        private float _currentDrag;
        private bool _wasOpened;

        public bool WasOpened => _wasOpened;

        public void Initialize(RocketBase rocketOwner, RocketData rocketData, Transform parachuteObjectRef)
        {
            _rocketOwner = rocketOwner;
            _rocketData = rocketData;
            _parachuteObjectRef = parachuteObjectRef;
        }

        public void Open()
        {
            _parachuteObjectRef.gameObject.SetActive(true);
            _wasOpened = true;
            Smooth();
        }

        private void Smooth()
        {
            _currentDrag = _rocketOwner.Rigidbody.drag;
            _rocketOwner.Rigidbody.drag = _rocketData.Smooth;
        }

        public void Close()
        {
            _rocketOwner.Rigidbody.drag = _currentDrag;
            _parachuteObjectRef.gameObject.SetActive(false);
        }
    }
}