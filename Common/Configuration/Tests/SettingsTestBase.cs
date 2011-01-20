#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#if	UNIT_TESTS
#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Configuration;
using ClearCanvas.Common.Utilities;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace ClearCanvas.Common.Configuration.Tests
{
	public enum SettingValue
	{
		Default,
		Current,
		Previous
	}

	public class SettingsTestBase
	{
		protected SettingsTestBase()
		{
			UpgradeSettings.Default.UnitTesting = true;
			Platform.SetExtensionFactory(new NullExtensionFactory());
		}

		public static void TestConfigResourceToFile(string fileName)
		{
			ResourceResolver resolver = new ResourceResolver(typeof(SettingsMigrationTests).Assembly);
			using (Stream resourceStream = resolver.OpenResource("TestPreviousConfiguration.config"))
			{
				StreamToFile(resourceStream, fileName);
				resourceStream.Close();
			}
		}

		public static void StreamToFile(Stream sourceStream, string fileName)
		{
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
			{
				byte[] buffer = new byte[1024];
				while (true)
				{
					int read = sourceStream.Read(buffer, 0, buffer.Length);
					if (read <= 0)
						break;

					fileStream.Write(buffer, 0, read);
					if (read < buffer.Length)
						break;
				}

				fileStream.Close();
			}
		}

		public static void ResetUpgradeSettings()
		{
			UpgradeSettings.Default.Reset();
		}

		public static void ResetAllSettingsClasses()
		{
			TestApplicationSettingsBase.ResetAll();
		}

		public static void ResetSimpleStore()
		{
			SimpleSettingsStore.Instance.Reset();
		}

		public static void PopulateSimpleStore(Type type)
		{
			ResetSimpleStore();
			TestApplicationSettingsBase.ResetAll();
			var settings = ApplicationSettingsHelper.GetSettingsClassInstance(type);

			foreach (SettingsProperty property in settings.Properties)
			{
				var value = CreateSettingsPropertyValue(property, MigrationScope.User, SettingValue.Current);
				if (value != null)
					SimpleSettingsStore.Instance.CurrentUserValues.Add(value);

				value = CreateSettingsPropertyValue(property, MigrationScope.User, SettingValue.Previous);
				if (value != null)
					SimpleSettingsStore.Instance.PreviousUserValues.Add(value);

				value = CreateSettingsPropertyValue(property, MigrationScope.Shared, SettingValue.Current);
				if (value != null)
					SimpleSettingsStore.Instance.CurrentSharedValues.Add(value);

				value = CreateSettingsPropertyValue(property, MigrationScope.Shared, SettingValue.Previous);
				if (value != null)
					SimpleSettingsStore.Instance.PreviousSharedValues.Add(value);
			}

			ValidateStoredValues(type, MigrationScope.Shared, SettingValue.Current);
			ValidateStoredValues(type, MigrationScope.User, SettingValue.Current);
		}

		private static void ValidateStoredValues(Type type, MigrationScope migrationScope, SettingValue expectedValue)
		{
			var settings = ApplicationSettingsHelper.GetSettingsClassInstance(type);
			foreach (SettingsProperty property in settings.Properties)
			{
				string expected = CreateSettingValue(property, migrationScope, expectedValue);
				if (migrationScope == MigrationScope.User)
				{
					if (SettingsPropertyExtensions.IsAppScoped(property))
						continue;

					string actual = (string)settings[property.Name];
					Assert.AreEqual(expected, actual);
				}
				else
				{
					if (SettingsPropertyExtensions.IsAppScoped(property))
					{
						string actual = (string)settings[property.Name];
						Assert.AreEqual(expected, actual);
					}

					string shared = (string)ApplicationSettingsExtensions.GetSharedPropertyValue(settings, property.Name);
					Assert.AreEqual(expected, shared);
				}
			}
		}

		public static SettingsPropertyValue CreateSettingsPropertyValue(SettingsProperty property, MigrationScope migrationScope, SettingValue settingValue)
		{
			var value = CreateSettingValue(property, migrationScope, settingValue);
			if (value == null)
				return null;

			return new SettingsPropertyValue(property) { SerializedValue = value };
		}

		public static Dictionary<string, string> CreateSettingsValues(Type type, MigrationScope migrationScope, SettingValue settingValue)
		{
			var values = new Dictionary<string, string>();
			SettingsGroupDescriptor group = new SettingsGroupDescriptor(type);
			foreach (var property in SettingsPropertyDescriptor.ListSettingsProperties(group))
			{
				var value = CreateSettingValue(property, migrationScope, settingValue);
				if (value != null)
					values[property.Name] = value;
			}
			return values;
		}

		public static string CreateSettingValue(PropertyInfo property, MigrationScope migrationScope, SettingValue settingValue)
		{
			return CreateSettingValue(new SettingsPropertyDescriptor(property), migrationScope, settingValue);
		}

		public static string CreateSettingValue(SettingsProperty property, MigrationScope migrationScope, SettingValue settingValue)
		{
			return CreateSettingValue(SettingsPropertyHelper.GetDescriptor(property), migrationScope, settingValue);
		}

		public static string CreateSettingValue(SettingsPropertyDescriptor property, MigrationScope migrationScope, SettingValue settingValue)
		{
			if (settingValue == SettingValue.Default)
				return property.DefaultValue;

			if (migrationScope == MigrationScope.User && property.Scope == SettingScope.Application)
				return null;

			if (property.Scope == SettingScope.User && migrationScope == MigrationScope.Shared)
				return CreateUserDefaultSettingValue(property.Name, settingValue);

			return CreateSettingValue(property.Name, settingValue);
		}

		public static string CreateSettingValue(string propertyName, SettingValue settingValue)
		{
			return String.Format("{0}{1}", settingValue, propertyName);
		}

		public static string CreateUserDefaultSettingValue(string propertyName, SettingValue settingValue)
		{
			return String.Format("{0}{1}{2}", settingValue, "Default", propertyName);
		}
	}
}

#endif