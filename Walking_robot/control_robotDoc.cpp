// control_robotDoc.cpp : implementation of the CControl_robotDoc class
//

#include "stdafx.h"
#include "control_robot.h"

#include "control_robotDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CControl_robotDoc

IMPLEMENT_DYNCREATE(CControl_robotDoc, CDocument)

BEGIN_MESSAGE_MAP(CControl_robotDoc, CDocument)
	//{{AFX_MSG_MAP(CControl_robotDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CControl_robotDoc construction/destruction

CControl_robotDoc::CControl_robotDoc()
{
	// TODO: add one-time construction code here

}

CControl_robotDoc::~CControl_robotDoc()
{
}

BOOL CControl_robotDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CControl_robotDoc serialization

void CControl_robotDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CControl_robotDoc diagnostics

#ifdef _DEBUG
void CControl_robotDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CControl_robotDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CControl_robotDoc commands
