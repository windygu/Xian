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
using System.Collections;
using System.Collections.Generic;

namespace ClearCanvas.Dicom
{
    /// <summary>
    /// Enumerated value to differentiate between little and big endian.
    /// </summary>
    public enum Endian
    {
        Little,
        Big
    }

    /// <summary>
    /// This class contains transfer syntax definitions.
    /// </summary>
    public class TransferSyntax
    {
        /// <summary>String representing
        /// <para>Deflated Explicit VR Little Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.1.99</para>
        /// </summary>
        public static readonly String DeflatedExplicitVrLittleEndianUid = "1.2.840.10008.1.2.1.99";

        /// <summary>TransferSyntax object representing
        /// <para>Deflated Explicit VR Little Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.1.99</para>
        /// </summary>
        public static readonly TransferSyntax DeflatedExplicitVrLittleEndian =
                    new TransferSyntax("Deflated Explicit VR Little Endian",
                                 TransferSyntax.DeflatedExplicitVrLittleEndianUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 true // Deflated?
                                 );

        /// <summary>String representing
        /// <para>Explicit VR Big Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.2</para>
        /// </summary>
        public static readonly String ExplicitVrBigEndianUid = "1.2.840.10008.1.2.2";

        /// <summary>TransferSyntax object representing
        /// <para>Explicit VR Big Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.2</para>
        /// </summary>
        public static readonly TransferSyntax ExplicitVrBigEndian =
                    new TransferSyntax("Explicit VR Big Endian",
                                 TransferSyntax.ExplicitVrBigEndianUid,
                                 false, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>Explicit VR Little Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.1</para>
        /// </summary>
        public static readonly String ExplicitVrLittleEndianUid = "1.2.840.10008.1.2.1";

        /// <summary>TransferSyntax object representing
        /// <para>Explicit VR Little Endian</para>
        /// <para>UID: 1.2.840.10008.1.2.1</para>
        /// </summary>
        public static readonly TransferSyntax ExplicitVrLittleEndian =
                    new TransferSyntax("Explicit VR Little Endian",
                                 TransferSyntax.ExplicitVrLittleEndianUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>Implicit VR Little Endian: Default Transfer Syntax for DICOM</para>
        /// <para>UID: 1.2.840.10008.1.2</para>
        /// </summary>
        public static readonly String ImplicitVrLittleEndianUid = "1.2.840.10008.1.2";

        /// <summary>TransferSyntax object representing
        /// <para>Implicit VR Little Endian: Default Transfer Syntax for DICOM</para>
        /// <para>UID: 1.2.840.10008.1.2</para>
        /// </summary>
        public static readonly TransferSyntax ImplicitVrLittleEndian =
                    new TransferSyntax("Implicit VR Little Endian: Default Transfer Syntax for DICOM",
                                 TransferSyntax.ImplicitVrLittleEndianUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 false, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG 2000 Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.91</para>
        /// </summary>
        public static readonly String Jpeg2000ImageCompressionUid = "1.2.840.10008.1.2.4.91";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG 2000 Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.91</para>
        /// </summary>
        public static readonly TransferSyntax Jpeg2000ImageCompression =
                    new TransferSyntax("JPEG 2000 Image Compression",
                                 TransferSyntax.Jpeg2000ImageCompressionUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG 2000 Image Compression (Lossless Only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.90</para>
        /// </summary>
        public static readonly String Jpeg2000ImageCompressionLosslessOnlyUid = "1.2.840.10008.1.2.4.90";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG 2000 Image Compression (Lossless Only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.90</para>
        /// </summary>
        public static readonly TransferSyntax Jpeg2000ImageCompressionLosslessOnly =
                    new TransferSyntax("JPEG 2000 Image Compression (Lossless Only)",
                                 TransferSyntax.Jpeg2000ImageCompressionLosslessOnlyUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG 2000 Part 2 Multi-component  Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.93</para>
        /// </summary>
        public static readonly String Jpeg2000Part2MultiComponentImageCompressionUid = "1.2.840.10008.1.2.4.93";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG 2000 Part 2 Multi-component  Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.93</para>
        /// </summary>
        public static readonly TransferSyntax Jpeg2000Part2MultiComponentImageCompression =
                    new TransferSyntax("JPEG 2000 Part 2 Multi-component  Image Compression",
                                 TransferSyntax.Jpeg2000Part2MultiComponentImageCompressionUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG 2000 Part 2 Multi-component  Image Compression (Lossless Only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.92</para>
        /// </summary>
        public static readonly String Jpeg2000Part2MultiComponentImageCompressionLosslessOnlyUid = "1.2.840.10008.1.2.4.92";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG 2000 Part 2 Multi-component  Image Compression (Lossless Only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.92</para>
        /// </summary>
        public static readonly TransferSyntax Jpeg2000Part2MultiComponentImageCompressionLosslessOnly =
                    new TransferSyntax("JPEG 2000 Part 2 Multi-component  Image Compression (Lossless Only)",
                                 TransferSyntax.Jpeg2000Part2MultiComponentImageCompressionLosslessOnlyUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.50</para>
        /// </summary>
        public static readonly String JpegBaselineProcess1Uid = "1.2.840.10008.1.2.4.50";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.50</para>
        /// </summary>
        public static readonly TransferSyntax JpegBaselineProcess1 =
                    new TransferSyntax("JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression",
                                 TransferSyntax.JpegBaselineProcess1Uid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.51</para>
        /// </summary>
        public static readonly String JpegExtendedProcess24Uid = "1.2.840.10008.1.2.4.51";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.51</para>
        /// </summary>
        public static readonly TransferSyntax JpegExtendedProcess24 =
                    new TransferSyntax("JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)",
                                 TransferSyntax.JpegExtendedProcess24Uid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Extended (Process 3 &amp; 5) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.52</para>
        /// </summary>
        public static readonly String JpegExtendedProcess35RetiredUid = "1.2.840.10008.1.2.4.52";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Extended (Process 3 &amp; 5) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.52</para>
        /// </summary>
        public static readonly TransferSyntax JpegExtendedProcess35Retired =
                    new TransferSyntax("JPEG Extended (Process 3 &amp; 5) (Retired)",
                                 TransferSyntax.JpegExtendedProcess35RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.59</para>
        /// </summary>
        public static readonly String JpegExtendedHierarchicalProcess1618RetiredUid = "1.2.840.10008.1.2.4.59";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.59</para>
        /// </summary>
        public static readonly TransferSyntax JpegExtendedHierarchicalProcess1618Retired =
                    new TransferSyntax("JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)",
                                 TransferSyntax.JpegExtendedHierarchicalProcess1618RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Extended, Hierarchical (Process 17 &amp; 19) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.60</para>
        /// </summary>
        public static readonly String JpegExtendedHierarchicalProcess1719RetiredUid = "1.2.840.10008.1.2.4.60";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Extended, Hierarchical (Process 17 &amp; 19) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.60</para>
        /// </summary>
        public static readonly TransferSyntax JpegExtendedHierarchicalProcess1719Retired =
                    new TransferSyntax("JPEG Extended, Hierarchical (Process 17 &amp; 19) (Retired)",
                                 TransferSyntax.JpegExtendedHierarchicalProcess1719RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Full Progression, Hierarchical (Process 24 &amp; 26) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.63</para>
        /// </summary>
        public static readonly String JpegFullProgressionHierarchicalProcess2426RetiredUid = "1.2.840.10008.1.2.4.63";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Full Progression, Hierarchical (Process 24 &amp; 26) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.63</para>
        /// </summary>
        public static readonly TransferSyntax JpegFullProgressionHierarchicalProcess2426Retired =
                    new TransferSyntax("JPEG Full Progression, Hierarchical (Process 24 &amp; 26) (Retired)",
                                 TransferSyntax.JpegFullProgressionHierarchicalProcess2426RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Full Progression, Hierarchical (Process 25 &amp; 27) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.64</para>
        /// </summary>
        public static readonly String JpegFullProgressionHierarchicalProcess2527RetiredUid = "1.2.840.10008.1.2.4.64";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Full Progression, Hierarchical (Process 25 &amp; 27) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.64</para>
        /// </summary>
        public static readonly TransferSyntax JpegFullProgressionHierarchicalProcess2527Retired =
                    new TransferSyntax("JPEG Full Progression, Hierarchical (Process 25 &amp; 27) (Retired)",
                                 TransferSyntax.JpegFullProgressionHierarchicalProcess2527RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.55</para>
        /// </summary>
        public static readonly String JpegFullProgressionNonHierarchicalProcess1012RetiredUid = "1.2.840.10008.1.2.4.55";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.55</para>
        /// </summary>
        public static readonly TransferSyntax JpegFullProgressionNonHierarchicalProcess1012Retired =
                    new TransferSyntax("JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) (Retired)",
                                 TransferSyntax.JpegFullProgressionNonHierarchicalProcess1012RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.56</para>
        /// </summary>
        public static readonly String JpegFullProgressionNonHierarchicalProcess1113RetiredUid = "1.2.840.10008.1.2.4.56";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.56</para>
        /// </summary>
        public static readonly TransferSyntax JpegFullProgressionNonHierarchicalProcess1113Retired =
                    new TransferSyntax("JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) (Retired)",
                                 TransferSyntax.JpegFullProgressionNonHierarchicalProcess1113RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Lossless, Hierarchical (Process 28) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.65</para>
        /// </summary>
        public static readonly String JpegLosslessHierarchicalProcess28RetiredUid = "1.2.840.10008.1.2.4.65";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Lossless, Hierarchical (Process 28) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.65</para>
        /// </summary>
        public static readonly TransferSyntax JpegLosslessHierarchicalProcess28Retired =
                    new TransferSyntax("JPEG Lossless, Hierarchical (Process 28) (Retired)",
                                 TransferSyntax.JpegLosslessHierarchicalProcess28RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Lossless, Hierarchical (Process 29) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.66</para>
        /// </summary>
        public static readonly String JpegLosslessHierarchicalProcess29RetiredUid = "1.2.840.10008.1.2.4.66";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Lossless, Hierarchical (Process 29) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.66</para>
        /// </summary>
        public static readonly TransferSyntax JpegLosslessHierarchicalProcess29Retired =
                    new TransferSyntax("JPEG Lossless, Hierarchical (Process 29) (Retired)",
                                 TransferSyntax.JpegLosslessHierarchicalProcess29RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Lossless, Non-Hierarchical (Process 14)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.57</para>
        /// </summary>
        public static readonly String JpegLosslessNonHierarchicalProcess14Uid = "1.2.840.10008.1.2.4.57";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Lossless, Non-Hierarchical (Process 14)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.57</para>
        /// </summary>
        public static readonly TransferSyntax JpegLosslessNonHierarchicalProcess14 =
                    new TransferSyntax("JPEG Lossless, Non-Hierarchical (Process 14)",
                                 TransferSyntax.JpegLosslessNonHierarchicalProcess14Uid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.58</para>
        /// </summary>
        public static readonly String JpegLosslessNonHierarchicalProcess15RetiredUid = "1.2.840.10008.1.2.4.58";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.58</para>
        /// </summary>
        public static readonly TransferSyntax JpegLosslessNonHierarchicalProcess15Retired =
                    new TransferSyntax("JPEG Lossless, Non-Hierarchical (Process 15) (Retired)",
                                 TransferSyntax.JpegLosslessNonHierarchicalProcess15RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.70</para>
        /// </summary>
        public static readonly String JpegLosslessNonHierarchicalFirstOrderPredictionProcess14SelectionValue1Uid = "1.2.840.10008.1.2.4.70";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.70</para>
        /// </summary>
        public static readonly TransferSyntax JpegLosslessNonHierarchicalFirstOrderPredictionProcess14SelectionValue1 =
                    new TransferSyntax("JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression",
                                 TransferSyntax.JpegLosslessNonHierarchicalFirstOrderPredictionProcess14SelectionValue1Uid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.61</para>
        /// </summary>
        public static readonly String JpegSpectralSelectionHierarchicalProcess2022RetiredUid = "1.2.840.10008.1.2.4.61";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.61</para>
        /// </summary>
        public static readonly TransferSyntax JpegSpectralSelectionHierarchicalProcess2022Retired =
                    new TransferSyntax("JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) (Retired)",
                                 TransferSyntax.JpegSpectralSelectionHierarchicalProcess2022RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.62</para>
        /// </summary>
        public static readonly String JpegSpectralSelectionHierarchicalProcess2123RetiredUid = "1.2.840.10008.1.2.4.62";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.62</para>
        /// </summary>
        public static readonly TransferSyntax JpegSpectralSelectionHierarchicalProcess2123Retired =
                    new TransferSyntax("JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) (Retired)",
                                 TransferSyntax.JpegSpectralSelectionHierarchicalProcess2123RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.53</para>
        /// </summary>
        public static readonly String JpegSpectralSelectionNonHierarchicalProcess68RetiredUid = "1.2.840.10008.1.2.4.53";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.53</para>
        /// </summary>
        public static readonly TransferSyntax JpegSpectralSelectionNonHierarchicalProcess68Retired =
                    new TransferSyntax("JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) (Retired)",
                                 TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess68RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.54</para>
        /// </summary>
        public static readonly String JpegSpectralSelectionNonHierarchicalProcess79RetiredUid = "1.2.840.10008.1.2.4.54";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) (Retired)</para>
        /// <para>UID: 1.2.840.10008.1.2.4.54</para>
        /// </summary>
        public static readonly TransferSyntax JpegSpectralSelectionNonHierarchicalProcess79Retired =
                    new TransferSyntax("JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) (Retired)",
                                 TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess79RetiredUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG-LS Lossless Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.80</para>
        /// </summary>
        public static readonly String JpegLsLosslessImageCompressionUid = "1.2.840.10008.1.2.4.80";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG-LS Lossless Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.80</para>
        /// </summary>
        public static readonly TransferSyntax JpegLsLosslessImageCompression =
                    new TransferSyntax("JPEG-LS Lossless Image Compression",
                                 TransferSyntax.JpegLsLosslessImageCompressionUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPEG-LS Lossy (Near-Lossless) Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.81</para>
        /// </summary>
        public static readonly String JpegLsLossyNearLosslessImageCompressionUid = "1.2.840.10008.1.2.4.81";

        /// <summary>TransferSyntax object representing
        /// <para>JPEG-LS Lossy (Near-Lossless) Image Compression</para>
        /// <para>UID: 1.2.840.10008.1.2.4.81</para>
        /// </summary>
        public static readonly TransferSyntax JpegLsLossyNearLosslessImageCompression =
                    new TransferSyntax("JPEG-LS Lossy (Near-Lossless) Image Compression",
                                 TransferSyntax.JpegLsLossyNearLosslessImageCompressionUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPIP Referenced</para>
        /// <para>UID: 1.2.840.10008.1.2.4.94</para>
        /// </summary>
        public static readonly String JpipReferencedUid = "1.2.840.10008.1.2.4.94";

        /// <summary>TransferSyntax object representing
        /// <para>JPIP Referenced</para>
        /// <para>UID: 1.2.840.10008.1.2.4.94</para>
        /// </summary>
        public static readonly TransferSyntax JpipReferenced =
                    new TransferSyntax("JPIP Referenced",
                                 TransferSyntax.JpipReferencedUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>JPIP Referenced Deflate</para>
        /// <para>UID: 1.2.840.10008.1.2.4.95</para>
        /// </summary>
        public static readonly String JpipReferencedDeflateUid = "1.2.840.10008.1.2.4.95";

        /// <summary>TransferSyntax object representing
        /// <para>JPIP Referenced Deflate</para>
        /// <para>UID: 1.2.840.10008.1.2.4.95</para>
        /// </summary>
        public static readonly TransferSyntax JpipReferencedDeflate =
                    new TransferSyntax("JPIP Referenced Deflate",
                                 TransferSyntax.JpipReferencedDeflateUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 true // Deflated?
                                 );

        /// <summary>String representing
        /// <para>MPEG2 Main Profile @ Main Level</para>
        /// <para>UID: 1.2.840.10008.1.2.4.100</para>
        /// </summary>
        public static readonly String Mpeg2MainProfileMainLevelUid = "1.2.840.10008.1.2.4.100";

        /// <summary>TransferSyntax object representing
        /// <para>MPEG2 Main Profile @ Main Level</para>
        /// <para>UID: 1.2.840.10008.1.2.4.100</para>
        /// </summary>
        public static readonly TransferSyntax Mpeg2MainProfileMainLevel =
                    new TransferSyntax("MPEG2 Main Profile @ Main Level",
                                 TransferSyntax.Mpeg2MainProfileMainLevelUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>RFC 2557 MIME encapsulation</para>
        /// <para>UID: 1.2.840.10008.1.2.6.1</para>
        /// </summary>
        public static readonly String Rfc2557MimeEncapsulationUid = "1.2.840.10008.1.2.6.1";

        /// <summary>TransferSyntax object representing
        /// <para>RFC 2557 MIME encapsulation</para>
        /// <para>UID: 1.2.840.10008.1.2.6.1</para>
        /// </summary>
        public static readonly TransferSyntax Rfc2557MimeEncapsulation =
                    new TransferSyntax("RFC 2557 MIME encapsulation",
                                 TransferSyntax.Rfc2557MimeEncapsulationUid,
                                 true, // Little Endian?
                                 false, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        /// <summary>String representing
        /// <para>RLE Lossless</para>
        /// <para>UID: 1.2.840.10008.1.2.5</para>
        /// </summary>
        public static readonly String RleLosslessUid = "1.2.840.10008.1.2.5";

        /// <summary>TransferSyntax object representing
        /// <para>RLE Lossless</para>
        /// <para>UID: 1.2.840.10008.1.2.5</para>
        /// </summary>
        public static readonly TransferSyntax RleLossless =
                    new TransferSyntax("RLE Lossless",
                                 TransferSyntax.RleLosslessUid,
                                 true, // Little Endian?
                                 true, // Encapsulated?
                                 true, // Explicit VR?
                                 false // Deflated?
                                 );

        // Internal members
        private static Dictionary<String,TransferSyntax> _transferSyntaxes = new Dictionary<String,TransferSyntax>();
        private static bool _listInit = false;
        private bool _littleEndian;
        private bool _encapsulated;
        private bool _explicitVr;
        private bool _deflate;
        private String _name;
        private String _uid;

        ///<summary>
        /// Constructor for transfer syntax objects
        ///</summary>
        public TransferSyntax(String name, String uid, bool bLittleEndian, bool bEncapsulated, bool bExplicitVr, bool bDeflate)
        {
            this._uid = uid;
            this._name = name;
            this._littleEndian = bLittleEndian;
            this._encapsulated = bEncapsulated;
            this._explicitVr = bExplicitVr;
            this._deflate = bDeflate;
        }

        ///<summary>Override to the ToString() method, returns the name of the transfer syntax.</summary>
        public override String ToString()
        {
            return _name;
        }

        ///<summary>Property representing the UID string of transfer syntax.</summary>
        public String UidString
        {
            get { return _uid; }
        }

        ///<summary>Property representing the DicomUid of the transfer syntax.</summary>
        public DicomUid DicomUid
        {
            get
            {
                return new DicomUid(_uid, _name, UidType.TransferSyntax);
            }
        }

        ///<summary>Property representing the name of the transfer syntax.</summary>
        public String Name
        {
            get { return _name; }
        }

        ///<summary>Property representing if the transfer syntax is encoded as little endian.</summary>
        public bool LittleEndian
        {
            get { return _littleEndian; }
        }

        ///<summary>Property representing the Endian enumerated value for the transfer syntax.</summary>
          public Endian Endian
        {
            get
            {
                if (_littleEndian)
                    return Endian.Little;

                return Endian.Big;
            }
        }

        ///<summary>Property representing if the transfer syntax is encoded as encapsulated.</summary>
        public bool Encapsulated
        {
            get { return _encapsulated; }
        }

        ///<summary>Property representing if the transfer syntax is encoded as explicit Value Representation.</summary>
        public bool ExplicitVr
        {
            get { return _explicitVr; }
        }

        ///<summary>Property representing if the transfer syntax is encoded in deflate format.</summary>
        public bool Deflate
        {
            get { return _deflate; }
        }

        /// <summary>
        /// Get a TransferSyntax object for a specific transfer syntax UID.
        /// </summary>
        public static TransferSyntax GetTransferSyntax(String uid)
        {
            if (_listInit == false)
            {
                _listInit = true;

                _transferSyntaxes.Add(TransferSyntax.DeflatedExplicitVrLittleEndianUid,
                                      TransferSyntax.DeflatedExplicitVrLittleEndian);

                _transferSyntaxes.Add(TransferSyntax.ExplicitVrBigEndianUid,
                                      TransferSyntax.ExplicitVrBigEndian);

                _transferSyntaxes.Add(TransferSyntax.ExplicitVrLittleEndianUid,
                                      TransferSyntax.ExplicitVrLittleEndian);

                _transferSyntaxes.Add(TransferSyntax.ImplicitVrLittleEndianUid,
                                      TransferSyntax.ImplicitVrLittleEndian);

                _transferSyntaxes.Add(TransferSyntax.Jpeg2000ImageCompressionUid,
                                      TransferSyntax.Jpeg2000ImageCompression);

                _transferSyntaxes.Add(TransferSyntax.Jpeg2000ImageCompressionLosslessOnlyUid,
                                      TransferSyntax.Jpeg2000ImageCompressionLosslessOnly);

                _transferSyntaxes.Add(TransferSyntax.Jpeg2000Part2MultiComponentImageCompressionUid,
                                      TransferSyntax.Jpeg2000Part2MultiComponentImageCompression);

                _transferSyntaxes.Add(TransferSyntax.Jpeg2000Part2MultiComponentImageCompressionLosslessOnlyUid,
                                      TransferSyntax.Jpeg2000Part2MultiComponentImageCompressionLosslessOnly);

                _transferSyntaxes.Add(TransferSyntax.JpegBaselineProcess1Uid,
                                      TransferSyntax.JpegBaselineProcess1);

                _transferSyntaxes.Add(TransferSyntax.JpegExtendedProcess24Uid,
                                      TransferSyntax.JpegExtendedProcess24);

                _transferSyntaxes.Add(TransferSyntax.JpegExtendedProcess35RetiredUid,
                                      TransferSyntax.JpegExtendedProcess35Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegExtendedHierarchicalProcess1618RetiredUid,
                                      TransferSyntax.JpegExtendedHierarchicalProcess1618Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegExtendedHierarchicalProcess1719RetiredUid,
                                      TransferSyntax.JpegExtendedHierarchicalProcess1719Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegFullProgressionHierarchicalProcess2426RetiredUid,
                                      TransferSyntax.JpegFullProgressionHierarchicalProcess2426Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegFullProgressionHierarchicalProcess2527RetiredUid,
                                      TransferSyntax.JpegFullProgressionHierarchicalProcess2527Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegFullProgressionNonHierarchicalProcess1012RetiredUid,
                                      TransferSyntax.JpegFullProgressionNonHierarchicalProcess1012Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegFullProgressionNonHierarchicalProcess1113RetiredUid,
                                      TransferSyntax.JpegFullProgressionNonHierarchicalProcess1113Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegLosslessHierarchicalProcess28RetiredUid,
                                      TransferSyntax.JpegLosslessHierarchicalProcess28Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegLosslessHierarchicalProcess29RetiredUid,
                                      TransferSyntax.JpegLosslessHierarchicalProcess29Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegLosslessNonHierarchicalProcess14Uid,
                                      TransferSyntax.JpegLosslessNonHierarchicalProcess14);

                _transferSyntaxes.Add(TransferSyntax.JpegLosslessNonHierarchicalProcess15RetiredUid,
                                      TransferSyntax.JpegLosslessNonHierarchicalProcess15Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegLosslessNonHierarchicalFirstOrderPredictionProcess14SelectionValue1Uid,
                                      TransferSyntax.JpegLosslessNonHierarchicalFirstOrderPredictionProcess14SelectionValue1);

                _transferSyntaxes.Add(TransferSyntax.JpegSpectralSelectionHierarchicalProcess2022RetiredUid,
                                      TransferSyntax.JpegSpectralSelectionHierarchicalProcess2022Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegSpectralSelectionHierarchicalProcess2123RetiredUid,
                                      TransferSyntax.JpegSpectralSelectionHierarchicalProcess2123Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess68RetiredUid,
                                      TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess68Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess79RetiredUid,
                                      TransferSyntax.JpegSpectralSelectionNonHierarchicalProcess79Retired);

                _transferSyntaxes.Add(TransferSyntax.JpegLsLosslessImageCompressionUid,
                                      TransferSyntax.JpegLsLosslessImageCompression);

                _transferSyntaxes.Add(TransferSyntax.JpegLsLossyNearLosslessImageCompressionUid,
                                      TransferSyntax.JpegLsLossyNearLosslessImageCompression);

                _transferSyntaxes.Add(TransferSyntax.JpipReferencedUid,
                                      TransferSyntax.JpipReferenced);

                _transferSyntaxes.Add(TransferSyntax.JpipReferencedDeflateUid,
                                      TransferSyntax.JpipReferencedDeflate);

                _transferSyntaxes.Add(TransferSyntax.Mpeg2MainProfileMainLevelUid,
                                      TransferSyntax.Mpeg2MainProfileMainLevel);

                _transferSyntaxes.Add(TransferSyntax.Rfc2557MimeEncapsulationUid,
                                      TransferSyntax.Rfc2557MimeEncapsulation);

                _transferSyntaxes.Add(TransferSyntax.RleLosslessUid,
                                      TransferSyntax.RleLossless);

            }

            if (!_transferSyntaxes.ContainsKey(uid))
                return null;

            return _transferSyntaxes[uid];
        }
    }
}
