// control_robotView.cpp : implementation of the CControl_robotView class
//

#include "stdafx.h"
#include "control_robot.h"
#include <math.h>

#include "control_robotDoc.h"
#include "control_robotView.h"
#include "serialport.h"
#include ".\control_robotview.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CControl_robotView

IMPLEMENT_DYNCREATE(CControl_robotView, CFormView)

BEGIN_MESSAGE_MAP(CControl_robotView, CFormView)
	//{{AFX_MSG_MAP(CControl_robotView)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton_ok_kamil_defined)
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CFormView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CFormView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CFormView::OnFilePrintPreview)
	ON_BN_CLICKED(IDC_EXIT, OnExit)
	ON_BN_CLICKED(IDC_STARTTIME, OnStarttime)
	ON_WM_TIMER()
	ON_EN_CHANGE(IDC_INTERVAL, OnEnChangeInterval)
	ON_BN_CLICKED(IDC_STOPTIMER, OnStoptimer)
	ON_EN_CHANGE(IDC_KROK_DLZKA, OnEnChangeKrokDlzka)
	ON_EN_CHANGE(IDC_KROK_VYSKA, OnEnChangeKrokVyska)
	ON_EN_CHANGE(IDC_EDIT16, OnEnChangeEdit16)
	ON_BN_CLICKED(IDC_BUTTON2, OnBnClickedButton2)
	ON_EN_CHANGE(IDC_EDIT12, OnEnChangeEdit12)
//	ON_BN_CLICKED(IDC_RADIO1, OnBnClickedRadio1)
	ON_BN_CLICKED(IDC_BUTTON3, OnBnClickedButton3)
	ON_EN_CHANGE(IDC_RICHEDIT22, OnEnChangeRichedit22)
	ON_EN_CHANGE(IDC_KROKY_POCET, OnEnChangeKrokyPocet)
	ON_EN_CHANGE(IDC_EDIT17, OnEnChangeEdit17)
	ON_EN_CHANGE(IDC_EDIT18, OnEnChangeEdit18)
	ON_EN_CHANGE(IDC_EDIT19, OnEnChangeEdit19)
	ON_EN_CHANGE(IDC_EDIT20, OnEnChangeEdit20)
	ON_EN_CHANGE(IDC_EDIT21, OnEnChangeEdit21)
	ON_EN_CHANGE(IDC_EDIT22, OnEnChangeEdit22)
	ON_EN_CHANGE(IDC_EDIT23, OnEnChangeEdit23)
	ON_EN_CHANGE(IDC_EDIT24, OnEnChangeEdit24)
	ON_EN_CHANGE(IDC_EDIT25, OnEnChangeEdit25)
	ON_EN_CHANGE(IDC_EDIT26, OnEnChangeEdit26)
	ON_BN_CLICKED(IDC_BUTTON8, OnBnClickedButton8)
	ON_CBN_SELCHANGE(IDC_COMBO1, OnCbnSelchangeCombo1)
	ON_CBN_SELCHANGE(IDC_COMBO2, OnCbnSelchangeCombo2)
	ON_CBN_SELCHANGE(IDC_COMBO3, OnCbnSelchangeCombo3)
	ON_CBN_SELCHANGE(IDC_COMBO4, OnCbnSelchangeCombo4)
	ON_CBN_SELCHANGE(IDC_COMBO5, OnCbnSelchangeCombo5)
	ON_CBN_SELCHANGE(IDC_COMBO6, OnCbnSelchangeCombo6)
	ON_CBN_SELCHANGE(IDC_COMBO7, OnCbnSelchangeCombo7)
	ON_CBN_SELCHANGE(IDC_COMBO8, OnCbnSelchangeCombo8)
	ON_BN_CLICKED(IDC_RADIO2, OnBnClickedRadio2)
	ON_BN_CLICKED(IDC_RADIO1, OnBnClickedRadio1)
	ON_BN_CLICKED(IDC_RADIO3, OnBnClickedRadio3)
	ON_BN_CLICKED(IDC_RADIO4, OnBnClickedRadio4)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CControl_robotView construction/destruction

CControl_robotView::CControl_robotView()
	: CFormView(CControl_robotView::IDD)
	, m_sTime(_T(""))
	, m_sCount(_T(""))
	, m_iInterval(0)
	, m_iCount(0)
	, m_krok_vyska(0)
	, m_krok_dlzka(0)
	, m_servo0_vysledok(0)
	, m_servo1_vysledok(0)
	, m_servo2_vysledok(0)
	, m_servo3_vysledok(0)
	, m_servo4_vysledok(0)
	, m_servo5_vysledok(0)
	, m_servo6_vysledok(0)
	, m_servo7_vysledok(0)
	, m_servo8_vysledok(0)
	, m_servo9_vysledok(0)
	, m_servo10_vysledok(0)
	, m_servo11_vysledok(0)
    , m_uhol_otocenie(0)
	, m_s0_alfa(0)
	, m_s1_gama(0)
	, m_x(0)
	, m_y(0)
	, m_c(0)
	, m_alfa(0)
	, m_gama_v(0)
	, m_alfa_v(0)
	, m_alfa_s(0)
	, m_gama_s(0)
	, m_alfa_s1(0)
	, m_gama(0)
	, x(0)
	, y(0)
	, m_kroky_pocet(0)
	, m_pocet_pootoceni(0)
	, m_stabilita(0)
	, m_beta(0)
	, m_vyska1(0)
	, m_vyska2(0)	
	, m_alg_1(1)
	, m_alg_2(1)
	, m_alg_3(1)
	, m_alg_4(1)
	, m_alg_5(1)
	, m_alg_6(1)
	, m_alg_7(1)
	, m_alg_8(1)
	, m_combo1(0)
	, m_combo2(0)
	, m_combo3(0)
	, m_combo4(0)
	, m_combo5(0)
	, m_combo6(0)
	, m_combo7(0)
	, m_combo8(0)
	, m_rot1(true)
	, m_rot2(false)
	, m_dozadu(false)
	, sx0(0)
	, sx1(0)
	, sx2(0)
	, sx3(0)
	, sx4(0)
	, sx5(0)
	, sx6(0)
	, sx7(0)
	, sx8(0)
	, sx9(0)
	, sx10(0)
	, sx11(0)
	{
	//{{AFX_DATA_INIT(CControl_robotView)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// TODO: add construction code here
//public:
//    CSerialPort  port;

}

CControl_robotView::~CControl_robotView()
{
}

