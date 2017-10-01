namespace Assets.Scripts.Interfaces
{
    public interface ISwitchTriggered{

        void OnSwitchOn ();
        void OnSwitchOff ();

    }

    class SwitchTriggered : ISwitchTriggered
    {
        public void OnSwitchOn()
        {
            throw new System.NotImplementedException();
        }

        public void OnSwitchOff()
        {
            throw new System.NotImplementedException();
        }
    }
}
