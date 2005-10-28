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

public class DcmUnsignedLong : DcmElement {
  private HandleRef swigCPtr;

  internal DcmUnsignedLong(IntPtr cPtr, bool cMemoryOwn) : base(DCMTKPINVOKE.DcmUnsignedLongUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DcmUnsignedLong obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DcmUnsignedLong() {
    Dispose();
  }

  public override void Dispose() {
    if(swigCPtr.Handle != IntPtr.Zero && swigCMemOwn) {
      swigCMemOwn = false;
      DCMTKPINVOKE.delete_DcmUnsignedLong(swigCPtr);
    }
    swigCPtr = new HandleRef(null, IntPtr.Zero);
    GC.SuppressFinalize(this);
    base.Dispose();
  }

  public DcmUnsignedLong(DcmTag tag, uint len) : this(DCMTKPINVOKE.new_DcmUnsignedLong__SWIG_0(DcmTag.getCPtr(tag), len), true) {
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DcmUnsignedLong(DcmTag tag) : this(DCMTKPINVOKE.new_DcmUnsignedLong__SWIG_1(DcmTag.getCPtr(tag)), true) {
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DcmUnsignedLong(DcmUnsignedLong old) : this(DCMTKPINVOKE.new_DcmUnsignedLong__SWIG_2(DcmUnsignedLong.getCPtr(old)), true) {
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override DcmEVR ident() {
    DcmEVR ret = (DcmEVR)DCMTKPINVOKE.DcmUnsignedLong_ident(swigCPtr);
    return ret;
  }

  public override uint getVM() {
    uint ret = DCMTKPINVOKE.DcmUnsignedLong_getVM(swigCPtr);
    return ret;
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level, string pixelFileName, SWIGTYPE_p_size_t pixelCounter) {
    DCMTKPINVOKE.DcmUnsignedLong_print__SWIG_0(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level, pixelFileName, SWIGTYPE_p_size_t.getCPtr(pixelCounter));
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level, string pixelFileName) {
    DCMTKPINVOKE.DcmUnsignedLong_print__SWIG_1(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level, pixelFileName);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level) {
    DCMTKPINVOKE.DcmUnsignedLong_print__SWIG_2(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags) {
    DCMTKPINVOKE.DcmUnsignedLong_print__SWIG_3(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream) {
    DCMTKPINVOKE.DcmUnsignedLong_print__SWIG_4(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream));
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override OFCondition getUint32(out uint uintVal, uint pos) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_getUint32__SWIG_0(swigCPtr, out uintVal, pos), true);
    return ret;
  }

  public override OFCondition getUint32(out uint uintVal) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_getUint32__SWIG_1(swigCPtr, out uintVal), true);
    return ret;
  }

  public override OFCondition getUint32Array(ref IntPtr uintVals) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_getUint32Array(swigCPtr, ref uintVals), true);
    return ret;
  }

  public override OFCondition getOFString(StringBuilder stringVal, uint pos, bool normalize) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_getOFString__SWIG_0(swigCPtr, stringVal, pos, normalize), true);
    return ret;
  }

  public override OFCondition getOFString(StringBuilder stringVal, uint pos) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_getOFString__SWIG_1(swigCPtr, stringVal, pos), true);
    return ret;
  }

  public override OFCondition putUint32(uint uintVal, uint pos) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_putUint32__SWIG_0(swigCPtr, uintVal, pos), true);
    return ret;
  }

  public override OFCondition putUint32(uint uintVal) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_putUint32__SWIG_1(swigCPtr, uintVal), true);
    return ret;
  }

  public override OFCondition putUint32Array(uint[] uintVals, uint numUints) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_putUint32Array(swigCPtr, uintVals, numUints), true);
    return ret;
  }

  public override OFCondition putString(string stringVal) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_putString(swigCPtr, stringVal), true);
    return ret;
  }

  public override OFCondition verify(bool autocorrect) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_verify__SWIG_0(swigCPtr, autocorrect), true);
    return ret;
  }

  public override OFCondition verify() {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmUnsignedLong_verify__SWIG_1(swigCPtr), true);
    return ret;
  }

}

}
