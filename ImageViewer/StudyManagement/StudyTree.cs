#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using ClearCanvas.Common;

namespace ClearCanvas.ImageViewer.StudyManagement
{
	/// <summary>
	/// A tree representation of the DICOM patient, study, series, SOP hierarchy.
	/// </summary>
	public sealed class StudyTree
	{
		// We add these master dictionaries so we can have rapid
		// look up of study, series and sop objects without having to traverse
		// the tree.
		private PatientCollection _patients;
		private Dictionary<string, Study> _studies;
		private Dictionary<string, Series> _series;
		private Dictionary<string, Sop> _sops;

		/// <summary>
		/// Creates an instance of <see cref="StudyTree"/>.
		/// </summary>
		public StudyTree()
		{
			_patients = new PatientCollection();
			_studies = new Dictionary<string, Study>();
			_series = new Dictionary<string, Series>();
			_sops = new Dictionary<string, Sop>();
		}

		/// <summary>
		/// Gets the collection of <see cref="Patient"/> objects that belong
		/// to this <see cref="StudyTree"/>.
		/// </summary>
		public PatientCollection Patients
		{
			get { return _patients; }
		}

		/// <summary>
		/// Gets a <see cref="Patient"/> with the specified patient ID.
		/// </summary>
		/// <param name="patientId"></param>
		/// <returns>The <see cref="Patient"/> or <b>null</b> if the patient ID
		/// cannot be found.</returns>
		public Patient GetPatient(string patientId)
		{
			Platform.CheckForEmptyString(patientId, "patientId");

			return Patients[patientId];
		}

		/// <summary>
		/// Gets a <see cref="Study"/> with the specified Study Instance UID.
		/// </summary>
		/// <param name="studyInstanceUID"></param>
		/// <returns>The <see cref="Study"/> or <b>null</b> if the Study Instance UID
		/// cannot be found.</returns>
		public Study GetStudy(string studyInstanceUID)
		{
			Platform.CheckForEmptyString(studyInstanceUID, "studyInstanceUID");

			if (!_studies.ContainsKey(studyInstanceUID))
				return null;

			return _studies[studyInstanceUID];
		}

		/// <summary>
		/// Gets a <see cref="Series"/> with the specified Series Instance UID.
		/// </summary>
		/// <param name="seriesInstanceUID"></param>
		/// <returns>The <see cref="Series"/> or <b>null</b> if the Series Instance UID
		/// cannot be found.</returns>
		public Series GetSeries(string seriesInstanceUID)
		{
			Platform.CheckForEmptyString(seriesInstanceUID, "seriesInstanceUID");

			if (!_series.ContainsKey(seriesInstanceUID))
				return null;

			return _series[seriesInstanceUID];
		}

		/// <summary>
		/// Gets a <see cref="Sop"/> with the specified SOP Instance UID.
		/// </summary>
		/// <param name="sopInstanceUID"></param>
		/// <returns>The <see cref="Sop"/> or <b>null</b> if the SOP Instance UID
		/// cannot be found.</returns>
		public Sop GetSop(string sopInstanceUID)
		{
			Platform.CheckForEmptyString(sopInstanceUID, "sopInstanceUID");

			if (!_sops.ContainsKey(sopInstanceUID))
				return null;

			return _sops[sopInstanceUID];
		}

		/// <summary>
		/// Adds a <see cref="Sop"/> to the <see cref="StudyTree"/>.
		/// </summary>
		public bool AddSop(Sop sop)
		{
			Platform.CheckForNullReference(sop, "sop");

			sop.Validate();

			if (_sops.ContainsKey(sop.SopInstanceUid))
			{
				sop.Dispose();
				return false;
			}

			AddPatient(sop);
			AddStudy(sop);
			AddSeries(sop);
			_sops[sop.SopInstanceUid] = sop;
		
			return true;
		}

		#region Private Methods

		private void AddPatient(Sop sop)
		{
			if (_patients[sop.PatientId] != null)
				return;

			Patient patient = new Patient();
			patient.SetSop(sop);

			_patients.Add(patient);
		}

		private void AddStudy(Sop sop)
		{
			if (_studies.ContainsKey(sop.StudyInstanceUid))
				return;

			Patient patient = _patients[sop.PatientId];
			Study study = new Study(patient);
			study.SetSop(sop);
			patient.Studies.Add(study);

			_studies[study.StudyInstanceUid] = study;
		}

		private void AddSeries(Sop sop)
		{
			Series series;
			if (_series.ContainsKey(sop.SeriesInstanceUid))
			{
				series = _series[sop.SeriesInstanceUid];
			}
			else
			{
				Study study = _studies[sop.StudyInstanceUid];
				series = new Series(study);
				series.SetSop(sop);
				study.Series.Add(series);

				_series[series.SeriesInstanceUid] = series;
			}

			sop.ParentSeries = series;
			series.Sops.Add(sop);
		}

		#endregion

		#region Disposal

		#region IDisposable Members

		/// <summary>
		/// Releases all resources used by this <see cref="StudyTree"/>.
		/// </summary>
		public void Dispose()
		{
			try
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception e)
			{
				// shouldn't throw anything from inside Dispose()
				Platform.Log(LogLevel.Error, e);
			}
		}

		#endregion

		/// <summary>
		/// Implementation of the <see cref="IDisposable"/> pattern
		/// </summary>
		/// <param name="disposing">True if this object is being disposed, false if it is being finalized</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_sops != null)
				{
					foreach (Sop sop in _sops.Values)
						sop.Dispose();

					_sops.Clear();
					_sops = null;
				}

				if (_series != null)
				{
					foreach (Series series in _series.Values)
						series.SetSop(null);

					_series.Clear();
					_series = null;
				}

				if (_studies != null)
				{
					foreach (Study study in _studies.Values)
						study.SetSop(null);

					_studies.Clear();
					_studies = null;
				}

				if (_patients != null)
				{
					foreach (Patient patient in _patients)
						patient.SetSop(null);

					_patients.Clear();
					_patients = null;
				}
			}
		}

		#endregion

	}
}
