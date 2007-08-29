using ClearCanvas.Desktop;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.Common;

namespace ClearCanvas.ImageViewer.Imaging
{
	internal sealed class VoiLutManager : IVoiLutManager
	{
		private GrayscaleImageGraphic _grayscaleImageGraphic;

		public VoiLutManager(GrayscaleImageGraphic grayscaleImageGraphic)
		{
			Platform.CheckForNullReference(grayscaleImageGraphic, "grayscaleImageGraphic");
			_grayscaleImageGraphic = grayscaleImageGraphic;
		}

		#region IVoiLutManager Members

		public ILut GetLut()
		{
			return _grayscaleImageGraphic.VoiLut;
		}

		public void InstallLut(ILut lut)
		{
			ILut existingLut = GetLut();
			if (existingLut is IDataLut)
			{
				//Clear the data in the data lut so it's not hanging around using up memory.
				((IDataLut)existingLut).Clear();
			}
			_grayscaleImageGraphic.InstallVoiLut(lut);
		}

		#endregion

		#region IMemorable Members

		public IMemento CreateMemento()
		{
			return new LutMemento(_grayscaleImageGraphic.VoiLut, _grayscaleImageGraphic.VoiLut.CreateMemento());
		}

		public void SetMemento(IMemento memento)
		{
			ILutMemento lutMemento = memento as ILutMemento;
			Platform.CheckForInvalidCast(lutMemento, "memento", typeof(ILutMemento).Name);

			if (_grayscaleImageGraphic.VoiLut != lutMemento.OriginatingLut)
				this.InstallLut(lutMemento.OriginatingLut);

			if (lutMemento.InnerMemento != null)
				_grayscaleImageGraphic.VoiLut.SetMemento(lutMemento.InnerMemento);
		}

		#endregion
	}
}
