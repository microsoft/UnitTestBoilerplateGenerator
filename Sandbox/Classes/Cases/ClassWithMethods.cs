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
	}
}
