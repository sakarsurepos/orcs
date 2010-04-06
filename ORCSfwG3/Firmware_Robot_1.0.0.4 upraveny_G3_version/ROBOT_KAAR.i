
#pragma used+
sfrb PINF=0;
sfrb PINE=1;
sfrb DDRE=2;
sfrb PORTE=3;
sfrb ADCL=4;
sfrb ADCH=5;
sfrw ADCW=4;      
sfrb ADCSRA=6;
sfrb ADMUX=7;
sfrb ACSR=8;
sfrb UBRR0L=9;
sfrb UCSR0B=0xa;
sfrb UCSR0A=0xb;
sfrb UDR0=0xc;
sfrb SPCR=0xd;
sfrb SPSR=0xe;
sfrb SPDR=0xf;
sfrb PIND=0x10;
sfrb DDRD=0x11;
sfrb PORTD=0x12;
sfrb PINC=0x13;
sfrb DDRC=0x14;
sfrb PORTC=0x15;
sfrb PINB=0x16;
sfrb DDRB=0x17;
sfrb PORTB=0x18;
sfrb PINA=0x19;
sfrb DDRA=0x1a;
sfrb PORTA=0x1b;
sfrb EECR=0x1c;
sfrb EEDR=0x1d;
sfrb EEARL=0x1e;
sfrb EEARH=0x1f;
sfrw EEAR=0x1e;   
sfrb SFIOR=0x20;
sfrb WDTCR=0x21;
sfrb OCDR=0x22;
sfrb OCR2=0x23;
sfrb TCNT2=0x24;
sfrb TCCR2=0x25;
sfrb ICR1L=0x26;
sfrb ICR1H=0x27;
sfrw ICR1=0x26;   
sfrb OCR1BL=0x28;
sfrb OCR1BH=0x29;
sfrw OCR1B=0x28;  
sfrb OCR1AL=0x2a;
sfrb OCR1AH=0x2b;
sfrw OCR1A=0x2a;  
sfrb TCNT1L=0x2c;
sfrb TCNT1H=0x2d;
sfrw TCNT1=0x2c;  
sfrb TCCR1B=0x2e;
sfrb TCCR1A=0x2f;
sfrb ASSR=0x30;
sfrb OCR0=0x31;
sfrb TCNT0=0x32;
sfrb TCCR0=0x33;
sfrb MCUCSR=0x34;
sfrb MCUCR=0x35;
sfrb TIFR=0x36;
sfrb TIMSK=0x37;
sfrb EIFR=0x38;
sfrb EIMSK=0x39;
sfrb EICRB=0x3a;
sfrb RAMPZ=0x3b;
sfrb XDIV=0x3c;
sfrb SPL=0x3d;
sfrb SPH=0x3e;
sfrb SREG=0x3f;
#pragma used-

#asm
	#ifndef __SLEEP_DEFINED__
	#define __SLEEP_DEFINED__
	.EQU __se_bit=0x20
	.EQU __sm_mask=0x1C
	.EQU __sm_powerdown=0x10
	.EQU __sm_powersave=0x18
	.EQU __sm_standby=0x14
	.EQU __sm_ext_standby=0x1C
	.EQU __sm_adc_noise_red=0x08
	.SET power_ctrl_reg=mcucr
	#endif
#endasm

typedef char *va_list;

#pragma used+

char getchar(void);
void putchar(char c);
void puts(char *str);
void putsf(char flash *str);

char *gets(char *str,unsigned int len);

void printf(char flash *fmtstr,...);
void sprintf(char *str, char flash *fmtstr,...);
void snprintf(char *str, unsigned int size, char flash *fmtstr,...);
void vprintf (char flash * fmtstr, va_list argptr);
void vsprintf (char *str, char flash * fmtstr, va_list argptr);
void vsnprintf (char *str, unsigned int size, char flash * fmtstr, va_list argptr);
signed char scanf(char flash *fmtstr,...);
signed char sscanf(char *str, char flash *fmtstr,...);

