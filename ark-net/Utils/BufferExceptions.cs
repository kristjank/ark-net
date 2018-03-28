// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkSettings.cs" company="Ark">
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

using System;

#if FEATURE_SERIALIZABLE
using System.Runtime.Serialization;
#endif

namespace ArkNet.Utils
{
#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    [Serializable]
    internal sealed class BufferUnderflowException : Exception
    {
        public BufferUnderflowException()
        {
        }

#if FEATURE_SERIALIZABLE
        /// <summary>
        /// Initializes a new instance of this class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public BufferUnderflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }

#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    [Serializable]
    internal sealed class BufferOverflowException : Exception
    {
        public BufferOverflowException()
        {
        }

#if FEATURE_SERIALIZABLE
        /// <summary>
        /// Initializes a new instance of this class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public BufferOverflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }

#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    [Serializable]
    internal sealed class ReadOnlyBufferException : Exception
    {
        public ReadOnlyBufferException()
        {
        }

#if FEATURE_SERIALIZABLE
        /// <summary>
        /// Initializes a new instance of this class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public ReadOnlyBufferException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }

#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    [Serializable]
    internal sealed class InvalidMarkException : Exception
    {
        public InvalidMarkException()
        {
        }

#if FEATURE_SERIALIZABLE
        /// <summary>
        /// Initializes a new instance of this class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public InvalidMarkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}