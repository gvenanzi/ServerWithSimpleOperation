using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerOfSimpleOperation
{
    public class Transporter:ITransportable
    {
        private Socket socket;

        public Transporter()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
        }

        public void Bind(string address, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            socket.Bind(endPoint);
        }

        public bool Send(byte[] data, EndPoint endPoint)
        {
            bool success = false;
            try
            {
                int rlen = socket.SendTo(data, endPoint);
                if (rlen == data.Length)
                    success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public byte[] Recv(int bufferSize, ref EndPoint sender)
        {
            int rlen = -1;
            byte[] data = new byte[bufferSize];
            try
            {
                rlen = socket.ReceiveFrom(data, ref sender);
                if (rlen <= 0)
                    return null;
            }
            catch
            {
                return null;
            }

            //unpack the operator command, first operator, second operator
            byte operation = data[0];
            float firstOperator = BitConverter.ToSingle(data, 1);
            float secondOperator = BitConverter.ToSingle(data, 5);

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

        public EndPoint CreateEndPoint()
        {
            return new IPEndPoint(0, 0);
        }
    }
}
