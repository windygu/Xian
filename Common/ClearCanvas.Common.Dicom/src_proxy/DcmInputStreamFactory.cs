/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 1.3.25
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace ClearCanvas.Common.Dicom {

using System;
using System.Text;
using System.Runtime.InteropServices;

public class DcmInputStreamFactory : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DcmInputStreamFactory(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DcmInputStreamFactory obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DcmInputStreamFactory() {
    Dispose();
  }

  public virtual void Dispose() {
    if(swigCPtr.Handle != IntPtr.Zero && swigCMemOwn) {
      swigCMemOwn = false;
      DCMTKPINVOKE.delete_DcmInputStreamFactory(swigCPtr);
    }
    swigCPtr = new HandleRef(null, IntPtr.Zero);
    GC.SuppressFinalize(this);
  }

  public virtual DcmInputStream create() {
    IntPtr cPtr = DCMTKPINVOKE.DcmInputStreamFactory_create(swigCPtr);
    DcmInputStream ret = (cPtr == IntPtr.Zero) ? null : new DcmInputStream(cPtr, false);
    return ret;
  }

  public virtual DcmInputStreamFactory clone() {
    IntPtr cPtr = DCMTKPINVOKE.DcmInputStreamFactory_clone(swigCPtr);
    DcmInputStreamFactory ret = (cPtr == IntPtr.Zero) ? null : new DcmInputStreamFactory(cPtr, false);
    return ret;
  }

}

}
