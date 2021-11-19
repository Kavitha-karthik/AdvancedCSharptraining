using System;
using System.Collections.Generic;
using System.Threading;

namespace ObserverPattern_Assignment3
{
    public enum OrderState
    {
        CRETAED,CONFIRMED,CANCELLED,CLOSED
    }
    
    public   class Order
    {
        public event Action<string> OrderStateChanged;//event
       
         public event Action<string> OrderClosed;//event
        string orderId;
        OrderState currentState;
        public Order()
        {
            orderId = Guid.NewGuid().ToString();
            currentState = OrderState.CRETAED;
        }
        public void ChangeState(OrderState newState)
        {
            this.currentState = newState;
            NotifyAll();
            if (currentState == OrderState.CLOSED)
            {
                NotifyClosed();

            }

        }
        void NotifyAll()
        {
            if (OrderStateChanged != null)
            {
                       

                //this.OrderStateChanged.Invoke(this.orderId);//one->Many (Multicast Delegate Instance)
                //this.OrderStateChanged.BeginInvoke(orderId, null, null);
                Delegate[] invocationArray = this.OrderStateChanged.GetInvocationList();
                foreach (Action<string> method in invocationArray)
                {
                    new Thread(new ParameterizedThreadStart((object obj) => { method.Invoke(obj.ToString()); })).Start(this.orderId);
                }
            }
        }





        void NotifyClosed()
        {
            if (OrderClosed != null)
            {
                OrderClosed.Invoke(orderId);
            }
        }
        ////Subscribe,Register
        //public void Add_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged += observerAddress;//System.Delegate.Combine
        //}
        ////UnSubScribe
        //public void Remove_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged -= observerAddress;//System.Delegate.Remove
        //}

    }

  
    public class AuditSystem
    {
         public void OrderClosed(string closedData) { Console.WriteLine($"Order Closed  {closedData}"); }
    }

   
    public class EmailNotifificationSystem
    {
      //  public void Sendemail(object data) { Console.WriteLine($"Email Sent  {data}"); }
        public void SendMail(string evtData) { Console.WriteLine($"Email Sent  {evtData}"); }
    }
    public class SMSNotificationSystem
    {
        public void SendSms(object data) { Console.WriteLine($"SMS Sent  {data}"); }
        public void SendSMS(string evtData) {
            Console.WriteLine($"SMS Sent  {evtData}");
        }
    }

    

    public class WhatsappNotificationSystem
    {
        public void Whatapp(object data) { Console.WriteLine($"Whatapp Sent  {data}"); }
        public void WhatsappSMS(string evtData)
        {
            Console.WriteLine($"whatsappmsg Sent  {evtData}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmailNotifificationSystem _emailSystem = new EmailNotifificationSystem();
            SMSNotificationSystem _smsSystem = new SMSNotificationSystem();
            WhatsappNotificationSystem _whatsappsys = new WhatsappNotificationSystem();
            AuditSystem _auditSystem = new AuditSystem();

           Action<string> _emailObserver = new Action<string>(_emailSystem.SendMail);

            Action<string> _smsObserver = new Action<string>(_smsSystem.SendSMS);
            Action<string> _orderClosedobserver = new Action<string>(_auditSystem.OrderClosed);

           Action<string> _whatsappObserver = new Action<string>(_whatsappsys.WhatsappSMS);
          
            Order _order1 = new Order(); 
           
            _order1.OrderClosed += _orderClosedobserver;
            _order1.OrderStateChanged += _emailObserver;// Add_OrderStateChanged(_emailObserver)
            _order1.OrderStateChanged += _smsObserver;
            _order1.OrderStateChanged += _whatsappObserver;
            _order1.OrderClosed += _orderClosedobserver;

            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(1000).Wait();
            _order1.ChangeState(OrderState.CANCELLED);
            System.Threading.Tasks.Task.Delay(3000).Wait();
            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(5000).Wait();
            _order1.ChangeState(OrderState.CLOSED);
            System.Threading.Tasks.Task.Delay(1000).Wait();
        }

        static void SendEmailWrapper(object arg)
        {
            SendEmail();
        }
        static void SendEmail()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Sending Email...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
