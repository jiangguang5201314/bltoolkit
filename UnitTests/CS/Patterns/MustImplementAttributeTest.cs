using System;
using BLToolkit.TypeBuilder;
using NUnit.Framework;

using BLToolkit.Patterns;

namespace Patterns
{
	[TestFixture]
	public class MustImplementAttributeTest
	{
		public MustImplementAttributeTest()
		{
			TypeFactory.SaveTypes = true;
		}

		[MustImplement]
		public interface IRequiredInterface
		{
			int RequiredMethod();
			[MustImplement(false)]
			int SameMethodName();
			[MustImplement(false)]
			int OptionalMethod();
		}

		public interface ISameMethodNameInterface
		{
			[MustImplement]
			int SameMethodName();
		}

		public interface IIntermediateInterface : IRequiredInterface, ISameMethodNameInterface
		{
			[MustImplement(false, ThrowException = true)]
			new int OptionalMethod();
		}

		public interface IOptionalInterface : IIntermediateInterface
		{
		}

		[MustImplement(false, ThrowException = false)]
		public interface IOptionalInterfaceNoException : IRequiredInterface
		{
			int OtherOptionalMethod();
		}

		[MustImplement(true, ThrowException = false)]
		public interface IOtherOptionalInterface
		{
			int SameMethodName();
		}

		public struct TestClass
		{
			public int RequiredMethod()
			{
				return 1;
			}

			public int SameMethodName()
			{
				return 2;
			}
		}

		public class EmptyClass
		{
		}

		[Test]
		public void Test()
		{
			IOptionalInterfaceNoException duck = DuckTyping.Implement<IOptionalInterfaceNoException> (new TestClass());

			Assert.AreEqual(1, duck.RequiredMethod());
			Assert.AreEqual(0, duck.OtherOptionalMethod());
			Assert.AreEqual(2, duck.SameMethodName());

			duck = DuckTyping.Aggregate<IOptionalInterfaceNoException>(new TestClass(), string.Empty, Guid.Empty);

			Assert.AreEqual(1, duck.RequiredMethod());
			Assert.AreEqual(0, duck.OtherOptionalMethod());
			Assert.AreEqual(2, duck.SameMethodName());
		}

		[Test, ExpectedException(typeof(NotImplementedException))]
		public void RuntimeExceptionTest()
		{
			IOptionalInterface duck = DuckTyping.Implement<IOptionalInterface>(new TestClass());

			Assert.AreEqual(1, duck.RequiredMethod());

			// Exception here.
			//
			duck.OptionalMethod();
		}

		[Test, ExpectedException(typeof(NotImplementedException))]
		public void RuntimeAggregateExceptionTest()
		{
			IOptionalInterface duck = DuckTyping.Aggregate<IOptionalInterface>(new TestClass(), new EmptyClass());

			Assert.AreEqual(1, duck.RequiredMethod());

			// Exception here.
			//
			duck.OptionalMethod();
		}

		[Test, ExpectedException(typeof(TypeBuilderException))]
		public void BuildtimeExceptionTest()
		{
			// Exception here.
			//
			IOptionalInterface duck1 = DuckTyping.Implement<IOptionalInterface> (string.Empty);
		}

		[Test, ExpectedException(typeof(TypeBuilderException))]
		public void BuildtimeAggregateExceptionTest()
		{
			// Exception here.
			//
			IOptionalInterface duck1 = DuckTyping.Aggregate<IOptionalInterface>(string.Empty, Guid.Empty);
		}

		[Test]
		public void AsLikeBehaviourTest()
		{
			IOtherOptionalInterface duck = DuckTyping.Implement<IOtherOptionalInterface>(new TestClass());
			Assert.IsNotNull(duck);

			duck = DuckTyping.Implement<IOtherOptionalInterface>(new EmptyClass());
			Assert.IsNull(duck);

			duck = DuckTyping.Implement<IOtherOptionalInterface>(new EmptyClass());
			Assert.IsNull   (duck);

			duck = DuckTyping.Aggregate<IOtherOptionalInterface>(new EmptyClass(), string.Empty);
			Assert.IsNull   (duck);
		}

	}
}
