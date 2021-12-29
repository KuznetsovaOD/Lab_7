using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int timeLeft;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TimeServer timeServer = new TimeServer();
            ConcreteObserverA observerA = new ConcreteObserverA();
            timeServer.Attach(observerA);
            timeServer.timeState();
            StartTheQuiz();
            progressBar1.Minimum = 0; // по умолчанию
            progressBar1.Maximum = 20; //по умолчанию
            progressBar1.Value =0; //по умолчанию
            progressBar1.Step = 1; //по умолчанию
        }
        public void subject()
        {
            Form1 form1 = new Form1();
            while (true)
            {
                int a = 20;
                Task.Delay(1000);

                form1.label1.Text = a.ToString();
                a++;
                subject();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                label1.Text = timeLeft.ToString();
                progressBar1.PerformStep();
    }
            else
            {
                timer1.Stop();
                label1.Text = "Подождите";
                webBrowser1.Navigate("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            }
        }

        public void StartTheQuiz()
        {
            timeLeft = 20;
            timer1.Start();
        }
    }


    public interface ITimeServer
    {
        void Attach(IObserver observer);
        void Notify();
    }

    public interface IObserver
    {
        void Update(ITimeServer timeServer);
    }

public class TimeServer : ITimeServer
    {
        public int a { get; set; } = 20;

        public List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        async public void timeState()
        {
            while (true)
            {

                await Task.Delay(1000);

                a--;

                Notify();
            }
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    public class ConcreteObserverA : IObserver
    {
        Form1 form1 = new Form1();
        public void Update(ITimeServer timeServer)
        {
            form1.label1.Text = Convert.ToString((timeServer as TimeServer).a);
        }
    }
}