void CControl_robotView::DoDataExchange(CDataExchange* pDX)
{
	CFormView::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CControl_robotView)
	// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
	DDX_Text(pDX, IDC_STATICTIME, m_sTime);
	DDX_Text(pDX, IDC_STATICCOUNT, m_sCount);
	DDX_Text(pDX, IDC_INTERVAL, m_iInterval);
	DDX_Text(pDX, IDC_KROK_VYSKA, m_krok_vyska);
	DDX_Text(pDX, IDC_KROK_DLZKA, m_krok_dlzka);
	DDX_Control(pDX, IDC_STARTTIME, m_cStartTime);
	DDX_Control(pDX, IDC_STOPTIMER, m_cStopTime);
	DDV_MinMaxInt(pDX, m_iInterval, 1, 100000);
	DDX_Text(pDX, IDC_EDIT4, m_servo0_vysledok);
	DDX_Text(pDX, IDC_EDIT5, m_servo1_vysledok);
	DDX_Text(pDX, IDC_EDIT6, m_servo2_vysledok);
	DDX_Text(pDX, IDC_EDIT7, m_servo3_vysledok);
	DDX_Text(pDX, IDC_EDIT8, m_servo4_vysledok);
	DDX_Text(pDX, IDC_EDIT9, m_servo5_vysledok);
	DDX_Text(pDX, IDC_EDIT10, m_servo6_vysledok);
	DDX_Text(pDX, IDC_EDIT11, m_servo7_vysledok);
	DDX_Text(pDX, IDC_EDIT12, m_servo8_vysledok);
	DDX_Text(pDX, IDC_EDIT13, m_servo9_vysledok);
	DDX_Text(pDX, IDC_EDIT14, m_servo10_vysledok);
	DDX_Text(pDX, IDC_EDIT15, m_servo11_vysledok);
	DDX_Text(pDX, IDC_EDIT16, m_uhol_otocenie);
	DDV_MinMaxDouble(pDX, m_krok_vyska, 0, 50);
	DDV_MinMaxDouble(pDX, m_krok_dlzka, 0, 50);	
	DDX_Control(pDX, IDC_BUTTON2, m_otoc);
	DDX_Text(pDX, IDC_KROKY_POCET, m_kroky_pocet);
	DDX_Text(pDX, IDC_EDIT17, m_pocet_pootoceni);
	DDX_Text(pDX, IDC_EDIT18, m_stabilita);
	DDX_Text(pDX, IDC_EDIT19, m_alg_1);
	DDX_Text(pDX, IDC_EDIT20, m_alg_2);
	DDX_Text(pDX, IDC_EDIT21, m_alg_3);
	DDX_Text(pDX, IDC_EDIT22, m_alg_4);
	DDX_Text(pDX, IDC_EDIT23, m_alg_5);
	DDX_Text(pDX, IDC_EDIT24, m_alg_6);
	DDX_Text(pDX, IDC_EDIT25, m_alg_7);
	DDX_Text(pDX, IDC_EDIT26, m_alg_8);
	DDX_CBIndex(pDX, IDC_COMBO1, m_combo1);
	DDX_CBIndex(pDX, IDC_COMBO2, m_combo2);
	DDX_CBIndex(pDX, IDC_COMBO3, m_combo3);
	DDX_CBIndex(pDX, IDC_COMBO4, m_combo4);
	DDX_CBIndex(pDX, IDC_COMBO5, m_combo5);
	DDX_CBIndex(pDX, IDC_COMBO6, m_combo6);
	DDX_CBIndex(pDX, IDC_COMBO7, m_combo7);
	DDX_CBIndex(pDX, IDC_COMBO8, m_combo8);
	DDX_Control(pDX, IDC_PROGRESS1, m_running);
}

BOOL CControl_robotView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CFormView::PreCreateWindow(cs);
}

void CControl_robotView::OnInitialUpdate()
{
	CFormView::OnInitialUpdate();
	GetParentFrame()->RecalcLayout();
	ResizeParentToFit();
SetTimer(ID_CLOCK_TIMER, 1000, NULL);

 
//Initialize number of steps
     m_kroky_pocet = 3;
	 //m_otoc_status=255;
	 m_pocet_pootoceni=3;
   // Update the dialog
   UpdateData(FALSE);
     m_uhol_otocenie = 20;
   UpdateData(FALSE);
     m_krok_vyska = 32;
   UpdateData(FALSE);
     //UpdateData(false);
     m_krok_dlzka = 18;
   UpdateData(FALSE);
     // Initialize the counter interval
     m_iInterval = 50;
     // Update the dialog
   UpdateData(FALSE);
   //stabilita
     m_stabilita=26;
   UpdateData(FALSE);
}

/////////////////////////////////////////////////////////////////////////////
// CControl_robotView printing

BOOL CControl_robotView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CControl_robotView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CControl_robotView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CControl_robotView::OnPrint(CDC* pDC, CPrintInfo* /*pInfo*/)
{
	// TODO: add customized printing code here
}

/////////////////////////////////////////////////////////////////////////////
// CControl_robotView diagnostics

#ifdef _DEBUG
void CControl_robotView::AssertValid() const
{
	CFormView::AssertValid();
}

void CControl_robotView::Dump(CDumpContext& dc) const
{
	CFormView::Dump(dc);
}

CControl_robotDoc* CControl_robotView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CControl_robotDoc)));
	return (CControl_robotDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CControl_robotView message handlers

void CControl_robotView::OnButton_ok_kamil_defined() 
{

/*	if(m_cStopTime.IsWindowEnabled)
   {
   port.ClosePort();
   }
*/
	
	m_servo_prikaz=255;
	UpdateData(true);
	BYTE data=0;
	if(!(port.OpenPort("com1"))){
		MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	else{
		if(!(port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT ))){
			MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
		//else{
		//	if(!(port.SetCommunicationTimeouts(0,500,0,0,0))){
		//		MessageBox("Cannot Configure Communication Timeouts","Error",MB_OK+MB_ICONERROR);
		//	port.ClosePort();}
			else{
				if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
			port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT);	
				}
              else{
                if (!(port.WriteByte(0))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();				                         
				  }
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
			  else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(1))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
///////////////////////////////////////////////////////////////

			  else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(2))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
 ///////////////////////////////////////////////////////////////
				else{
				     if(!(port.WriteByte(255))){
						///MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(3))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
	                 
/////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(4))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
/////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(5))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(6))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
			 ////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
					 }
              else{
				if (!(port.WriteByte(7))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(127))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
			////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(8))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(127))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(9))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(127))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
		////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(10))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(127))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			////////////////////////////////////////////////////////////////
		
			 else{
				     if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(11))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(127))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			  
		  }
		  }
		  }

		  }
		  }
		  }	

		  }
		  }
		  }  

		  }
		  }
		  }  

		  }
		  }
		  }

		  }
		  }
		  }
		    }
			}
			}	  
			  }
			  }
			  }
			        }
					}
				    }
			           }
					   }
					   }
				           }
			               }
			               }
			                  }
			                  }
		                  	  }
			  		
}
MessageBox(TEXT("Synchronization of servos OK"), NULL, MB_OK);
port.ClosePort();
}
void CControl_robotView::OnExit()
{
	//gogo.back(NULL,NULL);
	//gogo.back(NULL,NULL);
	//gogo.back(NULL,NULL);'
	faza3::pukino();
	// TODO: Add your control notification handler code here
	//faza1::back(NULL,NULL);
	
}

