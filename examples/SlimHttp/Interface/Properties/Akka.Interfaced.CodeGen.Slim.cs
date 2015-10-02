// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Akka.Interfaced CodeGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Akka.Interfaced;

#region SlimHttp.Interface.ICalculator

namespace SlimHttp.Interface
{
    public static class ICalculator_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,]
            {
                {typeof(Concat_Invoke), typeof(Concat_Return)},
                {typeof(Sum_Invoke), typeof(Sum_Return)},
            };
        }

        public class Concat_Invoke : IInterfacedPayload
        {
            public System.String a;
            public System.String b;

            public Type GetInterfaceType() { return typeof(ICalculator); }
        }

        public class Concat_Return : IInterfacedPayload, IValueGetable
        {
            public System.String v;

            public Type GetInterfaceType() { return typeof(ICalculator); }

            public object Value { get { return v; } }
        }

        public class Sum_Invoke : IInterfacedPayload
        {
            public System.Int32 a;
            public System.Int32 b;

            public Type GetInterfaceType() { return typeof(ICalculator); }
        }

        public class Sum_Return : IInterfacedPayload, IValueGetable
        {
            public System.Int32 v;

            public Type GetInterfaceType() { return typeof(ICalculator); }

            public object Value { get { return v; } }
        }
    }

    public interface ICalculator_NoReply
    {
        void Concat(System.String a, System.String b);
        void Sum(System.Int32 a, System.Int32 b);
    }

    public class CalculatorRef : InterfacedSlimActorRef, ICalculator, ICalculator_NoReply
    {
        public CalculatorRef(ISlimActorRef actor, ISlimRequestWaiter requestWaiter, TimeSpan? timeout)
            : base(actor, requestWaiter, timeout)
        {
        }

        public ICalculator_NoReply WithNoReply()
        {
            return this;
        }

        public CalculatorRef WithRequestWaiter(ISlimRequestWaiter requestWaiter)
        {
            return new CalculatorRef(Actor, requestWaiter, Timeout);
        }

        public CalculatorRef WithTimeout(TimeSpan? timeout)
        {
            return new CalculatorRef(Actor, RequestWaiter, timeout);
        }

        public Task<System.String> Concat(System.String a, System.String b)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICalculator_PayloadTable.Concat_Invoke { a = a, b = b }
            };
            return SendRequestAndReceive<System.String>(requestMessage);
        }

        public Task<System.Int32> Sum(System.Int32 a, System.Int32 b)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICalculator_PayloadTable.Sum_Invoke { a = a, b = b }
            };
            return SendRequestAndReceive<System.Int32>(requestMessage);
        }

        void ICalculator_NoReply.Concat(System.String a, System.String b)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICalculator_PayloadTable.Concat_Invoke { a = a, b = b }
            };
            SendRequest(requestMessage);
        }

        void ICalculator_NoReply.Sum(System.Int32 a, System.Int32 b)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICalculator_PayloadTable.Sum_Invoke { a = a, b = b }
            };
            SendRequest(requestMessage);
        }
    }
}

#endregion

#region SlimHttp.Interface.ICounter