#pragma used-

#pragma library stdio.lib

#pragma used+

void delay_us(unsigned int n);
void delay_ms(unsigned int n);

#pragma used-

char tx_buffer0[128];
char rx_buffer1[128];
char rx_buffer0[20];
char GPS_data[128];

char data_bytes[20];
char data_0[20];

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

interrupt [22] void adc_isr(void)
{
register unsigned char input_index=0;

adc_data[input_index]=ADCH;

if (++input_index > (0 -0))
input_index=0;
ADMUX=(0|0x20)+input_index;

ADCSRA|=0x40;       
}

interrupt [19] void usart0_rx_isr(void)
{
char status,data;
status=UCSR0A;
data=UDR0;
if ((status & ((1<<4) | (1<<2) | (1<<3)))==0)
{      
rx_buffer0[rx_wr_index0]=data;
if (++rx_wr_index0 == 20) 
rx_wr_index0=0;
if (++rx_counter0 == 20)
{
rx_counter0=0;
rx_buffer_overflow0=1;
};               
};
}

interrupt [21] void usart0_tx_isr(void)
{
if (tx_counter0)
{
--tx_counter0;
UDR0=tx_buffer0[tx_rd_index0];
if (++tx_rd_index0 == 128) 
tx_rd_index0=0;
};
}

void RD_USART0(void)
{
while(rx_counter0 != 0)
{
#asm("cli")
data_0[counter] = rx_buffer0[rx_rd_index0];

if(data_0[counter - 7] == '$' && data_0[counter - 6] == 'A' && data_0[counter - 5] == '7')
{ 
if(data_0[counter] == 10)
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
if(++rx_rd_index0 == 20)
rx_rd_index0 =0;
--rx_counter0;
#asm("sei")
}
}

char RD_DATA_BUFFER(int znak)
{
return data_bytes[znak];
}

interrupt [31] void usart1_rx_isr(void)
{
char status,data;
status = (*(unsigned char *) 0x9b);
data = (*(unsigned char *) 0x9c);

if ((status & ((1<<4) | (1<<2) | (1<<3))) == 0)
{
rx_buffer1[rx_wr_index1] = data;

if(++rx_wr_index1 == 128)
rx_wr_index1=0;

if(++rx_counter1 == 128)
{
rx_counter1=0;
rx_buffer_overflow1=1;
};

};
} 

void WR_USART0(char data_for_send)
{
while (tx_counter0 == 128);
#asm("cli")
if (tx_counter0 || ((UCSR0A & (1<<5))==0))
{
tx_buffer0[tx_wr_index0] = data_for_send;
if (++tx_wr_index0 == 128) 
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
if(++rx_rd_index1 == 128)
rx_rd_index1 = 0;

--rx_counter1;
#asm("sei")
};
}

void SERVO1( int servo1 )                       
{
(*(unsigned char *) 0x83) = (servo1 >> 8) & 0xFF;
(*(unsigned char *) 0x82) = servo1 & 0xFF;
} 

void SERVO3( int servo3 )                       
{
(*(unsigned char *) 0x87) = (servo3 >> 8) & 0xFF;
(*(unsigned char *) 0x86) = servo3 & 0xFF;
} 

void SERVO2( int servo2 )
{
(*(unsigned char *) 0x85) = (servo2 >> 8) & 0xFF;
(*(unsigned char *) 0x84) = servo2 & 0xFF;
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
WR_USART0(13);             
WR_USART0(10);             

status = 0;
}
}               

interrupt [17] void timer0_ovf_isr(void)
{
TCNT0=0x01;
Timer_0++; 
if(Timer_0 == 28)
{
status = 1;
Timer_0 = 0;
}     
} 