void CControl_robotView::OnStarttime()
{
	// TODO: Add your control notification handler code here
      ///////////////////////
      // MY CODE STARTS HERE
      ///////////////////////
 
     // Update the variables
     UpdateData(TRUE);
 
	 ////////////////////////////////////////////////////
     // Initialize the count, nastavenie nulovej polohy
	 ////////////////////////////////////////////////////
     m_iCount = 0;
     m_x=0;
	 //m_stabilita=10;

     m_gama_s=102.75003; //nulovy uhol nohy gama
	 m_gama_s1=0;
	 m_alfa_s=51.3751;   //nulovy uhol nohy alfa
	 m_alfa_s1=128.6248;
     m_kroky_status=0;

	 m_faza1=1;
     //m_otoco=500;
	 m_faza2=255;
	 m_faza3=255;
	 m_faza4=255;
	 m_otoco=255;

		//else{
		//	if(!(port.SetCommunicationTimeouts(0,500,0,0,0))){
		//		MessageBox("Cannot Configure Communication Timeouts","Error",MB_OK+MB_ICONERROR);
		//	port.ClosePort();}
     // Format the count for displaying
     m_sCount.Format("%d", m_iCount);

     // Update the dialog
     UpdateData(FALSE);
     // Start the timer
     SetTimer(ID_COUNT_TIMER, m_iInterval, NULL);
 
     // Enable the Stop Timer button
     m_cStopTime.EnableWindow(TRUE);
     // Disable the Start Timer button
     m_cStartTime.EnableWindow(FALSE);
 		
 
     ///////////////////////
     // MY CODE ENDS HERE
     ///////////////////////


}
/////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////
///////////  program servo 0,1                  /////////////
///////////                                     /////////////
/////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
void CControl_robotView::OnTimer(UINT nIDEvent)
{
	// TODO: Add your message handler code here and/or call default
      ///////////////////////
     // MY CODE STARTS HERE
     ///////////////////////
 
      // Get the current time
     CTime curTime = CTime::GetCurrentTime();
 
     // Which timer triggered this event?
     switch (nIDEvent)
	 {
         // The clock timer?
     case ID_CLOCK_TIMER:
         // Display the current time
         m_sTime.Format("%d:%d:%d", curTime.GetHour(),
             curTime.GetMinute(),
             curTime.GetSecond());
         break;
         // The count timer?
     case ID_COUNT_TIMER:

/////////////////////////////////////////////////
// Increment the count, inkrementovanie vsetkeho
/////////////////////////////////////////////////
         
m_iCount++;
double pi = 3.1415926535;         

if(m_kroky_status==m_kroky_pocet)
{
  ///MessageBox(_T("Hello World"));
  KillTimer(ID_COUNT_TIMER);
 
     // Disable the Stop Timer button
    m_cStopTime.EnableWindow(FALSE);
     // Enable the Start Timer button
    m_cStartTime.EnableWindow(TRUE);
  
     
}

//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 1/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////
/*if(comboBox1->Items->Add(S"Tokyo");
)
{
MessageBox("nes",NULL,MB_OK);
}
*/

if(m_kroky_status<m_kroky_pocet)
{

if(m_dozadu==false)
{
sx0=0;
sx1=1;
sx2=4;
sx3=5;
sx4=6;
sx5=7;
sx6=2;
sx7=3;
sx8=8;
sx9=9;
sx10=10;
sx11=11;

}
if(m_dozadu==true)
{
sx0=6;
sx1=7;
sx2=2;
sx3=3;
sx4=0;
sx5=1;
sx6=4;
sx7=5;
sx8=8;
sx9=9;
sx10=10;
sx11=11;

}


if(m_faza1<=2*m_krok_dlzka)
{
////////////////////////////////////////////////////////////////////OOOOOOOOOK///ok
///////////////Lava Predna vypocet//////////////////////////////////OOOOOOOOOK
////////////////////////////////////////////////////////////////////OOOOOOOOOK
//uhol pre stabilitu
m_beta=(atan(m_stabilita/150))/(pi/180);
m_vyska1=120;
m_vyska2=120-m_stabilita;
x1=0;

//hodnota x pozicie na elipse
//m_x=0;
//hodnota y pozicie na elipse [1]ok!!!
m_y=sqrt(((m_krok_vyska*m_krok_vyska)-((((m_x-m_krok_dlzka)*(m_x-m_krok_dlzka)))*(((m_krok_vyska)*(m_krok_vyska))))/((m_krok_dlzka*m_krok_dlzka))));
//tetiva nohy[2]ok!!!
m_c=sqrt((m_vyska1-m_y)*(m_vyska1-m_y)+(m_x-m_x/4)*(m_x-m_x/4));
//uhol medzi dolnym klbom[3]ok!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok
m_alfa=-m_beta+(m_gama/2)+(acos((m_vyska1-m_y)/m_c))/(pi/180);

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s-m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s-m_beta+m_gama-m_gama_s+60)/0.47058;
//
UpdateData(true);

////////////////////////////////////////////////////////////////////OKKKKKKKKK////ok
///////////////Lava Zadna vypocet///////////////////////////////////OKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKK

//tetiva nohy[2]ok!!!
m_c2=sqrt(m_vyska2*m_vyska2+(m_x/4)*(m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa2=+m_beta+((asin(m_vyska2/m_c2))/(pi/180)+90-(m_gama2/2));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=(-m_alfa2+m_alfa_s1+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v2=(-m_alfa2+m_alfa_s1+m_beta-m_gama2+m_gama_s+60)/0.47058;

////////////////////////////////////////////////////////////////////OKKKKKKKKK///ok
///////////////Prava Predna vypocet/////////////////////////////////OKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKK

m_c3=sqrt(m_vyska1*m_vyska1+(m_x/4)*(m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama3=2*((asin((m_c3/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa3=-m_beta+(((-acos(m_vyska1/m_c3))/(pi/180)+(m_gama3/2)));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v3=255-((-m_alfa3+m_alfa_s-m_beta+60)/0.47058);
///////////////////////////////////uhol gama vysledny

m_gama_v3=((+m_alfa3-m_alfa_s+m_beta-m_gama3+m_gama_s+60)/0.47058);


////////////////////////////////////////////////////////////////////OKKKKKKKKKK///okkk
///////////////Prava Zadna vypocet//////////////////////////////////OKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKK
//tetiva nohy[2]ok!!!
m_c4=(sqrt(m_vyska2*m_vyska2+(m_x/4)*(m_x/4)))-x1; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama4=2*((asin((m_c4/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!

m_alfa4=+m_beta+180-(((asin(m_vyska2/m_c4))/(pi/180)+90-(m_gama4/2))); //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v4=(-m_alfa4+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v4=255-((+m_alfa4-m_alfa_s-m_gama4+m_gama_s-m_beta+60)/0.47058); //zhodne

//////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
///////////koncove vypocty ///////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
//////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
if(m_iCount<(2*m_krok_dlzka+2)){

//predna lava krok
	m_servo0_vysledok=m_alfa_v+0.5*1;
	m_servo1_vysledok=m_gama_v+0.5*1;
UpdateData(false);

//zadna lava 
	m_servo2_vysledok=m_alfa_v2+0.5*1;
	m_servo3_vysledok=m_gama_v2+0.5*1;
UpdateData(false);

//predna prava
	m_servo4_vysledok=m_alfa_v3+0.5*1;
	m_servo5_vysledok=m_gama_v3+0.5*1;	
UpdateData(false);
    
//zadna prava
    m_servo6_vysledok=m_alfa_v4+0.5*1;
	m_servo7_vysledok=m_gama_v4+0.5*1;
UpdateData(false);	

//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_x==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	else{
		if(m_x==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////Prve servo 
/////////////////////////////////////Predna prava noha uhol alfa
		else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(sx0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Druhe servo
/////////////////////////////////////Predna prava noha uhol gama	

 else{			
				
	 //
	 if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Tretie servo
/////////////////////////////////////Zadna prava noha uhol alfa					
else{				
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Stvrte servo
/////////////////////////////////////Zadna prava noha uhol gama					
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Piate servo
/////////////////////////////////////Zadna lava noha uhol alfa					

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Sieste servo
/////////////////////////////////////Zadna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Siedme servo
/////////////////////////////////////Predna lava noha uhol alfa

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Osme servo
/////////////////////////////////////Predna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

   }	
   }
   }
   	 }	
	 }
	 }
        }	
		}
		}
			}	
		    }
		    }
			  }	
			  }
			  }
			    }
			    }
				} 
				  }
   				  }
			      }
			        }
			        }
		            }

	//MessageBox(TEXT("Could not verify password."), NULL, MB_OK); 	
	// TODO: Add your control notification handler code here
if(m_faza1==2*m_krok_dlzka)
     {
     m_faza2=0;
     m_x=-1;
	 //m_gama_s=m_gama;  //inicializacna hodnota prednej pravej
	 //m_gama_s1=m_gama2; //inicializacna hodnota prednej lavej zadnej pravej, lavej
	// m_alfa_s=m_alfa;  //predna prava
	 //m_alfa_s1=m_alfa2; //zadna lava prava
     //m_alfa_s4=m_alfa4; //predna lava
     }
m_faza1=m_faza1+1;

   
}


			//MessageBox("skuska","Error",MB_OK+MB_ICONERROR);
			}

///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_x=m_x+1;

/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;

		 

	 }////////////////////////////////////pre end 1 faza

//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 1/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////


//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 2/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////


