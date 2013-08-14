#region license
// This File is based on the easynet Project (http://easynet.codeplex.com) created by the Terry Liang.
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System.Runtime.InteropServices;

namespace Mizore.ContentSerializer.easynet_Javabin
{
	/// <summary>
	/// 
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct FloatConverter
	{
		[FieldOffset(0)]
		private float floatValue;
		[FieldOffset(0)]
		private int intValue;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="converter"></param>
		/// <returns></returns>
		public static float ToFloat(int value, ref FloatConverter converter)
		{
			converter.intValue = value;

			return converter.floatValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="converter"></param>
		/// <returns></returns>
		public static int ToInt(float value, ref FloatConverter converter)
		{
			converter.floatValue = value;

			return converter.intValue;
		}

	}
}