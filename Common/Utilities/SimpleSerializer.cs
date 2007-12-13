#region License

// Copyright (c) 2006-2007, ClearCanvas Inc.
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ClearCanvas.Common.Utilities
{
	// TODO (Stewart): think about using DataMember or specifying attribute type using generics.

	/// <summary>
	/// This attribute class is used to decorate properties of other classes for use with the <see cref="SimpleSerializer"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class SimpleSerializedAttribute : Attribute
	{
		private readonly bool _useInvariantCulture;
	
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public SimpleSerializedAttribute()
			: this(false)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="useInvariantCulture">Specifies whether or not to use the 
		/// <see cref="System.Globalization.CultureInfo.InvariantCulture"/> when converting property values to/from strings.</param>
		public SimpleSerializedAttribute(bool useInvariantCulture)
		{
			_useInvariantCulture = useInvariantCulture;
		}

		/// <summary>
		/// Gets whether or not the <see cref="System.Globalization.CultureInfo.InvariantCulture"/> 
		/// should be used when when converting property values to/from strings.
		/// </summary>
		public bool UseInvariantCulture
		{
			get { return _useInvariantCulture; }
		}
	}

	/// <summary>
	/// Thrown by <see cref="SimpleSerializer"/>'s public methods: <see cref="SimpleSerializer.Deserialize"/> and <see cref="SimpleSerializer.Serialize"/>.
	/// </summary>
	[Serializable]
	public sealed class SimpleSerializerException : Exception
	{
		internal SimpleSerializerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	/// <summary>
	/// The <see cref="SimpleSerializer"/> class can be used to serialize objects to and from a string dictionary (Property/Value pairs).
	/// </summary>
	/// <remarks>
	/// The resulting dictionary can be stored to a file or setting so that objects can be easily persisted and restored.
	/// </remarks>
	public sealed class SimpleSerializer
	{
		private SimpleSerializer()
		{
		}

		/// <summary>
		/// Populates the <paramref name="destinationObject"/>'s properties that are marked with a <see cref="SimpleSerializedAttribute"/> 
		/// attribute using the Property/Value pairs from the input dictionary (<paramref name="sourceValues"/>).
		/// </summary>
		/// <param name="destinationObject">The object whose properties are to be initialized using the input dictionary's Property/Value pairs.</param>
		/// <param name="sourceValues">The input dictionary of Property/Value pairs.</param>
		/// <exception cref="ArgumentNullException">Thrown when either of the input values are null.</exception>
		/// <exception cref="SimpleSerializerException">Thrown when an error occurs during serialization.</exception>
		public static void Serialize(object destinationObject, IDictionary<string, string> sourceValues)
		{
			Platform.CheckForNullReference(destinationObject, "destinationObject");
			Platform.CheckForNullReference(sourceValues, "sourceValues");

			try
			{
				PropertyInfo[] properties = destinationObject.GetType().GetProperties();

				foreach (PropertyInfo property in properties)
				{
					if (!property.IsDefined(typeof(SimpleSerializedAttribute), false))
						continue;

					SimpleSerializedAttribute attribute = (SimpleSerializedAttribute)(property.GetCustomAttributes(typeof (SimpleSerializedAttribute), false)[0]);

					Type propertyType = property.PropertyType;
					if (sourceValues.ContainsKey(property.Name))
					{
						string value = sourceValues[property.Name];

						TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
						if (converter.CanConvertFrom(typeof(string)))
						{
							if (attribute.UseInvariantCulture)
								property.SetValue(destinationObject, converter.ConvertFromString(null, System.Globalization.CultureInfo.InvariantCulture, value), null);
							else 
								property.SetValue(destinationObject, converter.ConvertFromString(value), null);
						}
						else
							throw new InvalidOperationException(String.Format(SR.ExceptionFormatCannotConvertFromStringToType, propertyType.FullName));
					}
				}
			}
			catch (Exception e)
			{
				throw new SimpleSerializerException(String.Format(SR.ExceptionFormatSerializationFailedForType, destinationObject.GetType().FullName), e);
			}
		}

		/// <summary>
		/// Constructs and returns a dictionary of Property/Value pairs from the input <paramref name="sourceObject"/>.
		/// </summary>
		/// <remarks>
		/// Those properties decorated with the <see cref="SimpleSerializedAttribute"/> attribute will have their
		/// values extracted and inserted into the resulting dictionary.
		/// </remarks>
		/// <param name="sourceObject">The object whose properties are to be extracted.</param>
		/// <exception cref="ArgumentNullException">Thrown when the input value is null.</exception>
		/// <exception cref="SimpleSerializerException">Thrown when an error occurs during deserialization.</exception>
		/// <returns>A dictionary of Property/Value pairs.</returns>
		public static IDictionary<string, string> Deserialize(object sourceObject)
		{
			Platform.CheckForNullReference(sourceObject, "sourceObject");

			Dictionary<string, string> dictionary = new Dictionary<string, string>();

			try
			{
				PropertyInfo[] properties = sourceObject.GetType().GetProperties();
				foreach (PropertyInfo property in properties)
				{
					if (!property.IsDefined(typeof(SimpleSerializedAttribute), false))
						continue;

					SimpleSerializedAttribute attribute = (SimpleSerializedAttribute)(property.GetCustomAttributes(typeof(SimpleSerializedAttribute), false)[0]);

					Type propertyType = property.PropertyType;
					object value = property.GetValue(sourceObject, null);

					if (value == null)
						continue;

					TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
					if (converter.CanConvertTo((typeof(string))))
					{
						string stringValue = null;
						if (attribute.UseInvariantCulture)
							stringValue = converter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, value);
						else
							stringValue = converter.ConvertToString(value);

						if (!String.IsNullOrEmpty(stringValue))
							dictionary[property.Name] = stringValue;
					}
					else
						throw new InvalidOperationException(String.Format(SR.ExceptionFormatCannotConvertFromTypeToString, propertyType.FullName));
				}

				return dictionary;
			}
			catch (Exception e)
			{
				throw new SimpleSerializerException(String.Format(SR.ExceptionFormatDeserializationFailedForType, sourceObject.GetType().FullName), e);
			}
		}
	}
}