if(m_faza2<=2*m_krok_dlzka)
{

////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKK//ok
///////////////Lava Predna vypocet//////////////////////////////////OKKKKKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKK

//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=sqrt(m_vyska1*m_vyska1+(((m_krok_dlzka*2)-m_x/4-(m_krok_dlzka/2))*((m_krok_dlzka*2)-m_x/4-(m_krok_dlzka/2)))); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=-m_beta+((+acos(m_vyska1/m_c))/(pi/180)+(m_gama/2)); //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s-m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s-m_beta+m_gama-m_gama_s+60)/0.47058;
//
//UpdateData(true);//////////okkkkkkkkkkkkk

/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKK////ok
///////////////Lava Zadna vypocet///////////////OKKKKKKKKKKKKKKKKKK
/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKK

//tetiva nohy[2]ok!!!
m_c2=(sqrt(m_vyska2*m_vyska2+(m_krok_dlzka/2+m_x/4)*(m_krok_dlzka/2+m_x/4)))-x1; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa2=+m_beta+((+acos(m_vyska2/m_c2))/(pi/180)+(m_gama2/2));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=255-((-m_alfa2+m_alfa_s+m_beta+60)/0.47058);
///////////////////////////////////uhol gama vysledny

m_gama_v2=(+m_alfa2-m_alfa_s-m_beta-m_gama2+m_gama_s+60)/0.47058;
///ok!!!!!!!!!!!!!!!!!
//////////////////////////////////////////////////OOOOOOOOOOOOOOOOOOOOKKKKKKKKKKKKKKKKKK//ok
///////////////Prava predna vypocet///////////////OOOOOOOKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
//////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
//krok nohy[2]ok!!!
m_y=sqrt(((m_krok_vyska*m_krok_vyska)-((((m_x-m_krok_dlzka)*(m_x-m_krok_dlzka)))*(((m_krok_vyska)*(m_krok_vyska))))/((m_krok_dlzka*m_krok_dlzka))));
//tetiva nohy[2]ok!!!
m_c3=sqrt(((m_vyska1-m_y)*(m_vyska1-m_y))+(-m_x+m_krok_dlzka/2+m_x/4)*(-m_x+m_krok_dlzka/2+m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama3=2*((asin((m_c3/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa3=-m_beta+(m_gama3/2)-((acos((m_vyska1-m_y)/m_c3))/(pi/180));

if(m_x>(m_krok_dlzka/2+m_x/4))
{
m_alfa3=-m_beta+(m_gama3/2)+((acos((m_vyska1-m_y)/m_c3))/(pi/180));  /////??????????????
}
///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v3=255-((-m_alfa3+m_alfa_s-m_beta+60)/0.47058);
///////////////////////////////////uhol gama vysledny

m_gama_v3=255-((-m_alfa3+m_alfa_s-m_beta+m_gama3-m_gama_s+60)/0.47058);

////////////////////////////////////////////////////////////////////OKKKKKKKK//ok
///////////////Prava zadna vypocet//////////////////////////////////OKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKK
//tetiva nohy[2]ok!!!
m_c4=sqrt(m_vyska2*m_vyska2+(m_krok_dlzka/2+m_x/4)*(m_krok_dlzka/2+m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama4=2*((asin((m_c4/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
//m_alfa4=((-acos(100/m_c4))/(pi/180)+(m_gama4/2));olddddddddd unknown
m_alfa4=+m_beta+(((acos(m_vyska2/m_c4))/(pi/180)+(m_gama4/2)));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v4=(-m_alfa4+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v4=(-m_alfa4+m_alfa_s+m_gama4-m_gama_s+m_beta+60)/0.47058;

//ok!!!!!!!!!!!!!!!!!!
//////////////////////////////////////////////////////////
///////////koncove vypocty ///////////////////////////////
//////////////////////////////////////////////////////////
if(m_iCount<(4*m_krok_dlzka+3)){

//predna lava
	m_servo0_vysledok=m_alfa_v+0.5*1;
	m_servo1_vysledok=m_gama_v+0.5*1;
UpdateData(false);

//zadna lava 
	m_servo2_vysledok=m_alfa_v2+0.5*1;
	m_servo3_vysledok=m_gama_v2+0.5*1;
UpdateData(false);

//predna prava 
	m_servo4_vysledok=m_alfa_v3+0.5*1;
	m_servo5_vysledok=m_gama_v3+0.5*1;	
UpdateData(false);
    
//zadna prava
    m_servo6_vysledok=m_alfa_v4+0.5*1;
	m_servo7_vysledok=m_gama_v4+0.5*1;
UpdateData(false);	

//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_faza1==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	else{
		if(m_faza1==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////Prve servo 
/////////////////////////////////////Predna prava noha uhol alfa
		else{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Druhe servo
/////////////////////////////////////Predna prava noha uhol gama	

 else{			
				
	 //
	 if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Tretie servo
/////////////////////////////////////Zadna prava noha uhol alfa					
else{				
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Stvrte servo
/////////////////////////////////////Zadna prava noha uhol gama					
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Piate servo
/////////////////////////////////////Zadna lava noha uhol alfa					

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Sieste servo
/////////////////////////////////////Zadna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Siedme servo
/////////////////////////////////////Predna lava noha uhol alfa

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Osme servo
/////////////////////////////////////Predna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

   }	
   }
   }
   	 }	
	 }
	 }
        }	
		}
		}
			}	
		    }
		    }
			  }	
			  }
			  }
			    }
			    }
				} 
				  }
   				  }
			      }
			        }
			        }
		            }

	//MessageBox(TEXT("Could not verify password."), NULL, MB_OK); 	
	// TODO: Add your control notification handler code here
if(m_faza2==2*m_krok_dlzka)
     {
     m_faza3=0;
     m_x=-1;
     }
m_faza2=m_faza2+1;   
}


			//MessageBox("skuska","Error",MB_OK+MB_ICONERROR);
			}

///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_x=m_x+1;
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
}////pre end faza 2

//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 2/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////

//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 3/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////


