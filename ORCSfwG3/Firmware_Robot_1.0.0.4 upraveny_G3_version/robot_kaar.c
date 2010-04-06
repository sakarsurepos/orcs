/*****************************************************
This program was produced by the
CodeWizardAVR V1.24.8d Professional
Automatic Program Generator
© Copyright 1998-2006 Pavel Haiduc, HP InfoTech s.r.l.
http://www.hpinfotech.com

Project : ROBOT_KAAR
Version : 1.0.0.3
Date    : 6.12.2007
Author  : Stanislav Spisak                            
Company : TUKE SJF KAAR                            
Comments: 


Chip type           : ATmega128L
Program type        : Application
Clock frequency     : 7,372800 MHz
Memory model        : Small
External SRAM size  : 0
Data Stack size     : 1024
*****************************************************/

#include <mega128.h>

#include <stdio.h>
#include <delay.h>

#define RXB8 1
#define TXB8 0
#define UPE 2
#define OVR 3
#define FE 4
#define UDRE 5
#define RXC 7 

#define FRAMING_ERROR (1<<FE)
#define PARITY_ERROR (1<<UPE)
#define DATA_OVERRUN (1<<OVR)
#define DATA_REGISTER_EMPTY (1<<UDRE)
#define RX_COMPLETE (1<<RXC)

#define CAMERA PORTC.7         ///uprava kamil PG2
#define H_V_CAM_SERVO PORTC.5  ///uprava kamil P??
#define FOREWARD PORTD.5
#define BACKWARD PORTD.6
#define SERVOS_PWM 9216  //50 Hz
#define DC_MOTOR_PWM 9216 //50 Hz

#define FIRST_ADC_INPUT 0
#define LAST_ADC_INPUT 0 

#define RX_BUFFER_SIZE1 128
#define GPS_BUFFER 128
#define TX_BUFFER_SIZE0 128
#define RX_BUFFER_SIZE0 20
#define DATA_BYTES0 20

#define ADC_VREF_TYPE 0x20


char tx_buffer0[TX_BUFFER_SIZE0];
char rx_buffer1[RX_BUFFER_SIZE1];
char rx_buffer0[RX_BUFFER_SIZE0];
char GPS_data[GPS_BUFFER];

char data_bytes[DATA_BYTES0];
char data_0[DATA_BYTES0];

unsigned char adc_data[1];

bit rx_buffer_overflow1;
bit rx_buffer_overflow0;

unsigned int GPS_index;
unsigned int Timer_0;
unsigned int status;
unsigned int c_counter;
unsigned int rx_wr_index1,rx_rd_index1,rx_counter1;
unsigned int rx_wr_index0, rx_rd_index0, rx_counter0, counter;
unsigned int tx_rd_index0,tx_counter0,tx_wr_index0;

//************************************************************* ADC ******************************************************************

// ADC interrupt service routine
// with auto input scanning
interrupt [ADC_INT] void adc_isr(void)
        {
        register unsigned char input_index=0;

        // Read the 8 most significant bits
        // of the AD conversion result
        adc_data[input_index]=ADCH;
        // Select next ADC input
        if (++input_index > (LAST_ADC_INPUT-FIRST_ADC_INPUT))
                input_index=0;
        ADMUX=(FIRST_ADC_INPUT|ADC_VREF_TYPE)+input_index;
        // Start the AD conversion
        ADCSRA|=0x40;       
        }

//**************************************************** RX INTERRUPT USART0 ***********************************************************

// USART0 Receiver interrupt service routine
interrupt [USART0_RXC] void usart0_rx_isr(void)
        {
        char status,data;
        status=UCSR0A;
        data=UDR0;
        if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN))==0)
                {      
                rx_buffer0[rx_wr_index0]=data;
                if (++rx_wr_index0 == RX_BUFFER_SIZE0) 
                        rx_wr_index0=0;
                if (++rx_counter0 == RX_BUFFER_SIZE0)
                        {
                        rx_counter0=0;
                        rx_buffer_overflow0=1;
                        };               
                };
        }

//**************************************************** TX INTERRUPT USART0 ************************************************************

// USART0 Transmitter interrupt service routine
interrupt [USART0_TXC] void usart0_tx_isr(void)
{
if (tx_counter0)
   {
   --tx_counter0;
   UDR0=tx_buffer0[tx_rd_index0];
   if (++tx_rd_index0 == TX_BUFFER_SIZE0) 
        tx_rd_index0=0;
   };
}

