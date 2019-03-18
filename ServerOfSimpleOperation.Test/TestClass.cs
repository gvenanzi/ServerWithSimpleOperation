using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerOfSimpleOperation.Test
{
    public class TestClass
    {
        private FakeTransporter transport;
        GameServer server;

        [SetUp]
        public void SetupTests()
        {
            transport = new FakeTransporter();
            server = new GameServer(transport);
        }

        [Test]
        public void TestSum()
        {
            //sum command is 0
            byte sum = 0;
            float firstOperator = 3;
            float secondOperator = 4;
            //have to create a byte[] with these three variables
            byte[] data = new byte[9];
            data[0] = sum;
            byte[] firstOp = BitConverter.GetBytes(firstOperator);
            Buffer.BlockCopy(firstOp, 0, data, 1, 4);
            byte[] secondOp = BitConverter.GetBytes(secondOperator);
            Buffer.BlockCopy(secondOp, 0, data, 5, 4);

            transport.DataEnqueue(data);
            byte[] response = transport.Recv(256);

            byte[] rightResult = BitConverter.GetBytes(firstOperator + secondOperator);

            Assert.That(response, Is.EqualTo(rightResult));
        }

        [Test]
        public void TestSubstraction()
        {
            //substraction command is 1
            byte substraction = 1;
            float firstOperator = 3;
            float secondOperator = 4;

            //have to create a byte[] with these three variables
            byte[] data = new byte[9];
            data[0] = substraction;
            byte[] firstOp = BitConverter.GetBytes(firstOperator);
            Buffer.BlockCopy(firstOp, 0, data, 1, 4);
            byte[] secondOp = BitConverter.GetBytes(secondOperator);
            Buffer.BlockCopy(secondOp, 0, data, 5, 4);

            transport.DataEnqueue(data);
            byte[] response = transport.Recv(256);

            byte[] rightResult = BitConverter.GetBytes(firstOperator - secondOperator);

            Assert.That(response, Is.EqualTo(rightResult));
        }

        [Test]
        public void TestMultiplication()
        {
            //multiplication command is 2
            byte multiplication = 2;
            float firstOperator = 3;
            float secondOperator = 4;

            //have to create a byte[] with these three variables
            byte[] data = new byte[9];
            data[0] = multiplication;
            byte[] firstOp = BitConverter.GetBytes(firstOperator);
            Buffer.BlockCopy(firstOp, 0, data, 1, 4);
            byte[] secondOp = BitConverter.GetBytes(secondOperator);
            Buffer.BlockCopy(secondOp, 0, data, 5, 4);

            transport.DataEnqueue(data);
            byte[] response = transport.Recv(256);

            byte[] rightResult = BitConverter.GetBytes(firstOperator * secondOperator);

            Assert.That(response, Is.EqualTo(rightResult));
        }

        [Test]
        public void TestDivision()
        {
            //division command is 3
            byte division = 3;
            float firstOperator = 3;
            float secondOperator = 4;

            //have to create a byte[] with these three variables
            byte[] data = new byte[9];
            data[0] = division;
            byte[] firstOp = BitConverter.GetBytes(firstOperator);
            Buffer.BlockCopy(firstOp, 0, data, 1, 4);
            byte[] secondOp = BitConverter.GetBytes(secondOperator);
            Buffer.BlockCopy(secondOp, 0, data, 5, 4);

            transport.DataEnqueue(data);
            byte[] response = transport.Recv(256);

            byte[] rightResult = BitConverter.GetBytes(firstOperator / secondOperator);

            Assert.That(response, Is.EqualTo(rightResult));
        }

        [Test]
        public void TestDivisionByZeroException()
        {
            //division command is 3
            byte division = 3;
            float firstOperator = 5;
            float secondOperator = 0;

            //have to create a byte[] with these three variables
            byte[] data = new byte[9];
            data[0] = division;
            byte[] firstOp = BitConverter.GetBytes(firstOperator);
            Buffer.BlockCopy(firstOp, 0, data, 1, 4);
            byte[] secondOp = BitConverter.GetBytes(secondOperator);
            Buffer.BlockCopy(secondOp, 0, data, 5, 4);

            transport.DataEnqueue(data);

            Assert.That(() => transport.Recv(256), Throws.InstanceOf<DivideByZeroException>());
        }
    }
}
