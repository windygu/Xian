using System.Collections.Generic;
using ClearCanvas.Enterprise.Common;

namespace ClearCanvas.Ris.Client
{
	public class OrderAdditionalInfoComponent : DHtmlComponent
	{
		private IDictionary<string, string> _orderExtendedProperties;
		private DataContractBase _healthcareContext;

		public OrderAdditionalInfoComponent(IDictionary<string, string> orderExtendedProperties)
		{
			_orderExtendedProperties = orderExtendedProperties;
		}

		public OrderAdditionalInfoComponent()
		{
			_orderExtendedProperties = new Dictionary<string, string>();
		}

		/// <summary>
		/// Gets or sets the dictionary of order extended properties that this component will
		/// use to store data.
		/// </summary>
		public IDictionary<string, string> OrderExtendedProperties
		{
			get { return _orderExtendedProperties; }
			set { _orderExtendedProperties = value; }
		}

		public override void Start()
		{
			SetUrl(WebResourcesSettings.Default.OrderAdditionalInfoPageUrl);
			base.Start();
		}

		protected override IDictionary<string, string> TagData
		{
			get
			{
				return _orderExtendedProperties;
			}
		}

		protected override DataContractBase GetHealthcareContext()
		{
			return _healthcareContext;
		}

		public DataContractBase HealthcareContext
		{
			get { return _healthcareContext; }
			set
			{
				_healthcareContext = value;
			}
		}
	}
}
