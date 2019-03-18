using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerOfSimpleOperation.Test
{
    class FakeTransporter:ITransportable
    {
        private Queue<byte[]> recvQueue;
        private Queue<byte[]> sendQueue;

        public FakeTransporter()
        {
            recvQueue = new Queue<byte[]>();
            sendQueue = new Queue<byte[]>();
        }

        public void DataEnqueue(byte[] data)
        {
            recvQueue.Enqueue(data);
        }

        public byte[] Recv(int bufferSize, ref EndPoint sender)
        {
            //devo fare il dequeue del dato 
            byte[] dequeueData = recvQueue.Dequeue();

            byte operation = dequeueData[0];
            float firstOperator = BitConverter.ToSingle(dequeueData, 1);
            float secondOperator = BitConverter.ToSingle(dequeueData, 5);

            return Operation(operation, firstOperator, secondOperator);
        }

        public byte[] Recv(int bufferSize)
        {
            //devo fare il dequeue del dato 
            byte[] dequeueData = recvQueue.Dequeue();

            byte operation = dequeueData[0];
            float firstOperator = BitConverter.ToSingle(dequeueData, 1);
            float secondOperator = BitConverter.ToSingle(dequeueData, 5);

            return Operation(operation, firstOperator, secondOperator);
        }

        public byte[] Operation(byte operation, float firstOperator, float secondOperator)
        {
            float result;
            switch (operation)
            {
                case 0:
                    //addizione
                    result = firstOperator + secondOperator;
                    break;
                case 1:
                    //sottrazione
                    result = firstOperator - secondOperator;
                    break;
                case 2:
                    //moltiplicazione
                    result = firstOperator * secondOperator;
                    break;
                case 3:
                    //divisione
                    if (secondOperator == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    result = firstOperator / secondOperator;
                    break;
                default:
                    result = 0;
                    return null;
            }
            byte[] resultant = BitConverter.GetBytes(result);
            return resultant;
        }

        public bool Send(byte[] packet, EndPoint endPoint)
        {
            return false;
        }
    }
}
