// control_robot.h : main header file for the CONTROL_ROBOT application
//

#if !defined(AFX_CONTROL_ROBOT_H__BCF3FD52_A01D_4B47_B644_770233BE79E0__INCLUDED_)
#define AFX_CONTROL_ROBOT_H__BCF3FD52_A01D_4B47_B644_770233BE79E0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CControl_robotApp:
// See control_robot.cpp for the implementation of this class
//

class CControl_robotApp : public CWinApp
{
public:
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	CControl_robotApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CControl_robotApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CControl_robotApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONTROL_ROBOT_H__BCF3FD52_A01D_4B47_B644_770233BE79E0__INCLUDED_)
