#region License

// Copyright (c) 2009, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

#if	UNIT_TESTS

#pragma warning disable 1591,0419,1574,1587

using System.Diagnostics;
using NUnit.Framework;

namespace ClearCanvas.ImageViewer.Volume.Mpr.Tests
{
	[TestFixture]
	public class SlicerTests
	{
		public SlicerTests() {}

		[Test]
		[Ignore("This test case doesn't handle multiple solutions for the same rotation")]
		public void TestInverseRotationMatrices()
		{
			for (int x = 0; x < 360; x += 15)
			{
				for (int y = 0; y < 360; y += 15)
				{
					for (int z = 0; z < 360; z += 15)
					{
						VolumeSlicerParams expected = new VolumeSlicerParams(x, y, z);
						VolumeSlicerParams test = new VolumeSlicerParams(expected.SlicingPlaneRotation);

						// Within 1 one-thousandth of a degree is close enough, no?
						Trace.WriteLine(string.Format("Expected: {0},{1},{2} Actual: {3},{4},{5}", expected.RotateAboutX, expected.RotateAboutY, expected.RotateAboutZ, test.RotateAboutX, test.RotateAboutY, test.RotateAboutZ));
						Assert.AreEqual(expected.RotateAboutX, NormalizeDegrees(test.RotateAboutX), 1e-3, "Computation Error in X: Q={0},{1},{2}", x, y, z);
						Assert.AreEqual(expected.RotateAboutY, NormalizeDegrees(test.RotateAboutY), 1e-3, "Computation Error in Y: Q={0},{1},{2}", x, y, z);
						Assert.AreEqual(expected.RotateAboutZ, NormalizeDegrees(test.RotateAboutZ), 1e-3, "Computation Error in Z: Q={0},{1},{2}", x, y, z);
					}
				}
			}
		}

		private static float NormalizeDegrees(float degrees)
		{
			return ((degrees%360) + 360)%360;
		}
	}
}

#endif