using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectMyShop.ViewModels
{
    internal class CategoryViewModel : INotifyPropertyChanged, ISubject
    {
        public BindingList<Category> Categories { get; set; } = new BindingList<Category>();
        public List<Category> SelectedCategories { get; set; } = new List<Category>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public int State { get; set; } = -0;

        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        public List<IObserver> _observers = new List<IObserver>();

        // Attach an observer to the subject.
        public void Attach(IObserver observer)
        {
            Debug.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        // Detach an observer from the subject.
        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Debug.WriteLine("Subject: Detached an observer.");
        }

        // Notify all observers about an event.
        public void Notify()
        {
            Debug.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
        public void SomeBusinessLogic(int type)
        {
            Debug.WriteLine("\nSubject: I'm doing something important.");
            this.State = type;

            Thread.Sleep(15);

            Debug.WriteLine("Subject: A Cate " + this.State);
            this.Notify();
        }
    }
}