namespace SlimHttp.Interface
{
    public static class ICounter_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,]
            {
                {typeof(IncCounter_Invoke), null},
                {typeof(GetCounter_Invoke), typeof(GetCounter_Return)},
            };
        }

        public class IncCounter_Invoke : IInterfacedPayload
        {
            public System.Int32 delta;

            public Type GetInterfaceType() { return typeof(ICounter); }
        }

        public class GetCounter_Invoke : IInterfacedPayload
        {
            public Type GetInterfaceType() { return typeof(ICounter); }
        }

        public class GetCounter_Return : IInterfacedPayload, IValueGetable
        {
            public System.Int32 v;

            public Type GetInterfaceType() { return typeof(ICounter); }

            public object Value { get { return v; } }
        }
    }

    public interface ICounter_NoReply
    {
        void IncCounter(System.Int32 delta);
        void GetCounter();
    }

    public class CounterRef : InterfacedSlimActorRef, ICounter, ICounter_NoReply
    {
        public CounterRef(ISlimActorRef actor, ISlimRequestWaiter requestWaiter, TimeSpan? timeout)
            : base(actor, requestWaiter, timeout)
        {
        }

        public ICounter_NoReply WithNoReply()
        {
            return this;
        }

        public CounterRef WithRequestWaiter(ISlimRequestWaiter requestWaiter)
        {
            return new CounterRef(Actor, requestWaiter, Timeout);
        }

        public CounterRef WithTimeout(TimeSpan? timeout)
        {
            return new CounterRef(Actor, RequestWaiter, timeout);
        }

        public Task IncCounter(System.Int32 delta)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICounter_PayloadTable.IncCounter_Invoke { delta = delta }
            };
            return SendRequestAndWait(requestMessage);
        }

        public Task<System.Int32> GetCounter()
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICounter_PayloadTable.GetCounter_Invoke {  }
            };
            return SendRequestAndReceive<System.Int32>(requestMessage);
        }

        void ICounter_NoReply.IncCounter(System.Int32 delta)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICounter_PayloadTable.IncCounter_Invoke { delta = delta }
            };
            SendRequest(requestMessage);
        }

        void ICounter_NoReply.GetCounter()
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new ICounter_PayloadTable.GetCounter_Invoke {  }
            };
            SendRequest(requestMessage);
        }
    }
}

#endregion

#region SlimHttp.Interface.IPedantic

namespace SlimHttp.Interface
{
    public static class IPedantic_PayloadTable
    {
        public static Type[,] GetPayloadTypes()
        {
            return new Type[,]
            {
                {typeof(TestCall_Invoke), null},
                {typeof(TestOptional_Invoke), typeof(TestOptional_Return)},
                {typeof(TestTuple_Invoke), typeof(TestTuple_Return)},
                {typeof(TestParams_Invoke), typeof(TestParams_Return)},
                {typeof(TestPassClass_Invoke), typeof(TestPassClass_Return)},
                {typeof(TestReturnClass_Invoke), typeof(TestReturnClass_Return)},
            };
        }

        public class TestCall_Invoke : IInterfacedPayload
        {
            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestOptional_Invoke : IInterfacedPayload
        {
            public System.Nullable<System.Int32> value;

            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestOptional_Return : IInterfacedPayload, IValueGetable
        {
            public System.Nullable<System.Int32> v;

            public Type GetInterfaceType() { return typeof(IPedantic); }

            public object Value { get { return v; } }
        }

        public class TestTuple_Invoke : IInterfacedPayload
        {
            public System.Tuple<System.Int32, System.String> value;

            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestTuple_Return : IInterfacedPayload, IValueGetable
        {
            public System.Tuple<System.Int32, System.String> v;

            public Type GetInterfaceType() { return typeof(IPedantic); }

            public object Value { get { return v; } }
        }

        public class TestParams_Invoke : IInterfacedPayload
        {
            public System.Int32[] values;

            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestParams_Return : IInterfacedPayload, IValueGetable
        {
            public System.Int32[] v;

            public Type GetInterfaceType() { return typeof(IPedantic); }

            public object Value { get { return v; } }
        }

        public class TestPassClass_Invoke : IInterfacedPayload
        {
            public SlimHttp.Interface.TestParam param;

            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestPassClass_Return : IInterfacedPayload, IValueGetable
        {
            public System.String v;

            public Type GetInterfaceType() { return typeof(IPedantic); }

            public object Value { get { return v; } }
        }

        public class TestReturnClass_Invoke : IInterfacedPayload
        {
            public System.Int32 value;
            public System.Int32 offset;

            public Type GetInterfaceType() { return typeof(IPedantic); }
        }

        public class TestReturnClass_Return : IInterfacedPayload, IValueGetable
        {
            public SlimHttp.Interface.TestResult v;

            public Type GetInterfaceType() { return typeof(IPedantic); }

            public object Value { get { return v; } }
        }
    }

    public interface IPedantic_NoReply
    {
        void TestCall();
        void TestOptional(System.Nullable<System.Int32> value);
        void TestTuple(System.Tuple<System.Int32, System.String> value);
        void TestParams(params System.Int32[] values);
        void TestPassClass(SlimHttp.Interface.TestParam param);
        void TestReturnClass(System.Int32 value, System.Int32 offset);
    }

    public class PedanticRef : InterfacedSlimActorRef, IPedantic, IPedantic_NoReply
    {
        public PedanticRef(ISlimActorRef actor, ISlimRequestWaiter requestWaiter, TimeSpan? timeout)
            : base(actor, requestWaiter, timeout)
        {
        }

        public IPedantic_NoReply WithNoReply()
        {
            return this;
        }

        public PedanticRef WithRequestWaiter(ISlimRequestWaiter requestWaiter)
        {
            return new PedanticRef(Actor, requestWaiter, Timeout);
        }

        public PedanticRef WithTimeout(TimeSpan? timeout)
        {
            return new PedanticRef(Actor, RequestWaiter, timeout);
        }

        public Task TestCall()
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestCall_Invoke {  }
            };
            return SendRequestAndWait(requestMessage);
        }

        public Task<System.Nullable<System.Int32>> TestOptional(System.Nullable<System.Int32> value)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestOptional_Invoke { value = (System.Nullable<System.Int32>)value }
            };
            return SendRequestAndReceive<System.Nullable<System.Int32>>(requestMessage);
        }