if(m_faza3<=2*m_krok_dlzka)
{

////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKK///ok
///////////////Lava Predna vypocet//////////////////////////////////OKKKKKKKKKKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKK

//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=(sqrt(m_vyska2*m_vyska2+(((m_krok_dlzka*2)-m_x/4-(m_krok_dlzka))*((m_krok_dlzka*2)-m_x/4-(m_krok_dlzka)))))-x1; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=+m_beta+((acos(m_vyska2/m_c))/(pi/180)+(m_gama/2)); //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s+m_beta+m_gama-m_gama_s+60)/0.47058;
//
//UpdateData(true);//////////okkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKK//ok
///////////////Lava Zadna vypocet///////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKKKK

//tetiva nohy[2]ok!!!
m_c2=sqrt(m_vyska1*m_vyska1+(m_krok_dlzka+m_x/4)*(m_krok_dlzka+m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa2=-m_beta+((+acos(m_vyska1/m_c2))/(pi/180)+(m_gama2/2));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=255-((-m_alfa2+m_alfa_s-m_beta+60)/0.47058);
///////////////////////////////////uhol gama vysledny

m_gama_v2=(+m_alfa2-m_alfa_s+m_beta-m_gama2+m_gama_s+60)/0.47058;

////////////////////////////////////////////////OKKKKKKKKKKKKKKK//ok
///////////////Prava Predna vypocet/////////////OKKKKKKKKKKKKKKK
///////////////////////////////////////////////OKKKKKKKKKKKKKKKK
m_c3=sqrt(m_vyska2*m_vyska2+((m_krok_dlzka-m_x/4)*(m_krok_dlzka-m_x/4)));
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!
m_gama3=2*((asin((m_c3/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok
//m_alfa3=180-((m_gama3/2)-(acos(100/m_c3))/(pi/180));
m_alfa3=+m_beta+(((acos(m_vyska2/m_c3))/(pi/180)+(m_gama3/2)));

m_alfa_v3=255-(-m_alfa3+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v3=255-(-m_alfa3+m_alfa_s+m_beta+m_gama3-m_gama_s+60)/0.47058;

////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKK//ok
///////////////Prava zadna vypocet//////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKKKKKK
m_y=sqrt(((m_krok_vyska*m_krok_vyska)-((((m_x-m_krok_dlzka)*(m_x-m_krok_dlzka)))*(((m_krok_vyska)*(m_krok_vyska))))/((m_krok_dlzka*m_krok_dlzka))));
//tetiva nohy[2]ok!!!
m_c4=sqrt(((m_vyska1-m_y)*(m_vyska1-m_y))+(-m_x+m_krok_dlzka+m_x/4)*(-m_x+m_krok_dlzka+m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama4=2*((asin((m_c4/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!

m_alfa4=-m_beta+((+acos((m_vyska1-m_y)/m_c4))/(pi/180)+(m_gama4/2));
if(m_x>(m_krok_dlzka+m_x/4))
{
m_alfa4=-m_beta+((-acos((m_vyska1-m_y)/m_c4))/(pi/180)+(m_gama4/2));
}
///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v4=(-m_alfa4+m_alfa_s-m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v4=(-m_alfa4+m_alfa_s-m_beta+m_gama4-m_gama_s+60)/0.47058;
//ok!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//////////////////////////////////////////////////////////
///////////koncove vypocty ///////////////////////////////
//////////////////////////////////////////////////////////
if(m_iCount<(6*m_krok_dlzka+4)){

//predna prava
	m_servo0_vysledok=m_alfa_v+0.5*1;
	m_servo1_vysledok=m_gama_v+0.5*1;
UpdateData(false);

//zadna prava rovnaka
	m_servo2_vysledok=m_alfa_v2+0.5*1;
	m_servo3_vysledok=m_gama_v2+0.5*1;
UpdateData(false);

//zadna lava rovnaka
	m_servo4_vysledok=m_alfa_v3+0.5*1;
	m_servo5_vysledok=m_gama_v3+0.5*1;	
UpdateData(false);
    
//predna lava
    m_servo6_vysledok=m_alfa_v4+0.5*1;
	m_servo7_vysledok=m_gama_v4+0.5*1;
UpdateData(false);	


//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_faza1==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	else{
		if(m_faza1==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////Prve servo 
/////////////////////////////////////Predna prava noha uhol alfa
		else{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Druhe servo
/////////////////////////////////////Predna prava noha uhol gama	

 else{			
				
	 //
	 if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Tretie servo
/////////////////////////////////////Zadna prava noha uhol alfa					
else{				
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Stvrte servo
/////////////////////////////////////Zadna prava noha uhol gama					
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Piate servo
/////////////////////////////////////Zadna lava noha uhol alfa					

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Sieste servo
/////////////////////////////////////Zadna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Siedme servo
/////////////////////////////////////Predna lava noha uhol alfa

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Osme servo
/////////////////////////////////////Predna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

   }	
   }
   }
   	 }	
	 }
	 }
        }	
		}
		}
			}	
		    }
		    }
			  }	
			  }
			  }
			    }
			    }
				} 
				  }
   				  }
			      }
			        }
			        }
		            }

	//MessageBox(TEXT("Could not verify password."), NULL, MB_OK); 	
	// TODO: Add your control notification handler code here
if(m_faza3==2*m_krok_dlzka)
     {
     m_faza4=0;
     m_x=-1;
     }
m_faza3=m_faza3+1;      
}


			//MessageBox("skuska","Error",MB_OK+MB_ICONERROR);
			}

///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_x=m_x+1;
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
}////pre end faza 3

//////////////////////////////////////////////////////////////////////////////
///////////////////FAZA 4/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////

if(m_faza4<=2*m_krok_dlzka)
{


////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK//ok
///////////////Lava Predna vypocet//////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK

//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=sqrt(m_vyska2*m_vyska2+(((m_krok_dlzka/2)-m_x/4)*((m_krok_dlzka/2)-m_x/4))); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=+m_beta+((acos(m_vyska2/m_c))/(pi/180)+(m_gama/2)); //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s+m_beta+m_gama-m_gama_s+60)/0.47058;
//
////UpdateData(true);//////////okkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKK//ok
///////////////Lava Zadna vypocet///////////////OKKKKKKKKKKKKKKKKKKKKK
/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKK
m_y=sqrt(((m_krok_vyska*m_krok_vyska)-((((m_x-m_krok_dlzka)*(m_x-m_krok_dlzka)))*(((m_krok_vyska)*(m_krok_vyska))))/((m_krok_dlzka*m_krok_dlzka))));
//tetiva nohy[2]ok!!!
m_c2=sqrt(((m_vyska1-m_y)*(m_vyska1-m_y))+(-m_x+3*m_krok_dlzka/2+m_x/4)*(-m_x+3*m_krok_dlzka/2+m_x/4)); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa2=-m_beta+((+acos((m_vyska1-m_y)/m_c2))/(pi/180)+(m_gama2/2));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=255-((-m_alfa2+m_alfa_s-m_beta+60)/0.47058);
///////////////////////////////////uhol gama vysledny

m_gama_v2=(+m_alfa2-m_alfa_s+m_beta-m_gama2+m_gama_s+60)/0.47058;


/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKK//ok
///////////////Prava Predna vypocet//////////////OKKKKKKKKKKKKKKKKKK
/////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKK
//tetiva nohy[2]ok!!!!!!!!!!!
m_c3=(sqrt(m_vyska2*m_vyska2+(((m_krok_dlzka*2)-m_x/4-(3*m_krok_dlzka/2))*((m_krok_dlzka*2)-m_x/4-(3*m_krok_dlzka/2)))))-x1;
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!
m_gama3=2*((asin((m_c3/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok
//m_alfa3=180-((m_gama3/2)-(acos(100/m_c3))/(pi/180));
m_alfa3=+m_beta+(((acos(m_vyska2/m_c3))/(pi/180)+(m_gama3/2)));

m_alfa_v3=255-(-m_alfa3+m_alfa_s+m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v3=255-(-m_alfa3+m_alfa_s+m_beta+m_gama3-m_gama_s+60)/0.47058;
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK//ok
///////////////Prava zadna vypocet//////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK
////////////////////////////////////////////////////////////////////OKKKKKKKKKKKKKKKKKKKKKKK
//tetiva nohy[2]ok!!!
m_c4=sqrt(m_vyska1*m_vyska1+(((m_krok_dlzka*2)-m_x/4-(3*m_krok_dlzka/2))*((m_krok_dlzka*2)-m_x/4-(3*m_krok_dlzka/2)))); //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!
m_gama4=2*((asin((m_c4/2)/64))/(pi/180));
//uhol medzi hornym klbom[4]ok!!!
m_alfa4=-m_beta+((-(acos(m_vyska1/m_c4))/(pi/180)+(m_gama4/2)));

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v4=(-m_alfa4+m_alfa_s-m_beta+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v4=255-((+m_alfa4-m_alfa_s+m_beta-m_gama4+m_gama_s+60)/0.47058);
//ok!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

//////////////////////////////////////////////////////////
///////////koncove vypocty ///////////////////////////////
//////////////////////////////////////////////////////////
if(m_iCount<(8*m_krok_dlzka+5)){

//predna prava
	m_servo0_vysledok=m_alfa_v+0.5*1;
	m_servo1_vysledok=m_gama_v+0.5*1;
UpdateData(false);

//zadna prava rovnaka
	m_servo2_vysledok=m_alfa_v2+0.5*1;
	m_servo3_vysledok=m_gama_v2+0.5*1;
UpdateData(false);

//zadna lava rovnaka
	m_servo4_vysledok=m_alfa_v3+0.5*1;
	m_servo5_vysledok=m_gama_v3+0.5*1;	
UpdateData(false);
    
//predna lava
    m_servo6_vysledok=m_alfa_v4+0.5*1;
	m_servo7_vysledok=m_gama_v4+0.5*1;
UpdateData(false);	


//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_faza1==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	else{
		if(m_faza1==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////Prve servo 
/////////////////////////////////////Predna prava noha uhol alfa
		else{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Druhe servo
/////////////////////////////////////Predna prava noha uhol gama	

 else{			
				
	 //
	 if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Tretie servo
/////////////////////////////////////Zadna prava noha uhol alfa					
else{				
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////Stvrte servo
/////////////////////////////////////Zadna prava noha uhol gama					
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Piate servo
/////////////////////////////////////Zadna lava noha uhol alfa					

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Sieste servo
/////////////////////////////////////Zadna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Siedme servo
/////////////////////////////////////Predna lava noha uhol alfa

else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////Osme servo
/////////////////////////////////////Predna lava noha uhol gama
else{
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

   }	
   }
   }
   	 }	
	 }
	 }
        }	
		}
		}
			}	
		    }
		    }
			  }	
			  }
			  }
			    }
			    }
				} 
				  }
   				  }
			      }
			        }
			        }
		            }

	//MessageBox(TEXT("Could not verify password."), NULL, MB_OK); 	
	// TODO: Add your control notification handler code here
