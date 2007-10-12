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
using System.Text;

using ClearCanvas.Common;
using ClearCanvas.Desktop;

namespace ClearCanvas.Desktop
{
    /// <summary>
    /// Extension point for views onto <see cref="StackTabComponentContainer"/>
    /// </summary>
    [ExtensionPoint]
    public class StackTabComponentContainerViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    public enum StackStyle
    {
        /// <summary>
        /// Only one stack can be open at the same time.  
        /// Each stack can be open/closed by clicking on the title bar itself, which act as a button.
        /// </summary>
        ShowOneOnly = 0,

        /// <summary>
        /// Multiple stack can be open at the same time.  
        /// Each stack can be open/closed by clicking on the Down/Up arrow on the title bar.
        /// </summary>
        ShowMultiple = 1
    }

    /// <summary>
    /// StackTabComponentContainer class
    /// </summary>
    [AssociateView(typeof(StackTabComponentContainerViewExtensionPoint))]
    public class StackTabComponentContainer : PagedComponentContainer<TabPage>
    {
        private StackStyle _stackStyle;

        /// <summary>
        /// Constructor
        /// </summary>
        public StackTabComponentContainer(StackStyle stackStyle)
        {
            _stackStyle = stackStyle;
        }

        public StackStyle StackStyle
        {
            get { return _stackStyle; }
        }
    }
}
