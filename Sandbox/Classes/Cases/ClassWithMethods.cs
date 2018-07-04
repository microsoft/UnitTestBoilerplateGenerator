using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class ClassWithMethods
    {
        private readonly ISomeInterface someInterface;
        private readonly ISomeOtherInterface someOtherInterface;

        public ClassWithMethods(ISomeInterface someInterface, ISomeOtherInterface someOtherInterface)
        {
            this.someInterface = someInterface;
            this.someOtherInterface = someOtherInterface;
        }

		public async Task<bool> GetBoolTaskAsync(IInterface3 interface3, DateTime time)
		{
			return await Task.FromResult(true);
		}

		public Task<bool> GetBoolTaskNoAsync(IInterface3 interface3, DateTime time)
		{
			return Task.FromResult(true);
		}

		public Task GetTaskNoAsync(IInterface3 interface3, DateTime time)
		{
			return Task.Delay(1);
		}

		public string GetString()
		{
			return string.Empty;
		}

		public int GetIntMultipleSignatures(string bla)
		{
			return 0;
		}

		public int GetIntMultipleSignatures(IInterface4 interface4)
		{
			return 0;
		}

		private void PrivateMethod(ISomeOtherInterface someOtherInterface)
		{
		}

		public string GetOut(bool fufu, out int bubu)
		{
			bubu = 0;

			return string.Empty;
		}

		public void DoRef(ref ClassWithMethods refArg)
		{
		}

		public void DoEnum(Cucu cucuENum)
		{
		}

		public Task<bool> GetParams(params string[] values)
		{
			return Task.FromResult(true);
		}

		public Task<bool> GetParams2D(params DateTime[][] values)
		{
			return Task.FromResult(true);
		}

		public Task<bool> GetParamsClass(params ClassWithMethods[] values)
		{
			return Task.FromResult(true);
		}

		public Task<bool> GetParamsClass2D(params ClassWithMethods[][] values)
		{
			return Task.FromResult(true);
		}

		public Task<bool> GetWithClass4D(ClassWithMethods[][][][] values)
		{
			return Task.FromResult(true);
		}

		public string MethodWithNullableArgument(int? argument)
		{
			return string.Empty;
		}

		public string MethodWithNamespaceQualifiedArgument(Classes.IInterface3 myInterface)
		{
			return string.Empty;
		}
	}

	public enum Cucu
	{
		ZUUA,

		UJSAXK
	}
}
