#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
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
using System.Text;

namespace ClearCanvas.Dicom
{
    /// <summary>
    /// Static helper class for checking if flags have been set.
    /// </summary>
    public static class Flags
    {
        public static bool IsSet(DicomDumpOptions options, DicomDumpOptions flag)
        {
            return (options & flag) == flag;
        }
        public static bool IsSet(DicomReadOptions options, DicomReadOptions flag)
        {
            return (options & flag) == flag;
        }
        public static bool IsSet(DicomWriteOptions options, DicomWriteOptions flag)
        {
            return (options & flag) == flag;
        }
    }

    /// <summary>
    /// An enumerated value to specify options when generating a dump of a DICOM object.
    /// </summary>
    [Flags]
    public enum DicomDumpOptions
    {
        None = 0,
        ShortenLongValues = 1,
        Restrict80CharactersPerLine= 2,
        KeepGroupLengthElements = 4,
        Default = DicomDumpOptions.ShortenLongValues | DicomDumpOptions.Restrict80CharactersPerLine
    }

    /// <summary>
    /// An enumerated value to specify options when reading DICOM files. 
    /// </summary>
    [Flags]
    public enum DicomReadOptions
    {
        None = 0,
        KeepGroupLengths = 1,
        UseDictionaryForExplicitUN = 2,
        AllowSeekingForContext = 4,
        ReadNonPart10Files = 8,
        DoNotStorePixelDataInDataSet = 16,
        StorePixelDataReferences = 32,
        Default = DicomReadOptions.UseDictionaryForExplicitUN | DicomReadOptions.AllowSeekingForContext | DicomReadOptions.ReadNonPart10Files
    }

    /// <summary>
    /// An enumerated value to specify options when writing DICOM files.
    /// </summary>
    [Flags]
    public enum DicomWriteOptions
    {
        None = 0,
        CalculateGroupLengths = 1,
        ExplicitLengthSequence = 2,
        ExplicitLengthSequenceItem = 4,
        WriteFragmentOffsetTable = 8,
        Default = DicomWriteOptions.WriteFragmentOffsetTable
    }
}