        public Task<System.Tuple<System.Int32, System.String>> TestTuple(System.Tuple<System.Int32, System.String> value)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestTuple_Invoke { value = (System.Tuple<System.Int32, System.String>)value }
            };
            return SendRequestAndReceive<System.Tuple<System.Int32, System.String>>(requestMessage);
        }

        public Task<System.Int32[]> TestParams(params System.Int32[] values)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestParams_Invoke { values = values }
            };
            return SendRequestAndReceive<System.Int32[]>(requestMessage);
        }

        public Task<System.String> TestPassClass(SlimHttp.Interface.TestParam param)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestPassClass_Invoke { param = param }
            };
            return SendRequestAndReceive<System.String>(requestMessage);
        }

        public Task<SlimHttp.Interface.TestResult> TestReturnClass(System.Int32 value, System.Int32 offset)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestReturnClass_Invoke { value = value, offset = offset }
            };
            return SendRequestAndReceive<SlimHttp.Interface.TestResult>(requestMessage);
        }

        void IPedantic_NoReply.TestCall()
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestCall_Invoke {  }
            };
            SendRequest(requestMessage);
        }

        void IPedantic_NoReply.TestOptional(System.Nullable<System.Int32> value)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestOptional_Invoke { value = (System.Nullable<System.Int32>)value }
            };
            SendRequest(requestMessage);
        }

        void IPedantic_NoReply.TestTuple(System.Tuple<System.Int32, System.String> value)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestTuple_Invoke { value = (System.Tuple<System.Int32, System.String>)value }
            };
            SendRequest(requestMessage);
        }

        void IPedantic_NoReply.TestParams(params System.Int32[] values)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestParams_Invoke { values = values }
            };
            SendRequest(requestMessage);
        }

        void IPedantic_NoReply.TestPassClass(SlimHttp.Interface.TestParam param)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestPassClass_Invoke { param = param }
            };
            SendRequest(requestMessage);
        }

        void IPedantic_NoReply.TestReturnClass(System.Int32 value, System.Int32 offset)
        {
            var requestMessage = new SlimRequestMessage
            {
                InvokePayload = new IPedantic_PayloadTable.TestReturnClass_Invoke { value = value, offset = offset }
            };
            SendRequest(requestMessage);
        }
    }
}

#endregion
