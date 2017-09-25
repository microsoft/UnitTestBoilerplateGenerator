namespace UnitTestBoilerplate.Model
{
	/// <summary>
	/// How the boilerplate will create the object to be tested.
	/// </summary>
	public enum TestedObjectCreationStyle
	{
		/// <summary>
		/// A helper method is on the test class and is called by each test.
		/// </summary>
		HelperMethod,

		/// <summary>
		/// The object is created directly from code with help of the mock library.
		/// </summary>
		DirectCode,
	}
}
