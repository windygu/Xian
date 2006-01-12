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

public class T_DIMSE_C_MoveRSP : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal T_DIMSE_C_MoveRSP(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(T_DIMSE_C_MoveRSP obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~T_DIMSE_C_MoveRSP() {
    Dispose();
  }

  public virtual void Dispose() {
    if(swigCPtr.Handle != IntPtr.Zero && swigCMemOwn) {
      swigCMemOwn = false;
      DCMTKPINVOKE.delete_T_DIMSE_C_MoveRSP(swigCPtr);
    }
    swigCPtr = new HandleRef(null, IntPtr.Zero);
    GC.SuppressFinalize(this);
  }

  public ushort MessageIDBeingRespondedTo {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_MessageIDBeingRespondedTo(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_MessageIDBeingRespondedTo(swigCPtr);
      return ret;
    } 
  }

  public string AffectedSOPClassUID {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_AffectedSOPClassUID(swigCPtr, value);
    } 
    get {
      string ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_AffectedSOPClassUID(swigCPtr);
      return ret;
    } 
  }

  public T_DIMSE_DataSetType DataSetType {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_DataSetType(swigCPtr, (int)value);
    } 
    get {
      T_DIMSE_DataSetType ret = (T_DIMSE_DataSetType)DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_DataSetType(swigCPtr);
      return ret;
    } 
  }

  public ushort DimseStatus {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_DimseStatus(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_DimseStatus(swigCPtr);
      return ret;
    } 
  }

  public ushort NumberOfRemainingSubOperations {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_NumberOfRemainingSubOperations(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_NumberOfRemainingSubOperations(swigCPtr);
      return ret;
    } 
  }

  public ushort NumberOfCompletedSubOperations {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_NumberOfCompletedSubOperations(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_NumberOfCompletedSubOperations(swigCPtr);
      return ret;
    } 
  }

  public ushort NumberOfFailedSubOperations {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_NumberOfFailedSubOperations(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_NumberOfFailedSubOperations(swigCPtr);
      return ret;
    } 
  }

  public ushort NumberOfWarningSubOperations {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_NumberOfWarningSubOperations(swigCPtr, value);
    } 
    get {
      ushort ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_NumberOfWarningSubOperations(swigCPtr);
      return ret;
    } 
  }

  public uint opts {
    set {
      DCMTKPINVOKE.set_T_DIMSE_C_MoveRSP_opts(swigCPtr, value);
    } 
    get {
      uint ret = DCMTKPINVOKE.get_T_DIMSE_C_MoveRSP_opts(swigCPtr);
      return ret;
    } 
  }

  public T_DIMSE_C_MoveRSP() : this(DCMTKPINVOKE.new_T_DIMSE_C_MoveRSP(), true) {
  }

}

}
