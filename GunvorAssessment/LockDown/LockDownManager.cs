using System;

namespace GunvorAssessment.LockDown
{
    public interface ILockDownManager
    {
        event EventHandler LockDownStarted;

        event EventHandler LockDownEnded;

        void StartLockDown();

        void EndLockDown();

    }

    public class LockDownManager : ILockDownManager
    {
        public event EventHandler LockDownStarted;
        public event EventHandler LockDownEnded;
        public bool isLockDown = false;

        public void EndLockDown()
        {
            OnLockDownEnded(EventArgs.Empty);
        }

        public void StartLockDown()
        {
            OnLockDownStarted(EventArgs.Empty);
        }

        private void OnLockDownStarted(EventArgs empty)
        {
            if (!isLockDown)
            {
                isLockDown = true;
                LockDownStarted?.Invoke(this, empty);
            }
        }

        private void OnLockDownEnded(EventArgs empty)
        {
            if (isLockDown)
            {
                isLockDown = false;
                LockDownEnded?.Invoke(this, empty);
            }
        }
    }
}
