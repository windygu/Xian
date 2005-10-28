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

public class DcmFileFormat : DcmSequenceOfItems {
  private HandleRef swigCPtr;

  internal DcmFileFormat(IntPtr cPtr, bool cMemoryOwn) : base(DCMTKPINVOKE.DcmFileFormatUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DcmFileFormat obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DcmFileFormat() {
    Dispose();
  }

  public override void Dispose() {
    if(swigCPtr.Handle != IntPtr.Zero && swigCMemOwn) {
      swigCMemOwn = false;
      DCMTKPINVOKE.delete_DcmFileFormat(swigCPtr);
    }
    swigCPtr = new HandleRef(null, IntPtr.Zero);
    GC.SuppressFinalize(this);
    base.Dispose();
  }

  public DcmFileFormat() : this(DCMTKPINVOKE.new_DcmFileFormat__SWIG_0(), true) {
  }

  public DcmFileFormat(DcmDataset dataset) : this(DCMTKPINVOKE.new_DcmFileFormat__SWIG_1(DcmDataset.getCPtr(dataset)), true) {
  }

  public DcmFileFormat(DcmFileFormat old) : this(DCMTKPINVOKE.new_DcmFileFormat__SWIG_2(DcmFileFormat.getCPtr(old)), true) {
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override DcmEVR ident() {
    DcmEVR ret = (DcmEVR)DCMTKPINVOKE.DcmFileFormat_ident(swigCPtr);
    return ret;
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level, string pixelFileName, SWIGTYPE_p_size_t pixelCounter) {
    DCMTKPINVOKE.DcmFileFormat_print__SWIG_0(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level, pixelFileName, SWIGTYPE_p_size_t.getCPtr(pixelCounter));
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level, string pixelFileName) {
    DCMTKPINVOKE.DcmFileFormat_print__SWIG_1(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level, pixelFileName);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags, int level) {
    DCMTKPINVOKE.DcmFileFormat_print__SWIG_2(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags, level);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream, uint flags) {
    DCMTKPINVOKE.DcmFileFormat_print__SWIG_3(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void print(SWIGTYPE_p_ostream outStream) {
    DCMTKPINVOKE.DcmFileFormat_print__SWIG_4(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream));
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual OFCondition validateMetaInfo(E_TransferSyntax oxfer) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_validateMetaInfo(swigCPtr, (int)oxfer), true);
    return ret;
  }

  public DcmMetaInfo getMetaInfo() {
    IntPtr cPtr = DCMTKPINVOKE.DcmFileFormat_getMetaInfo(swigCPtr);
    DcmMetaInfo ret = (cPtr == IntPtr.Zero) ? null : new DcmMetaInfo(cPtr, false);
    return ret;
  }

  public DcmDataset getDataset() {
    IntPtr cPtr = DCMTKPINVOKE.DcmFileFormat_getDataset(swigCPtr);
    DcmDataset ret = (cPtr == IntPtr.Zero) ? null : new DcmDataset(cPtr, false);
    return ret;
  }

  public DcmDataset getAndRemoveDataset() {
    IntPtr cPtr = DCMTKPINVOKE.DcmFileFormat_getAndRemoveDataset(swigCPtr);
    DcmDataset ret = (cPtr == IntPtr.Zero) ? null : new DcmDataset(cPtr, false);
    return ret;
  }

  public override uint calcElementLength(E_TransferSyntax xfer, E_EncodingType enctype) {
    uint ret = DCMTKPINVOKE.DcmFileFormat_calcElementLength(swigCPtr, (int)xfer, (int)enctype);
    return ret;
  }

  public override bool canWriteXfer(E_TransferSyntax newXfer, E_TransferSyntax oldXfer) {
    bool ret = DCMTKPINVOKE.DcmFileFormat_canWriteXfer__SWIG_0(swigCPtr, (int)newXfer, (int)oldXfer);
    return ret;
  }

  public virtual bool canWriteXfer(E_TransferSyntax newXfer) {
    bool ret = DCMTKPINVOKE.DcmFileFormat_canWriteXfer__SWIG_1(swigCPtr, (int)newXfer);
    return ret;
  }

  public override OFCondition read(DcmInputStream inStream, E_TransferSyntax xfer, E_GrpLenEncoding glenc, uint maxReadLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_read__SWIG_0(swigCPtr, DcmInputStream.getCPtr(inStream), (int)xfer, (int)glenc, maxReadLength), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition read(DcmInputStream inStream, E_TransferSyntax xfer, E_GrpLenEncoding glenc) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_read__SWIG_1(swigCPtr, DcmInputStream.getCPtr(inStream), (int)xfer, (int)glenc), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition read(DcmInputStream inStream, E_TransferSyntax xfer) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_read__SWIG_2(swigCPtr, DcmInputStream.getCPtr(inStream), (int)xfer), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition read(DcmInputStream inStream) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_read__SWIG_3(swigCPtr, DcmInputStream.getCPtr(inStream)), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_0(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_1(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype, E_GrpLenEncoding glenc, E_PaddingEncoding padenc, uint padlen, uint subPadlen, uint instanceLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_2(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype, (int)glenc, (int)padenc, padlen, subPadlen, instanceLength), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype, E_GrpLenEncoding glenc, E_PaddingEncoding padenc, uint padlen, uint subPadlen) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_3(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype, (int)glenc, (int)padenc, padlen, subPadlen), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype, E_GrpLenEncoding glenc, E_PaddingEncoding padenc, uint padlen) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_4(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype, (int)glenc, (int)padenc, padlen), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype, E_GrpLenEncoding glenc, E_PaddingEncoding padenc) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_5(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype, (int)glenc, (int)padenc), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition write(DcmOutputStream outStream, E_TransferSyntax oxfer, E_EncodingType enctype, E_GrpLenEncoding glenc) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_write__SWIG_6(swigCPtr, DcmOutputStream.getCPtr(outStream), (int)oxfer, (int)enctype, (int)glenc), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition writeXML(SWIGTYPE_p_ostream outStream, uint flags) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_writeXML__SWIG_0(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream), flags), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override OFCondition writeXML(SWIGTYPE_p_ostream outStream) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_writeXML__SWIG_1(swigCPtr, SWIGTYPE_p_ostream.getCPtr(outStream)), true);
    if (DCMTKPINVOKE.SWIGPendingException.Pending) throw DCMTKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual OFCondition loadFile(string fileName, E_TransferSyntax readXfer, E_GrpLenEncoding groupLength, uint maxReadLength, bool isDataset) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_loadFile__SWIG_0(swigCPtr, fileName, (int)readXfer, (int)groupLength, maxReadLength, isDataset), true);
    return ret;
  }

  public virtual OFCondition loadFile(string fileName, E_TransferSyntax readXfer, E_GrpLenEncoding groupLength, uint maxReadLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_loadFile__SWIG_1(swigCPtr, fileName, (int)readXfer, (int)groupLength, maxReadLength), true);
    return ret;
  }

  public virtual OFCondition loadFile(string fileName, E_TransferSyntax readXfer, E_GrpLenEncoding groupLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_loadFile__SWIG_2(swigCPtr, fileName, (int)readXfer, (int)groupLength), true);
    return ret;
  }

  public virtual OFCondition loadFile(string fileName, E_TransferSyntax readXfer) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_loadFile__SWIG_3(swigCPtr, fileName, (int)readXfer), true);
    return ret;
  }

  public virtual OFCondition loadFile(string fileName) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_loadFile__SWIG_4(swigCPtr, fileName), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType, E_GrpLenEncoding groupLength, E_PaddingEncoding padEncoding, uint padLength, uint subPadLength, bool isDataset) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_0(swigCPtr, fileName, (int)writeXfer, (int)encodingType, (int)groupLength, (int)padEncoding, padLength, subPadLength, isDataset), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType, E_GrpLenEncoding groupLength, E_PaddingEncoding padEncoding, uint padLength, uint subPadLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_1(swigCPtr, fileName, (int)writeXfer, (int)encodingType, (int)groupLength, (int)padEncoding, padLength, subPadLength), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType, E_GrpLenEncoding groupLength, E_PaddingEncoding padEncoding, uint padLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_2(swigCPtr, fileName, (int)writeXfer, (int)encodingType, (int)groupLength, (int)padEncoding, padLength), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType, E_GrpLenEncoding groupLength, E_PaddingEncoding padEncoding) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_3(swigCPtr, fileName, (int)writeXfer, (int)encodingType, (int)groupLength, (int)padEncoding), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType, E_GrpLenEncoding groupLength) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_4(swigCPtr, fileName, (int)writeXfer, (int)encodingType, (int)groupLength), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer, E_EncodingType encodingType) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_5(swigCPtr, fileName, (int)writeXfer, (int)encodingType), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName, E_TransferSyntax writeXfer) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_6(swigCPtr, fileName, (int)writeXfer), true);
    return ret;
  }

  public virtual OFCondition saveFile(string fileName) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_saveFile__SWIG_7(swigCPtr, fileName), true);
    return ret;
  }

  public OFCondition chooseRepresentation(E_TransferSyntax repType, DcmRepresentationParameter repParam) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_chooseRepresentation(swigCPtr, (int)repType, DcmRepresentationParameter.getCPtr(repParam)), true);
    return ret;
  }

  public bool hasRepresentation(E_TransferSyntax repType, DcmRepresentationParameter repParam) {
    bool ret = DCMTKPINVOKE.DcmFileFormat_hasRepresentation(swigCPtr, (int)repType, DcmRepresentationParameter.getCPtr(repParam));
    return ret;
  }

  public void removeAllButOriginalRepresentations() {
    DCMTKPINVOKE.DcmFileFormat_removeAllButOriginalRepresentations(swigCPtr);
  }

  public void removeAllButCurrentRepresentations() {
    DCMTKPINVOKE.DcmFileFormat_removeAllButCurrentRepresentations(swigCPtr);
  }

  public virtual OFCondition insertItem(DcmItem item, uint where) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_insertItem__SWIG_0(swigCPtr, DcmItem.getCPtr(item), where), true);
    return ret;
  }

  public virtual OFCondition insertItem(DcmItem item) {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_insertItem__SWIG_1(swigCPtr, DcmItem.getCPtr(item)), true);
    return ret;
  }

  public override DcmItem remove(uint num) {
    IntPtr cPtr = DCMTKPINVOKE.DcmFileFormat_remove__SWIG_0(swigCPtr, num);
    DcmItem ret = (cPtr == IntPtr.Zero) ? null : new DcmItem(cPtr, false);
    return ret;
  }

  public override DcmItem remove(DcmItem item) {
    IntPtr cPtr = DCMTKPINVOKE.DcmFileFormat_remove__SWIG_1(swigCPtr, DcmItem.getCPtr(item));
    DcmItem ret = (cPtr == IntPtr.Zero) ? null : new DcmItem(cPtr, false);
    return ret;
  }

  public override OFCondition clear() {
    OFCondition ret = new OFCondition(DCMTKPINVOKE.DcmFileFormat_clear(swigCPtr), true);
    return ret;
  }

}

}
