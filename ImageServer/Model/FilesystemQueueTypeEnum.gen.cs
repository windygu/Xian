#region License

// Copyright (c) 2009, ClearCanvas Inc.
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

// This file is auto-generated by the ClearCanvas.Model.SqlServer2005.CodeGenerator project.

namespace ClearCanvas.ImageServer.Model
{
    using System;
    using System.Collections.Generic;
    using ClearCanvas.ImageServer.Model.EntityBrokers;
    using ClearCanvas.ImageServer.Enterprise;
    using System.Reflection;

[Serializable]
public partial class FilesystemQueueTypeEnum : ServerEnum
{
      #region Private Static Members
      private static readonly FilesystemQueueTypeEnum _DeleteStudy = GetEnum("DeleteStudy");
      private static readonly FilesystemQueueTypeEnum _PurgeStudy = GetEnum("PurgeStudy");
      private static readonly FilesystemQueueTypeEnum _TierMigrate = GetEnum("TierMigrate");
      private static readonly FilesystemQueueTypeEnum _LosslessCompress = GetEnum("LosslessCompress");
      private static readonly FilesystemQueueTypeEnum _LossyCompress = GetEnum("LossyCompress");
      #endregion

      #region Public Static Properties
      /// <summary>
      /// A record telling when a study is eligible for deletion.  The study will be completely removed from the system.
      /// </summary>
      public static FilesystemQueueTypeEnum DeleteStudy
      {
          get { return _DeleteStudy; }
      }
      /// <summary>
      /// A record telling when a study can be purged from a filesystem.  Only archived studies can be purged.  The study will remain archived and can be restored.
      /// </summary>
      public static FilesystemQueueTypeEnum PurgeStudy
      {
          get { return _PurgeStudy; }
      }
      /// <summary>
      /// A record telling when a study is eligable to be migrated to a lower tier filesystem.
      /// </summary>
      public static FilesystemQueueTypeEnum TierMigrate
      {
          get { return _TierMigrate; }
      }
      /// <summary>
      /// A record telling when a study is eligable for lossless compression and the type of compression to be performed on the study.
      /// </summary>
      public static FilesystemQueueTypeEnum LosslessCompress
      {
          get { return _LosslessCompress; }
      }
      /// <summary>
      /// A record telling when a study is eligable for lossy compression and the type of compression to be performed.
      /// </summary>
      public static FilesystemQueueTypeEnum LossyCompress
      {
          get { return _LossyCompress; }
      }

      #endregion

      #region Constructors
      public FilesystemQueueTypeEnum():base("FilesystemQueueTypeEnum")
      {}
      #endregion
      #region Public Members
      public override void SetEnum(short val)
      {
          ServerEnumHelper<FilesystemQueueTypeEnum, IFilesystemQueueTypeEnumBroker>.SetEnum(this, val);
      }
      static public List<FilesystemQueueTypeEnum> GetAll()
      {
          return ServerEnumHelper<FilesystemQueueTypeEnum, IFilesystemQueueTypeEnumBroker>.GetAll();
      }
      static public FilesystemQueueTypeEnum GetEnum(string lookup)
      {
          return ServerEnumHelper<FilesystemQueueTypeEnum, IFilesystemQueueTypeEnumBroker>.GetEnum(lookup);
      }
      #endregion
}
}