if(m_faza4==2*m_krok_dlzka)
     {
     m_x=-1;
     }
m_faza4=m_faza4+1;      
}


			//MessageBox("skuska","Error",MB_OK+MB_ICONERROR);
			}

///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_x=m_x+1;

m_otoco=500;
m_otoco1=500;
m_otoco2=500;
m_otoco3=500;
m_otoco4=500;
m_otoco5=500;
m_otoco6=500;
m_otoco7=500;
m_otoco8=500;
m_otoco9=500;
m_otoco10=500;
m_otoco11=500;
m_otoco12=500;
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////
         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
}////pre end faza 4
///////////////////////////////////////////////////////////////////////////////////////
//////////////////OTACANIE/XXXX/////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
if(m_otoc_status==m_pocet_pootoceni)
{
m_cStopTime.EnableWindow(FALSE);
m_cStartTime.EnableWindow(TRUE);
///MessageBox(_T("Hello World"));
  KillTimer(ID_COUNT_TIMER);
}
if(m_otoc_status<m_pocet_pootoceni)
{
//(1.faza predna lava,,3.faza zadna prava,4.faza predna lava)
if(m_otoco<=m_krok_vyska)
{
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
///vyrovannie stability zdvih pohybujucej sa nohy
//zdvihanie a otacanie
m_c=100-m_otoco; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
///vyrovannie stability zdvih pohybujucej sa nohy
////////////////////////////////////////////////////
////////////////////////////////////////////////////
if(m_otoco==0&m_otoco9==0&m_otoc_status>0)
{
m_otoco=500;
}

if(m_otoco==0)
{
m_c=100-m_otoco+25;
}
////////////////////////////////////////////////////
////////////////////////////////////////////////////
m_c2=100-25;
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa2=m_gama2/2; //rpzdiel -90 ok!!
///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=(-m_alfa2+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny
m_gama_v2=(-m_alfa2+m_alfa_s+m_gama2-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!
///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny
m_gama_v=(-m_alfa+m_alfa_s+m_gama-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo0_vysledok=m_alfa_v+0.5*1;
m_servo1_vysledok=m_gama_v+0.5*1;
m_servo6_vysledok=m_alfa_v2+0.5*1;
m_servo7_vysledok=m_gama_v2+0.5*1;
m_servo2_vysledok=m_alfa_v2+0.5*1;
m_servo3_vysledok=m_gama_v2+0.5*1;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
if(m_otoco==0)
{
    if(m_otoco==0)
	{
    if(m_rot1!=false)
	{
    port.OpenPort("com1");
	}
	//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0)
	{
    port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
    //MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
    //port.ClosePort();
	}
    }
    /////////////////////////////////////0te-servo
    /////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
				}
              else{
				if (!(port.WriteByte(0))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
/////////////////////////////////////1ve-servo
/////////////////////////////////////	
 {			
				if(!(port.WriteByte(255))){
				//		MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			      //      port.ClosePort();
				}
              else{
				if (!(port.WriteByte(1))){
				//		MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                  //      port.ClosePort();
				}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////7-me servo
/////////////////////////////////////
{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////8-me servo
/////////////////////////////////////
{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////
/////////////////////////////////////
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco=m_otoco+1;

if(m_otoco==m_krok_vyska)
{
m_otoco1=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
}////////////////////////////////////pre end otacanie
if(m_otoco1<=m_uhol_otocenie)
{
if(m_rot1==true)
{
cx=1;
}
if(m_rot1==false)
{
cx=-1;
}
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
m_servo8_vysledok=127-(cx)*(m_otoco1*360/127);

UpdateData(false);
/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
UpdateData(true);
BYTE data=0;
if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////deviate servo
/////////////////////////////////////
if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
				else{
				if (!(port.WriteByte(8))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo8_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			  }
				}
//////////////////////////////////////////////////////
//////////////////////////////////////////////////////

///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco1=m_otoco1+1;
if(m_otoco1==m_uhol_otocenie)
{
m_otoco2=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie


if(m_otoco2<=m_krok_vyska+1)
{
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
if(m_otoco2==m_krok_vyska+1)
{
m_servo6_vysledok=127;
m_servo7_vysledok=127;
m_servo2_vysledok=127;
m_servo3_vysledok=127;
}
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=100-m_krok_vyska+m_otoco2; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s+m_gama-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo0_vysledok=m_alfa_v+0.5*1;
m_servo1_vysledok=m_gama_v+0.5*1;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	

/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////jedenaste servo
/////////////////////////////////////			
///////////////////////////////////desiate servo
/////////////////////////////////////	
 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
			  }
			  }
 }

 			  }
			  }
 }

			  }
			  }
 }
			  }
			  }
	}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco2=m_otoco2+1;

if(m_otoco2==m_krok_vyska)
{
m_otoco3=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
}////////////////////////////////////pre end otacanie

///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///2.faza zadna lava

if(m_otoco3<=m_krok_vyska)
{
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=100-m_otoco3; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
///vyrovannie stability zdvih pohybujucej sa nohy
////////////////////////////////////////////////////
////////////////////////////////////////////////////
if(m_otoco3==0)
{
m_c=100-m_otoco3+25;
}
////////////////////////////////////////////////////
////////////////////////////////////////////////////
m_c2=100-25;
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa2=m_gama2/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=(+m_alfa2-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v2=(+m_alfa2-m_alfa_s-m_gama2+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(+m_alfa-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(+m_alfa-m_alfa_s-m_gama+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo4_vysledok=m_alfa_v+0.5*1;
m_servo5_vysledok=m_gama_v+0.5*1;
m_servo2_vysledok=m_alfa_v2+0.5*1;
m_servo3_vysledok=m_gama_v2+0.5*1;
m_servo6_vysledok=m_alfa_v2+0.5*1;
m_servo7_vysledok=m_gama_v2+0.5*1;

UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	
 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
				/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	

			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco3=m_otoco3+1;

if(m_otoco3==m_krok_vyska)
{
m_otoco4=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie

if(m_otoco4<=m_uhol_otocenie)
{
if(m_rot1==true)
{
cx=1;
}
if(m_rot1==false)
{
cx=-1;
}

///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
m_servo10_vysledok=127-(cx)*(m_otoco4*(360/127));

UpdateData(false);
/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
UpdateData(true);
BYTE data=0;
if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////deviate servo
/////////////////////////////////////
if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
				else{
				if (!(port.WriteByte(10))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo10_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			  }
				}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco4=m_otoco4+1;
if(m_otoco4==m_uhol_otocenie)
{
m_otoco5=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie


if(m_otoco5<=m_krok_vyska+1)
{
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
if(m_otoco5==m_krok_vyska+1)
{
m_servo4_vysledok=127;
m_servo5_vysledok=127;
m_servo0_vysledok=127;
m_servo1_vysledok=127;
}

//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=100-m_krok_vyska+m_otoco5; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rozdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(+m_alfa-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(+m_alfa-m_alfa_s-m_gama+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo4_vysledok=m_alfa_v+0.5*1;
m_servo5_vysledok=m_gama_v+0.5*1;

UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	
				/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	
		      }
			  }
 }
		      }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco5=m_otoco5+1;

if(m_otoco5==m_krok_vyska)
{
m_otoco6=0;
if(m_rot2==true)
{
m_otoco6=500;
m_otoco12=0;
}

}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
}

///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///3.faza

if(m_otoco6<=m_krok_vyska)
{
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=100-m_otoco6; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
if(m_otoco6==0)
{
m_c=100-m_otoco6+25;
}
////////////////////////////////////////////////////
m_c2=100-25;
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa2=m_gama2/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=(-m_alfa2+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v2=(-m_alfa2+m_alfa_s+m_gama2-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////

//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s+m_gama-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
m_servo6_vysledok=m_alfa_v+0.5*1;
m_servo7_vysledok=m_gama_v+0.5*1;
m_servo0_vysledok=m_alfa_v2+0.5*1;
m_servo1_vysledok=m_gama_v2+0.5*1;
m_servo4_vysledok=m_alfa_v2+0.5*1;
m_servo5_vysledok=m_gama_v2+0.5*1;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////
				{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
				{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco6=m_otoco6+1;

if(m_otoco6==m_krok_vyska)
{
m_otoco7=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie

if(m_otoco7<=m_uhol_otocenie)
{
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
if(m_rot1==true)
{
cx=1;
}
if(m_rot1==false)
{
cx=-1;
}

m_servo11_vysledok=127-(cx)*(m_otoco7*360/127);

UpdateData(false);
/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
UpdateData(true);
BYTE data=0;
if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////deviate servo
/////////////////////////////////////
if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
				else{
				if (!(port.WriteByte(11))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo11_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
			  }
				}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco7=m_otoco7+1;
if(m_otoco7==m_uhol_otocenie)
{
m_otoco8=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie


if(m_otoco8<=m_krok_vyska+1)
{
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
if(m_otoco8==m_krok_vyska+1)
{
m_servo0_vysledok=127;
m_servo1_vysledok=127;
m_servo4_vysledok=127;
m_servo5_vysledok=127;
}

m_c=100-m_krok_vyska+m_otoco8; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(-m_alfa+m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(-m_alfa+m_alfa_s+m_gama-m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo6_vysledok=m_alfa_v+0.5*1;
m_servo7_vysledok=m_gama_v+0.5*1;
UpdateData(false);


//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(6))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo6_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(7))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo7_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	
					{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(0))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo0_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(1))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo1_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	
/////////////////////////////////////jedenaste servo
/////////////////////////////////////					
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
	}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco8=m_otoco8+1;

if(m_otoco8==m_krok_vyska)
{
m_otoco9=0;

if(m_rot2==true)
{
m_otoco9=500;
m_otoco=0;
}

}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
}

///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///4.faza

if(m_otoco9<=m_krok_vyska)
{
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
m_c=100-m_otoco9; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!

if(m_otoco9==0)
{
m_c=100-m_otoco9+25;
}
////////////////////////////////////////////////////

m_c2=100-25;
m_gama2=2*((asin((m_c2/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa2=m_gama2/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v2=(+m_alfa2-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v2=(+m_alfa2-m_alfa_s-m_gama2+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////

m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(+m_alfa-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(+m_alfa-m_alfa_s-m_gama+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo2_vysledok=m_alfa_v+0.5*1;
m_servo3_vysledok=m_gama_v+0.5*1;
m_servo4_vysledok=m_alfa_v2+0.5*1;
m_servo5_vysledok=m_gama_v2+0.5*1;
m_servo0_vysledok=m_alfa_v2+0.5*1;
m_servo1_vysledok=m_gama_v2+0.5*1;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
if(m_rot2==true)
{
  
   
	
	if(m_otoco9==0)
	{
    port.OpenPort("com1");
	;
	;
	;
	;
	port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
	}
  
}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }
              else{
				if (!(port.WriteByte(2))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				                         }
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                         }
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }
              else{
				if (!(port.WriteByte(3))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				                          }
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }

/////////////////////////////////////jedenaste servo
/////////////////////////////////////	
				{			
				if(!(port.WriteByte(255))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }
              else{
				if (!(port.WriteByte(4))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				                         }
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
				{			
				if(!(port.WriteByte(255))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                          }
              else{
				if (!(port.WriteByte(5))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				                         }
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						;//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				                         }

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco9=m_otoco9+1;

if(m_otoco9==m_krok_vyska)
{
m_otoco10=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie

if(m_otoco10<=m_uhol_otocenie)
{
if(m_rot1==true)
{
cx=1;
}
if(m_rot1==false)
{
cx=-1;
}
///MessageBox("jkkjkj","Error",MB_OK+MB_ICONERROR);
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
m_servo9_vysledok=127-(cx)*(m_otoco10*360/127);

UpdateData(false);
/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
UpdateData(true);
BYTE data=0;
if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////deviate servo
/////////////////////////////////////
if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
				else{
				if (!(port.WriteByte(9))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(m_servo9_vysledok))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}
			  }
				}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco10=m_otoco10+1;
if(m_otoco10==m_uhol_otocenie)
{
m_otoco11=0;
}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
	 }////////////////////////////////////pre end otacanie


if(m_otoco11<=m_krok_vyska+1)
{
////////////////////////////////////////////////////////////////////
///////////////Lava Predna vypocet//////////////////////////////////
////////////////////////////////////////////////////////////////////
//tetiva nohy[2]ok!!!!!!!!!!!!!!!!!!!
if(m_otoco11==m_krok_vyska+1)
{
m_servo4_vysledok=127;
m_servo5_vysledok=127;
m_servo0_vysledok=127;
m_servo1_vysledok=127;
}
m_c=100-m_krok_vyska+m_otoco11; //ok (m_x/8)!!!!!!!!!!!!!!!!!!!!!!!!
//uhol medzi dolnym klbom[3]ok!!!!!!!!!!!!!!!!
m_gama=2*((asin((m_c/2)/64))/(pi/180));//ok!!!!!!!!
//uhol medzi hornym klbom[4]ok!!!!!!!!!!
m_alfa=m_gama/2; //rpzdiel -90 ok!!

///////////////////////////////////uhol alfa vysledny[5]ok
m_alfa_v=(+m_alfa-m_alfa_s+60)/0.47058;
///////////////////////////////////uhol gama vysledny

m_gama_v=(+m_alfa-m_alfa_s-m_gama+m_gama_s+60)/0.47058;
//////////////////////////////////////////////////////////////////////
m_servo2_vysledok=m_alfa_v+0.5*1;
m_servo3_vysledok=m_gama_v+0.5*1;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(2))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo2_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////desiate servo
/////////////////////////////////////	

 {			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(3))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo3_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////		
				{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(4))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo4_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////
				{			
				if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
              else{
				if (!(port.WriteByte(5))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo5_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////jedenaste servo
/////////////////////////////////////
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
 }
			  }
			  }
	}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco11=m_otoco11+1;

if(m_otoco11==m_krok_vyska)
{
m_otoco12=0;

if(m_rot2==true)
{
m_otoco12=500;
m_otoco6=0;
}

}
/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////
         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
}
if(m_otoco12<1)
{
m_servo8_vysledok=127;
m_servo9_vysledok=127;
m_servo10_vysledok=127;
m_servo11_vysledok=127;
UpdateData(false);
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
	UpdateData(true);
	BYTE data=0;
	if(m_otoco==0){
       port.OpenPort("com1");
		//MessageBox("Cannot open Communication Port.Please\nquit the application & re-start your PC.","Error",MB_OK+MB_ICONERROR);
	}
	if(m_otoco==0){
            port.ConfigurePort(9600,8,0,NOPARITY ,ONESTOPBIT );
			//MessageBox("Cannot Configure Communication Port","Error",MB_OK+MB_ICONERROR);
			//port.ClosePort();
		}
/////////////////////////////////////osme servo
/////////////////////////////////////
	   {			
		if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			port.ClosePort();}
              else{
				if (!(port.WriteByte(8))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				  if(!(port.WriteByte(m_servo8_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}
/////////////////////////////////////deviate servo
/////////////////////////////////////	

		{			
		  if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			   port.ClosePort();}
              else{
				if (!(port.WriteByte(9))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        port.ClosePort();}
              else{
				if(!(port.WriteByte(m_servo9_vysledok))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            port.ClosePort();}

/////////////////////////////////////desiate servo
/////////////////////////////////////	

		{			
		  if(!(port.WriteByte(255))){
						MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			   port.ClosePort();}
              else{
				if (!(port.WriteByte(10))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(m_servo10_vysledok))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}	
/////////////////////////////////////jedenaste servo
/////////////////////////////////////	

		{			
		  if(!(port.WriteByte(255))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			   //port.ClosePort();
		  }
              else{
				if (!(port.WriteByte(11))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
                        //port.ClosePort();
				}
              else{
				if(!(port.WriteByte(m_servo11_vysledok))){
						//MessageBox("Cannot Write to Port","Error",MB_OK+MB_ICONERROR);
			            //port.ClosePort();
				}		
			  }
			  }
    }
			  }
			  }
	}
              }
			  }
	}
	          }
			  }
	}
///////////////////////////////////
///////...zaverecne inkrementovanie
///////////////////////////////////
m_otoco12=m_otoco12+1;
//port.ClosePort();
///MessageBox(_T("Hello World"));

/////////////////////////////////
///////...koniec inkrementovania
/////////////////////////////////

         // Format and display the count
         m_sCount.Format("%d", m_iCount);
         break;
		 
}




///////////////////////////////////////////////////////////
//////////////////END OTACANIE/////////////////////////////
///////////////////////////////////////////////////////////
//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


///podmienka pre opakovanie krokov
if(m_faza1>2*m_krok_dlzka)
{
m_faza1=1;
m_kroky_status=m_kroky_status+1;

if(m_kod_kroku==1)
{
m_otoc_status=m_otoc_status+1;
m_faza1=500;
m_otoco=0;
if(m_rot2==true)
{
m_faza1=500;
m_otoco9=0;
}
/*
KillTimer(ID_COUNT_TIMER);
 
     // Disable the Stop Timer button
    m_cStopTime.EnableWindow(FALSE);
     // Enable the Start Timer button
    m_cStartTime.EnableWindow(TRUE);
    port.ClosePort();
*/

}
m_faza2=255;
m_faza3=255;
m_faza4=255;
m_otoco=255;
if(m_kod_kroku==1)
{
m_otoco=0;
//port.ClosePort();
}
m_iCount=0;
m_x=0;

port.ClosePort();
}


/////////////////////////////

/////////////////////////////////////////

}



}////////////////////////////////////////////end of big if


}





     // Update the dialog
     UpdateData(FALSE);

	 ////////////////////////////////////////////////////
	 ///////////Inkrementovanie posunutia nohy
     ////////////////////////

     ///////////////////////
     // MY CODE ENDS HERE
     ///////////////////////
	CFormView::OnTimer(nIDEvent);
}

void CControl_robotView::OnEnChangeInterval()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
     ///////////////////////
     // MY CODE STARTS HERE
     ///////////////////////
 
     // Update the variables
     UpdateData(TRUE);
 
     ///////////////////////
     // MY CODE ENDS HERE
     ///////////////////////

}

void CControl_robotView::OnStoptimer()
{
	// TODO: Add your control notification handler code here
   ///////////////////////
     // MY CODE STARTS HERE
     ///////////////////////
 
     // Stop the timer
     KillTimer(ID_COUNT_TIMER);
 
     // Disable the Stop Timer button
     m_cStopTime.EnableWindow(FALSE);
     // Enable the Start Timer button
     m_cStartTime.EnableWindow(TRUE);

     m_otoc.EnableWindow(TRUE);

     port.ClosePort();
 
     ///////////////////////
     // MY CODE ENDS HERE
     ///////////////////////


}

void CControl_robotView::OnEnChangeKrokDlzka()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}

void CControl_robotView::OnEnChangeKrokVyska()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}



void CControl_robotView::OnEnChangeEdit16()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}

void CControl_robotView::OnBnClickedButton2()
{
	// TODO: Add your control notification handler code here
	// TODO: Add your control notification handler code here
      ///////////////////////
      // MY CODE STARTS HERE
      ///////////////////////
 
     // Update the variables
     UpdateData(TRUE);
 
	 ////////////////////////////////////////////////////
     // Initialize the count, nastavenie nulovej polohy
	 ////////////////////////////////////////////////////
	m_kod_kroku=1;
	m_iCount = 0;
    m_x=0;
	m_otoco=0;

	m_otoco1=500;
    m_otoco2=500;
	m_otoco3=500;
	m_otoco4=500;
    m_otoco5=500;
	m_otoco6=500;
    m_otoco7=500;
    m_otoco8=500;
	m_otoco9=500;
	m_otoco10=500;
	m_otoco11=500;
	m_otoco12=500;
    
  if(m_rot2==true)
	{
    m_otoco=500;
    m_otoco9=0;
	}
  
	m_faza1=500;
	m_faza2=500;
    m_faza3=500;
	m_faza4=500;
	m_otoc_status=0;

	m_gama_s=102.75003;
	m_gama_s1=0;
	m_alfa_s=51.3751;
	m_alfa_s1=128.6248;
    m_alfa_s4=51.3751;
     //else{
		//	if(!(port.SetCommunicationTimeouts(0,500,0,0,0))){
		//		MessageBox("Cannot Configure Communication Timeouts","Error",MB_OK+MB_ICONERROR);
		//	port.ClosePort();}
     // Format the count for displaying
   
	m_sCount.Format("%d", m_iCount);

     // Update the dialog
     UpdateData(FALSE);
     // Start the timer
     SetTimer(ID_COUNT_TIMER, m_iInterval, NULL);
 
     // Enable the Stop Timer button
     m_cStopTime.EnableWindow(TRUE);
     // Disable the Start Timer button
     m_cStartTime.EnableWindow(FALSE);
//	 m_otoc.EnableWindow(FALSE);
     ///////////////////////
     // MY CODE ENDS HERE
     ///////////////////////
}
void CControl_robotView::OnEnChangeEdit12()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
}

//void CControl_robotView::OnBnClickedRadio1()
//{
//	// TODO: Add your control notification handler code here
//}

void CControl_robotView::OnBnClickedButton3()
{
	// TODO: Add your control notification handler code here
	//int faza1::back(1,10);
}

void CControl_robotView::OnEnChangeRichedit22()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here

}
void CControl_robotView::OnEnChangeKrokyPocet()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}

void CControl_robotView::OnEnChangeEdit17()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}

void CControl_robotView::OnEnChangeEdit18()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	UpdateData(TRUE);
}

void CControl_robotView::OnEnChangeEdit19()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit20()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit21()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit22()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit23()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit24()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit25()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnEnChangeEdit26()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CFormView::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.
UpdateData(TRUE);
	// TODO:  Add your control notification handler code here
}

void CControl_robotView::OnBnClickedButton8()
{
	// TODO: Add your control notification handler code here
if(m_rot2==true)
{
//	MessageBox(_T("222222222"),NULL,MB_OK);
}

if(m_rot1==true)
{
//	MessageBox(_T("111111111"),NULL,MB_OK);
}

/*if(CControl_robotView::SetDlgItemInt==0)
{
	MessageBox(_T("test_OK"),NULL,MB_OK);
}

*/
if(m_combo1==1)
{
	MessageBox(_T("cucak"),NULL,MB_OK);
}
if(m_combo1==2)
{
	MessageBox(_T("3333333333"),NULL,MB_OK);
}


}

void CControl_robotView::OnCbnSelchangeCombo1()
{
	// TODO: Add your control notification handler code here

UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo2()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo3()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo4()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo5()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo6()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo7()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnCbnSelchangeCombo8()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
}

void CControl_robotView::OnBnClickedRadio2()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
m_rot2=true;
m_rot1=false;
}

void CControl_robotView::OnBnClickedRadio1()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
m_rot2=false;
m_rot1=true;
}

void CControl_robotView::OnBnClickedRadio3()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
m_dozadu=true;
}

void CControl_robotView::OnBnClickedRadio4()
{
	// TODO: Add your control notification handler code here
UpdateData(TRUE);
m_dozadu=false;
}
