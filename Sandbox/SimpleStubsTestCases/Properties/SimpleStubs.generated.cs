using  System;
using  System.Runtime.CompilerServices;
using  Etg.SimpleStubs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes
{
    [CompilerGenerated]
    public class StubIGenericInterface<T> : IGenericInterface<T>
    {
        private readonly StubContainer<StubIGenericInterface<T>> _stubs = new StubContainer<StubIGenericInterface<T>>();

        public MockBehavior MockBehavior { get; set; }

        T global::UnitBoilerplate.Sandbox.Classes.IGenericInterface<T>.GetAThing()
        {
            GetAThing_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<GetAThing_Delegate>("GetAThing");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<GetAThing_Delegate>("GetAThing", out del))
                {
                    return default(T);
                }
            }

            return del.Invoke();
        }

        public delegate T GetAThing_Delegate();

        public StubIGenericInterface<T> GetAThing(GetAThing_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubIGenericInterface(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace UnitBoilerplate.Sandbox.Classes
{
    [CompilerGenerated]
    public class StubIInterface3 : IInterface3
    {
        private readonly StubContainer<StubIInterface3> _stubs = new StubContainer<StubIInterface3>();

        public MockBehavior MockBehavior { get; set; }

        public StubIInterface3(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace UnitBoilerplate.Sandbox.Classes
{
    [CompilerGenerated]
    public class StubIInterface4 : IInterface4
    {
        private readonly StubContainer<StubIInterface4> _stubs = new StubContainer<StubIInterface4>();

        public MockBehavior MockBehavior { get; set; }

        public StubIInterface4(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace UnitBoilerplate.Sandbox.Classes
{
    [CompilerGenerated]
    public class StubISomeInterface : ISomeInterface
    {
        private readonly StubContainer<StubISomeInterface> _stubs = new StubContainer<StubISomeInterface>();

        public MockBehavior MockBehavior { get; set; }

        void global::UnitBoilerplate.Sandbox.Classes.ISomeInterface.DoAThing()
        {
            DoAThing_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<DoAThing_Delegate>("DoAThing");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<DoAThing_Delegate>("DoAThing", out del))
                {
                    return;
                }
            }

            del.Invoke();
        }

        public delegate void DoAThing_Delegate();

        public StubISomeInterface DoAThing(DoAThing_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubISomeInterface(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace UnitBoilerplate.Sandbox.Classes
{
    [CompilerGenerated]
    public class StubISomeOtherInterface : ISomeOtherInterface
    {
        private readonly StubContainer<StubISomeOtherInterface> _stubs = new StubContainer<StubISomeOtherInterface>();

        public MockBehavior MockBehavior { get; set; }

        public StubISomeOtherInterface(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}