//***************************************************** RD_USART0 *****************************************************************

void RD_USART0(void)
         {
         while(rx_counter0 != 0)
                  {
                  #asm("cli")
                  data_0[counter] = rx_buffer0[rx_rd_index0];
                                          
                  if(data_0[counter - 7] == '$' && data_0[counter - 6] == 'A' && data_0[counter - 5] == '7')
                        { 
                        if(data_0[counter] == 10/*<LF>*/)
                                { 
                                data_bytes[2] = data_0[counter - 4];
                                data_bytes[1] = data_0[counter - 3];
                                data_bytes[0] = data_0[counter - 2];
                                counter = 0;                                
                                goto here1;                                
                                }
                        }
                   counter++;
                   here1:
                   if(++rx_rd_index0 == RX_BUFFER_SIZE0)
                        rx_rd_index0 =0;
                   --rx_counter0;
                  #asm("sei")
                  }
         }

char RD_DATA_BUFFER(int znak)
        {
        return data_bytes[znak];
        }


//************************************************** RX INTERRUPT USART1 *************************************************************
// USART1 Receiver interrupt service routine
interrupt [USART1_RXC] void usart1_rx_isr(void)
{
char status,data;
status = UCSR1A;
data = UDR1;

if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN)) == 0)
   {
   rx_buffer1[rx_wr_index1] = data;
   
   if(++rx_wr_index1 == RX_BUFFER_SIZE1)
          rx_wr_index1=0;
       
   if(++rx_counter1 == RX_BUFFER_SIZE1)
        {
        rx_counter1=0;
        rx_buffer_overflow1=1;
        };
       
   };
} 
//*********************************************************************************************************************************
void WR_USART0(char data_for_send)
{
         while (tx_counter0 == TX_BUFFER_SIZE0);
         #asm("cli")
         if (tx_counter0 || ((UCSR0A & DATA_REGISTER_EMPTY)==0))
                  {
                  tx_buffer0[tx_wr_index0] = data_for_send;
                  if (++tx_wr_index0 == TX_BUFFER_SIZE0) 
                          tx_wr_index0=0;
                  ++tx_counter0;
                  }
         else
                  UDR0=data_for_send;
         #asm("sei")
}

void RD_USART1(void)
        { 
        while(rx_counter1 != 0)
                {
                GPS_data[GPS_index] = rx_buffer1[rx_rd_index1];
                
                #asm("cli")        
                if(GPS_data[GPS_index] == 10)
                        { 
                        for(c_counter = 0;c_counter <= GPS_index; c_counter++)
                                {
                                WR_USART0(GPS_data[c_counter]);                                                                    
                                }
                        GPS_index = 0;
                        goto here;
                        };
                
                GPS_index++;
                here:
                if(++rx_rd_index1 == RX_BUFFER_SIZE1)
                        rx_rd_index1 = 0;
                        
                --rx_counter1;
                #asm("sei")
                };
        }

//**************************************************** DEVICES ****************************************************************
void SERVO1( int servo1 )                       //Direction servo
        {
        OCR3CH = (servo1 >> 8) & 0xFF;
        OCR3CL = servo1 & 0xFF;
        } 
        
void SERVO3( int servo3 )                       //Camera servo
        {
        OCR3AH = (servo3 >> 8) & 0xFF;
        OCR3AL = servo3 & 0xFF;
        } 
        
void SERVO2( int servo2 )
        {
        OCR3BH = (servo2 >> 8) & 0xFF;
        OCR3BL = servo2 & 0xFF;
        }                

void DC_MOTOR ( int dc_motor )
        {
        OCR1BH = (dc_motor >> 8) & 0xFF;
        OCR1BL = dc_motor & 0xFF;
        } 
        
void BATTERY_STATUS(void)
        {
        if(status == 1)
                {
                WR_USART0('$');
                WR_USART0('M');
                WR_USART0('S');
                WR_USART0('G');
                WR_USART0('1');
                WR_USART0(',');
                WR_USART0(adc_data[0]);
                WR_USART0(13);             //<CR>
                WR_USART0(10);             //<LF>
                
                status = 0;
                }
        }               
//**************************************************** TIMER_0 ***************************************************************
 // Timer 0 overflow interrupt service routine
interrupt [TIM0_OVF] void timer0_ovf_isr(void)
{
TCNT0=0x01;
Timer_0++; 
if(Timer_0 == 28)
        {
        status = 1;
        Timer_0 = 0;
        }     
} 

