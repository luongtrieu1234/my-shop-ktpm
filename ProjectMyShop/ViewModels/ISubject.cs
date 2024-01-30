using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMyShop.ViewModels
{
    public interface ISubject
    {
        public void Attach(IObserver observer);

        // Detach an observer from the subject.
        public void Detach(IObserver observer);

        // Notify all observers about an event.
        public void Notify();
        public void SomeBusinessLogic();
    }
}
