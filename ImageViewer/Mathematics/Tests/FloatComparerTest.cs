#if	UNIT_TESTS

using System;
using NUnit.Framework;
using System.Drawing;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.ImageViewer.Mathematics.Tests
{
	[TestFixture]
	public class FloatComparerTest
	{
		public FloatComparerTest()
		{
		}

		[TestFixtureSetUp]
		public void Init()
		{
		}
		
		[TestFixtureTearDown]
		public void Cleanup()
		{
		}

		[Test]
		public void AreEqual()
		{
			float x = 1.0f;
			float y = 2.0f;
			float z = x + y;

			Assert.IsTrue(FloatComparer.AreEqual(z, 3.0f));
		}

		[Test]
		public void IsGreaterThan()
		{
			float x = 1.001f;
			float y = 1.0f;

			Assert.IsTrue(FloatComparer.IsGreaterThan(x, y));
		}

		[Test]
		public void IsLessThan()
		{
			float x = 1.001f;
			float y = 1.0f;

			Assert.IsTrue(FloatComparer.IsLessThan(y, x));
		}
	}
}

#endif