//****************************************************************************************************************************
//***************************************************** MAIN *****************************************************************

void main(void)
{ 

int servo_start_position = 280;
//int dc_motor_start_position = 0;
int korekcia_1 = 146;
int i;
char data[DATA_BYTES0];

//****************************************** PORT A 
// Input/Output Ports initialization 
// Port A initialization
PORTA=0x00;
DDRA=0x00;
//****************************************** PORT B
// Port B initialization
// DDRB.0 = 1;  PORTB.0 = 1; SERVO1 - power supply transistor - (normally ON) //remove
// DDRB.6 = PWM OC3A   PORTE.3 = 0
PORTB=0x00;    ///uprava kamil 0x01                                                                            
DDRB=0x40;     ///uprava kamil 0x41
//****************************************** PORT C
//DDRC.7 = 1; PORTC.7 = 1; CAMERA ON/OFF (normally ON)
PORTC=0xFF; //uprava 0xB0
DDRC=0xFF;  //uprava 0xB0
//****************************************** PORT D
// Port D initialization
// DDRD.5 = 1; PORTD.5 = 0; DC_MOTOR_Direction 1/1 (normally OFF)
// DDRD.6 = 1; PORTD.6 = 0; DC_MOTOR_Direction 1/2 (normally OFF)
PORTD=0x00;
DDRD=0x60;
//****************************************** PORT E
// Port E initialization
// DDRE.3 = PWM OC3A   PORTE.3 = 0; SERVO#2
// DDRE.4 = PWM OC3B   PORTE.4 = 0; SERVO#1
// DDRE.5 = PWM OC3C   PORTE.5 = 0; SERVO#3
// DDRE.6 = 1;  PORTB.6 = 1; SERVO2 - power supply transistor - (normally ON)  //remove
// DDRE.7 = 1;  PORTB.7 = 1; SERVO3 - power supply transistor - (normally ON)  //remove
PORTE=0x00;  ///uprava kamil 0xC0
DDRE=0x38;   ///uprava kamil 0xF8
//****************************************** PORT F
// Port F initialization
PORTF=0x00;
DDRF=0x00;
//****************************************** PORT G
// Port G initialization
PORTG=0x00;
DDRG=0x00;
//****************************************** TIMER 0
// Timer/Counter 0 initialization
// Clock source: 7200Hz System Clock
// Clock value: Timer 0 Stopped
// Mode: Normal top=FFh
// OC0 output: Disconnected
ASSR=0x00;
TCCR0=0x07;
TCNT0=0x01;
OCR0=0x00;
//****************************************** TIMER 1
// Timer/Counter 1 initialization
// Clock source: System Clock
// Clock value: 7372,800 kHz
// Mode: Ph. & fr. cor. PWM top=ICR1
// OC1A output: Discon.
// OC1B output: Non-Inv.
// OC1C output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 1 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR1A=0x20;
TCCR1B=0x12;
TCNT1H=0x00;
TCNT1L=0x00;

ICR1H =(DC_MOTOR_PWM >> 8) & 0xFF;
ICR1L = DC_MOTOR_PWM & 0xFF;

DC_MOTOR(0);  //DC motor null

//***************************************** TIMER 2 
// Timer/Counter 2 initialization
// Clock source: System Clock
// Clock value: Timer 2 Stopped
// Mode: Normal top=FFh
// OC2 output: Disconnected
TCCR2=0x00;
TCNT2=0x00;
OCR2=0x00;
//***************************************** TIMER 3 
// Timer/Counter 3 initialization
// Clock source: System Clock
// Clock value: 7372,800 kHz
// Mode: Ph. & fr. cor. PWM top=ICR3
// Noise Canceler: Off
// Input Capture on Falling Edge
// OC3A output: Discon.
// OC3B output: Non-Inv.
// OC3C output: Discon.
// Timer 3 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR3A=0xA8;
TCCR3B=0x12;
TCNT3H=0x00;
TCNT3L=0x00;

ICR3H =(SERVOS_PWM >> 8) & 0xFF;
ICR3L = SERVOS_PWM & 0xFF;

SERVO1(servo_start_position);
SERVO2(servo_start_position);
SERVO3(servo_start_position);
//**************************************************

// External Interrupt(s) initialization
// INT0: Off
// INT1: Off
// INT2: Off
// INT3: Off
// INT4: Off
// INT5: Off
// INT6: Off
// INT7: Off
EICRA=0x00;
EICRB=0x00;
EIMSK=0x00;
//**************************************************
// Timer(s)/Counter(s) Interrupt(s) initialization
TIMSK=0x01;
ETIMSK=0x00;
//**************************************************
// USART0 initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART0 Receiver: On
// USART0 Transmitter: On
// USART0 Mode: Asynchronous
// USART0 Baud rate: 19200
UCSR0A=0x00;
UCSR0B=0xD8;
UCSR0C=0x06;
UBRR0H=0x00;
UBRR0L=0x17;
//**************************************************
// USART1 initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART1 Receiver: On
// USART1 Transmitter: On
// USART1 Mode: Asynchronous
// USART1 Baud rate: 4800
UCSR1A=0x00;
UCSR1B=0xD8;
UCSR1C=0x06;
UBRR1H=0x00;
UBRR1L=0x5F;
//**************************************************
// Analog Comparator initialization
// Analog Comparator: Off
// Analog Comparator Input Capture by Timer/Counter 1: Off
ACSR=0x80;
SFIOR=0x00;

// ADC initialization
// ADC Clock frequency: 900,600 kHz
// ADC Voltage Reference: AREF pin
// Only the 8 most significant bits of
// the AD conversion result are used
ADMUX=FIRST_ADC_INPUT|ADC_VREF_TYPE;
ADCSRA=0xCD;
//***************************************************
// Global enable interrupts
#asm("sei")

while (1)
      { 
       RD_USART1();             //Read GPS data from USART1 
       BATTERY_STATUS();        //Send msg "battery status"
       RD_USART0();             //Read incoming msg
        
       data[2] = RD_DATA_BUFFER(2);
       data[1] = RD_DATA_BUFFER(1);
       data[0] = RD_DATA_BUFFER(0);
       
       switch(data[2])
             {                                                             
             case 'M':  ///////////////////////////// MOTION ////////////////////////////// 
                                                                                                        
                if (data[1] == '!')                  // FOREWARD
                        {
                        FOREWARD = 0;
                        BACKWARD = 1;
                        i = (int)data[0];
                        DC_MOTOR ( korekcia_1 * i);
                        } 
                                        
                else if(data[1] == '%')              // BACKWARD
                        {
                        FOREWARD = 1;
                        BACKWARD = 0;
                        i = (int)data[0];
                        DC_MOTOR ( korekcia_1 * i);
                        }      
                                        
                else if(data[1] == ')')              // STOP
                        {
                        FOREWARD = 1;
                        BACKWARD = 1;
                        DC_MOTOR (63);
                        }
                break;                 
                                   
             case 'D':   ////////////////////////// DIRECTION /////////////////////////////
                 
                if(data[1] == '1')
                        { 
                        PORTC.4 = 1;                 //Direction Servo#1 enabled  ///kamil uprava
                        }
                else if(data[1] == '0')
                        { 
                        PORTC.4 = 0;                 //Direction Servo#1 disabled ///kamil uprava
                        }
                i = (int)data[0];
                SERVO1(servo_start_position + 5*i);
                i = 0;
                                      
                break;           
                                
             case 'G':   /////////////////////////////GPS /////////////////////////////////
                
                if(data[1] == '1')
                        {
                        PORTC.0 = 1;                 //enabled  ///uprava kamil PD1
                        }
                else if(data[1] == '0')
                        {
                        PORTC.0 = 0;                 //disabled ///uprava kamil PD1
                        }
                break;
                
             case 'C':   /////////////////////////// CAMERA ///////////////////////////////
                 
                if(data[1] == '1')                   //camera servo#2
                        { 
                        i = (int)data[0];      
                        SERVO2(servo_start_position + 5*i);
                        i = 0;
                        }
                else if(data[1] == '2')              //camera servo#3
                        {                                 
                        i = (int)data[0];      
                        SERVO3(servo_start_position + 5*i);
                        i = 0;            
                        }                       
                else if(data[1] == '3')              
                        { 
                        if(data[0] == '0')
                                H_V_CAM_SERVO = 0;     //servo#2,3 disabled
                        else if(data[0] == '1')
                                H_V_CAM_SERVO = 1;     //servo#2,3 enabled       
                        }   
                else if(data[1] == '5')              
                        { 
                        if(data[0] == '0')
                                CAMERA = 0;     //servo#2,3 disabled
                        else if(data[0] == '1')
                                CAMERA = 1;     //servo#2,3 enabled       
                        }
                break; 
            
             default:
                break;
             }                                       
 
      };
}
