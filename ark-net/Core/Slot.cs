// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Slot.cs" company="Ark">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ArkNet.Core
{
    // References:
    // - TransactionApi.cs

    /// <summary>
    /// Provides a timer for timestamping created transactions.
    /// </summary>
    /// 
	public class Slot
	{
        #region Fields

        /// <summary>
        /// The initial epoch to calculate the elapsed time from.
        /// </summary>
        /// 
        private static readonly DateTime beginEpoch = new DateTime(2017, 3, 21, 13, 00, 0, DateTimeKind.Utc);

        #endregion

        #region Methods

        /// <summary>
        /// Returns the elapsed time up to the current epoch.
        /// </summary>
        /// 
        /// <returns>Returns the number of seconds elapsed an <see cref="System.Int32"/>.</returns>
        /// 
        public static int GetTime()
		{
			//DateTime date = DateTime.Now;
            
			return Convert.ToInt32((DateTime.UtcNow - beginEpoch).TotalMilliseconds / 1000);
		}

        #endregion
    }
}
