using System;
using System.Collections.Generic;
using System.Text;

namespace ClearCanvas.Server.ShredHost
{
    /// <summary>
    /// Defines the set of operations that are possible on a Shred
    /// </summary>
    /// <seealso cref="ShredExtensionPoint"/>
    public interface IShred
    {
        /// <summary>
        /// Shred should initialize all required resources and data structures and begin 
        /// exeuction of its mainline code
        /// </summary>
        /// <param name="port">The port that </param>
        void Start(int port);
        /// <summary>
        /// Shred should stop, and release all held resources.
        /// </summary>
        void Stop();
        /// <summary>
        /// Shred should return a human-readable, friendly name that will be used in
        /// display lists and other human-readable user-interfaces.
        /// </summary>
        /// <returns></returns>
        string GetDisplayName();
    }
}
