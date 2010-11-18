// control_robotView.h : interface of the CControl_robotView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_CONTROL_ROBOTVIEW_H__3B2D3917_439B_4891_832F_6DFE4D71605B__INCLUDED_)
#define AFX_CONTROL_ROBOTVIEW_H__3B2D3917_439B_4891_832F_6DFE4D71605B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "SerialPort.h"
#include "afxwin.h"
#include "faza1.h"
#include "faza3.h"
//#include "e:\visualstudio2003\vc7\atlmfc\include\afxwin.h"
//#include "e:\Program Files\Microsoft Visual Studio .NET 2003\vc7\atlmfc\include\afxwin.h"
//#include "e:\Program Files\Microsoft Visual Studio .NET 2003\vc7\atlmfc\include\afxcmn.h"


class CControl_robotView : public CFormView
{
protected: // create from serialization only
	CControl_robotView();
	DECLARE_DYNCREATE(CControl_robotView)

public:
	CSerialPort  port;
	faza1 gogo;

	//////////////////////////////////////////////////////////////////CSerialDlg(CWnd* pParent = NULL);
	//{{AFX_DATA(CControl_robotView)
	enum{ IDD = IDD_CONTROL_ROBOT_FORM };
	BYTE	m_servo_prikaz;
	BYTE    m_servo0;
	BYTE    m_servo1;
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

// Attributes
public:
	CControl_robotDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CControl_robotView)
	public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual void OnInitialUpdate(); // called first time after construct
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnPrint(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CControl_robotView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CControl_robotView)
	afx_msg void OnButton_ok_kamil_defined();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
	//int kajo;
public:
	afx_msg void OnExit();
	afx_msg void OnStarttime();
	CString m_sTime;
	afx_msg void OnTimer(UINT nIDEvent);
    afx_msg void OnTimer2(UINT nIDEvent);
	CString m_sCount;
	int m_iInterval;
	CButton m_cStartTime;
	CButton m_cStopTime;
    afx_msg void OnEnChangeInterval();
	afx_msg void OnStoptimer();
	double m_krok_vyska;        //b elipsy
	double m_krok_dlzka;        //a elipsy
	double m_uhol_otocenie;
	int m_servo0_vysledok;
	int m_servo1_vysledok;
	int m_servo2_vysledok;
	int m_servo3_vysledok;
	int m_servo4_vysledok;
	int m_servo5_vysledok;
	int m_servo6_vysledok;
	int m_servo7_vysledok;
	int m_servo8_vysledok;
	int m_servo9_vysledok;
	int m_servo10_vysledok;
	int m_servo11_vysledok;
    int m_kroky_pocet;
	int m_kroky_status;
	int m_otoc_status;
	int m_kod_kroku;
	int m_pocet_pootoceni;
	double m_stabilita;
	int cx;

	double m_beta;

	double m_s0_alfa;
	double m_s1_gama;

	double m_x;
	double m_y;

/////hodnota tetivo nohy///////
	double m_c;
    double m_c2;
	double m_c3; //no use
	double m_c4; //no use

	double m_alfa;
    double m_alfa2;
	double m_alfa3;
    double m_alfa4;

//////Vysledne hodnoty pre servo//////
	double m_gama_v;
    double m_gama_v2;
	double m_gama_v3;
    double m_gama_v4;
///////Vysledne hodnoty pre servo///
	double m_alfa_v;
	double m_alfa_v2;
    double m_alfa_v3;
	double m_alfa_v4;

///////ukladacie hodnoty konecnych stavov noh////////
	double m_alfa_s;
	double m_alfa_s1;
    double m_alfa_s3;
    double m_alfa_s4;

	double m_gama_s;
    double m_gama_s1;
    double m_gama_s3; //no use
    double m_gama_s4; //no use

	double m_gama;
	double m_gama2;
	double m_gama3;
	double m_gama4;

	double x;
	double y;

/////hodnoty pre otacanie/////////
	double m_otoco;
	double m_otoco1;
	double m_otoco2;
	double m_otoco3;
    double m_otoco4;
	double m_otoco5;
	double m_otoco6;
	double m_otoco7;
	double m_otoco8;
	double m_otoco9;
	double m_otoco10;
	double m_otoco11;
	double m_otoco12;


//////////hodnoty faz/////////////
	double m_faza1;
	double m_faza2;
	double m_faza3;
	double m_faza4;
	double m_faza5;
	double m_faza6;

private:
	int m_iCount;
	int m_itest;

	
public:
	afx_msg void OnEnChangeKrokDlzka();
	afx_msg void OnEnChangeKrokVyska();
	afx_msg void OnEnChangeEdit16();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnEnChangeEdit12();
	CButton m_otoc;
	afx_msg void OnEnMaxtextEdit16();
//	afx_msg void OnBnClickedRadio1();
	afx_msg void OnBnClickedButton3();
	afx_msg void OnEnChangeRichedit22();
	afx_msg void OnEnChangeKrokyPocet();
	afx_msg void OnEnChangeEdit17();	
	
	double m_vyska1;
	double m_vyska2;
	double x1;
	
	afx_msg void OnEnChangeEdit18();
	int m_alg_1;
	afx_msg void OnEnChangeEdit19();
	int m_alg_2;
	int m_alg_3;
	int m_alg_4;
	int m_alg_5;
	int m_alg_6;
	int m_alg_7;
	int m_alg_8;
	afx_msg void OnEnChangeEdit20();
	afx_msg void OnEnChangeEdit21();
	afx_msg void OnEnChangeEdit22();
	afx_msg void OnEnChangeEdit23();
	afx_msg void OnEnChangeEdit24();
	afx_msg void OnEnChangeEdit25();
	afx_msg void OnEnChangeEdit26();
	
	afx_msg void OnBnClickedButton8();
	int m_combo1;
	int m_combo2;	
	int m_combo3;
	int m_combo4;
	int m_combo5;
	int m_combo6;
	int m_combo7;
	int m_combo8;
	afx_msg void OnCbnSelchangeCombo1();
	afx_msg void OnCbnSelchangeCombo2();
	afx_msg void OnCbnSelchangeCombo3();
	afx_msg void OnCbnSelchangeCombo4();
	afx_msg void OnCbnSelchangeCombo5();
	afx_msg void OnCbnSelchangeCombo6();
	afx_msg void OnCbnSelchangeCombo7();
	afx_msg void OnCbnSelchangeCombo8();
	CProgressCtrl m_running;
	bool m_rot1;
	bool m_rot2;
	bool m_dozadu;
	afx_msg void OnBnClickedRadio2();
	afx_msg void OnBnClickedRadio1();
	afx_msg void OnBnClickedRadio3();
	afx_msg void OnBnClickedRadio4();
	char sx0;
	char sx1;
	char sx2;
	char sx3;
	char sx4;
	char sx5;
	char sx6;
	char sx7;
	char sx8;
	char sx9;
	char sx10;
	char sx11;


};

#ifndef _DEBUG  // debug version in control_robotView.cpp
inline CControl_robotDoc* CControl_robotView::GetDocument()
   { return (CControl_robotDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONTROL_ROBOTVIEW_H__3B2D3917_439B_4891_832F_6DFE4D71605B__INCLUDED_)