void main(void)
{ 

int servo_start_position = 280;

int korekcia_1 = 146;
int i;
char data[20];

PORTA=0x00;
DDRA=0x00;

PORTB=0x00;    
DDRB=0x40;     

PORTC=0xFF; 
DDRC=0xFF;  

PORTD=0x00;
DDRD=0x60;

PORTE=0x00;  
DDRE=0x38;   

(*(unsigned char *) 0x62)=0x00;
(*(unsigned char *) 0x61)=0x00;

(*(unsigned char *) 0x65)=0x00;
(*(unsigned char *) 0x64)=0x00;

ASSR=0x00;
TCCR0=0x07;
TCNT0=0x01;
OCR0=0x00;

TCCR1A=0x20;
TCCR1B=0x12;
TCNT1H=0x00;
TCNT1L=0x00;

ICR1H =(9216  >> 8) & 0xFF;
ICR1L = 9216  & 0xFF;

DC_MOTOR(0);  

TCCR2=0x00;
TCNT2=0x00;
OCR2=0x00;

(*(unsigned char *) 0x8b)=0xA8;
(*(unsigned char *) 0x8a)=0x12;
(*(unsigned char *) 0x89)=0x00;
(*(unsigned char *) 0x88)=0x00;

(*(unsigned char *) 0x81) =(9216   >> 8) & 0xFF;
(*(unsigned char *) 0x80) = 9216   & 0xFF;

SERVO1(servo_start_position);
SERVO2(servo_start_position);
SERVO3(servo_start_position);

(*(unsigned char *) 0x6a)=0x00;
EICRB=0x00;
EIMSK=0x00;

TIMSK=0x01;
(*(unsigned char *) 0x7d)=0x00;

UCSR0A=0x00;
UCSR0B=0xD8;
(*(unsigned char *) 0x95)=0x06;
(*(unsigned char *) 0x90)=0x00;
UBRR0L=0x17;

(*(unsigned char *) 0x9b)=0x00;
(*(unsigned char *) 0x9a)=0xD8;
(*(unsigned char *) 0x9d)=0x06;
(*(unsigned char *) 0x98)=0x00;
(*(unsigned char *) 0x99)=0x5F;

ACSR=0x80;
SFIOR=0x00;

ADMUX=0|0x20;
ADCSRA=0xCD;

#asm("sei")

while (1)
{ 
RD_USART1();             
BATTERY_STATUS();        
RD_USART0();             

data[2] = RD_DATA_BUFFER(2);
data[1] = RD_DATA_BUFFER(1);
data[0] = RD_DATA_BUFFER(0);

switch(data[2])
{                                                             
case 'M':  

if (data[1] == '!')                  
{
PORTD.5 = 0;
PORTD.6 = 1;
i = (int)data[0];
DC_MOTOR ( korekcia_1 * i);
} 

else if(data[1] == '%')              
{
PORTD.5 = 1;
PORTD.6 = 0;
i = (int)data[0];
DC_MOTOR ( korekcia_1 * i);
}      

else if(data[1] == ')')              
{
PORTD.5 = 1;
PORTD.6 = 1;
DC_MOTOR (63);
}
break;                 

case 'D':   

if(data[1] == '1')
{ 
PORTC.4 = 1;                 
}
else if(data[1] == '0')
{ 
PORTC.4 = 0;                 
}
i = (int)data[0];
SERVO1(servo_start_position + 5*i);
i = 0;

break;           

case 'G':   

if(data[1] == '1')
{
PORTC.0 = 1;                 
}
else if(data[1] == '0')
{
PORTC.0 = 0;                 
}
break;

case 'C':   

if(data[1] == '1')                   
{ 
i = (int)data[0];      
SERVO2(servo_start_position + 5*i);
i = 0;
}
else if(data[1] == '2')              
{                                 
i = (int)data[0];      
SERVO3(servo_start_position + 5*i);
i = 0;            
}                       
else if(data[1] == '3')              
{ 
if(data[0] == '0')
PORTC.5   = 0;     
else if(data[0] == '1')
PORTC.5   = 1;     
}   
else if(data[1] == '5')              
{ 
if(data[0] == '0')
PORTC.7          = 0;     
else if(data[0] == '1')
PORTC.7          = 1;     
}
break; 

default:
break;
}                                       

};
}
