using System;
using System.Runtime.Serialization;

namespace BLToolkit.TypeInfo
{
	/// <summary>
	/// The exception that is thrown by <see cref="Map"/>.
	/// </summary>
	/// <remarks>
	/// <b>TypeInfoException</b> is used as a wrapper of any exceptions,
	/// which may occur during the the execution of the <see cref="Map"/> class methods.
	/// </remarks>
	[Serializable] 
	public class TypeInfoException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <b>TypeInfoException</b> class.
		/// </summary>
		/// <remarks>
		/// This constructor initializes the <para>Message</para> property of the new instance 
		/// to a system-supplied message that describes the error, 
		/// such as "A mapping exception has occurred."
		/// </remarks>
		public TypeInfoException() 
			: base("A type info exception has occurred.")
		{
		}
        
		/// <summary>
		/// Initializes a new instance of the <b>TypeInfoException</b> class 
		/// with the specified error message.
		/// </summary>
		/// <param name="message">The message to display to the client when the exception is thrown.</param>
		public TypeInfoException(string message) 
			: base(message) 
		{
		}
    	
		/// <summary>
		/// Initializes a new instance of the <b>TypeInfoException</b> class 
		/// with the specified error message and InnerException property.
		/// </summary>
		/// <param name="message">The message to display to the client when the exception is thrown.</param>
		/// <param name="innerException">The InnerException, if any, that threw the current exception.</param>
		public TypeInfoException(string message, Exception innerException) 
			: base(message, innerException) 
		{
		}

		/// <summary>
		/// Initializes a new instance of the <b>TypeInfoException</b> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
		protected TypeInfoException(SerializationInfo info, StreamingContext context) 
			: base(info,context) 
		{
		}
	}
}