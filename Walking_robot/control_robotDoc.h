// control_robotDoc.h : interface of the CControl_robotDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_CONTROL_ROBOTDOC_H__2D23E13A_066E_477E_B273_6C3F65B54C4F__INCLUDED_)
#define AFX_CONTROL_ROBOTDOC_H__2D23E13A_066E_477E_B273_6C3F65B54C4F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CControl_robotDoc : public CDocument
{
protected: // create from serialization only
	CControl_robotDoc();
	DECLARE_DYNCREATE(CControl_robotDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CControl_robotDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CControl_robotDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CControl_robotDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONTROL_ROBOTDOC_H__2D23E13A_066E_477E_B273_6C3F65B54C4F__INCLUDED_)
