using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marb.Quemanager
{
    public class QueueManager
    {
        Queue functionsQueue;

        public bool IsEmpty
        {
            get
            {
                if (functionsQueue.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        public QueueManager()
        {
            functionsQueue = new Queue();
        }

        public bool Contains(Action action)
        {
            if (functionsQueue.Contains(action))
                return true;
            else
                return false;
        }
        public bool Contains(Func<object> function)
        {
            if (functionsQueue.Contains(function))
                return true;
            else
                return false;
        }

        public object Pop()
        {
            return functionsQueue.Dequeue(); 
        }

        public void Add(Action action)
        {
            functionsQueue.Enqueue(action);
        }

        public void Add(Func<object> function )
        {
            functionsQueue.Enqueue(function);
        }

    }

}
