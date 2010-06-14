﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClearCanvas.Ris.Application.Services {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class WorklistSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static WorklistSettings defaultInstance = ((WorklistSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new WorklistSettings())));
        
        public static WorklistSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// Maximum number of worklists that can be owned by an individual user.
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Maximum number of worklists that can be owned by an individual user.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int MaxPersonalOwnedWorklists {
            get {
                return ((int)(this["MaxPersonalOwnedWorklists"]));
            }
        }
        
        /// <summary>
        /// Maximum number of worklists that can be owned by a staff group.
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Maximum number of worklists that can be owned by a staff group.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int MaxGroupOwnedWorklists {
            get {
                return ((int)(this["MaxGroupOwnedWorklists"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool WorklistItemCountCachingEnabled {
            get {
                return ((bool)(this["WorklistItemCountCachingEnabled"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int WorklistItemCountCachingTimeToLiveSeconds {
            get {
                return ((int)(this["WorklistItemCountCachingTimeToLiveSeconds"]));
            }
        }
    }
}
