
;CodeVisionAVR C Compiler V2.03.4 Standard
;(C) Copyright 1998-2008 Pavel Haiduc, HP InfoTech s.r.l.
;http://www.hpinfotech.com

;Chip type              : ATmega128L
;Program type           : Application
;Clock frequency        : 7,372800 MHz
;Memory model           : Small
;Optimize for           : Size
;(s)printf features     : int, width
;(s)scanf features      : int, width
;External RAM size      : 0
;Data Stack size        : 1024 byte(s)
;Heap size              : 0 byte(s)
;Promote char to int    : No
;char is unsigned       : Yes
;global const stored in FLASH  : No
;8 bit enums            : Yes
;Enhanced core instructions    : On
;Smart register allocation : On
;Automatic register allocation : On

	#pragma AVRPART ADMIN PART_NAME ATmega128L
	#pragma AVRPART MEMORY PROG_FLASH 131072
	#pragma AVRPART MEMORY EEPROM 4096
	#pragma AVRPART MEMORY INT_SRAM SIZE 4096
	#pragma AVRPART MEMORY INT_SRAM START_ADDR 0x100

	.LISTMAC
	.EQU UDRE=0x5
	.EQU RXC=0x7
	.EQU USR=0xB
	.EQU UDR=0xC
	.EQU SPSR=0xE
	.EQU SPDR=0xF
	.EQU EERE=0x0
	.EQU EEWE=0x1
	.EQU EEMWE=0x2
	.EQU EECR=0x1C
	.EQU EEDR=0x1D
	.EQU EEARL=0x1E
	.EQU EEARH=0x1F
	.EQU WDTCR=0x21
	.EQU MCUCR=0x35
	.EQU RAMPZ=0x3B
	.EQU SPL=0x3D
	.EQU SPH=0x3E
	.EQU SREG=0x3F
	.EQU XMCRA=0x6D
	.EQU XMCRB=0x6C

	.DEF R0X0=R0
	.DEF R0X1=R1
	.DEF R0X2=R2
	.DEF R0X3=R3
	.DEF R0X4=R4
	.DEF R0X5=R5
	.DEF R0X6=R6
	.DEF R0X7=R7
	.DEF R0X8=R8
	.DEF R0X9=R9
	.DEF R0XA=R10
	.DEF R0XB=R11
	.DEF R0XC=R12
	.DEF R0XD=R13
	.DEF R0XE=R14
	.DEF R0XF=R15
	.DEF R0X10=R16
	.DEF R0X11=R17
	.DEF R0X12=R18
	.DEF R0X13=R19
	.DEF R0X14=R20
	.DEF R0X15=R21
	.DEF R0X16=R22
	.DEF R0X17=R23
	.DEF R0X18=R24
	.DEF R0X19=R25
	.DEF R0X1A=R26
	.DEF R0X1B=R27
	.DEF R0X1C=R28
	.DEF R0X1D=R29
	.DEF R0X1E=R30
	.DEF R0X1F=R31

	.MACRO __CPD1N
	CPI  R30,LOW(@0)
	LDI  R26,HIGH(@0)
	CPC  R31,R26
	LDI  R26,BYTE3(@0)
	CPC  R22,R26
	LDI  R26,BYTE4(@0)
	CPC  R23,R26
	.ENDM

	.MACRO __CPD2N
	CPI  R26,LOW(@0)
	LDI  R30,HIGH(@0)
	CPC  R27,R30
	LDI  R30,BYTE3(@0)
	CPC  R24,R30
	LDI  R30,BYTE4(@0)
	CPC  R25,R30
	.ENDM

	.MACRO __CPWRR
	CP   R@0,R@2
	CPC  R@1,R@3
	.ENDM

	.MACRO __CPWRN
	CPI  R@0,LOW(@2)
	LDI  R30,HIGH(@2)
	CPC  R@1,R30
	.ENDM

	.MACRO __ADDB1MN
	SUBI R30,LOW(-@0-(@1))
	.ENDM

	.MACRO __ADDB2MN
	SUBI R26,LOW(-@0-(@1))
	.ENDM

	.MACRO __ADDW1MN
	SUBI R30,LOW(-@0-(@1))
	SBCI R31,HIGH(-@0-(@1))
	.ENDM

	.MACRO __ADDW2MN
	SUBI R26,LOW(-@0-(@1))
	SBCI R27,HIGH(-@0-(@1))
	.ENDM

	.MACRO __ADDW1FN
	SUBI R30,LOW(-2*@0-(@1))
	SBCI R31,HIGH(-2*@0-(@1))
	.ENDM

	.MACRO __ADDD1FN
	SUBI R30,LOW(-2*@0-(@1))
	SBCI R31,HIGH(-2*@0-(@1))
	SBCI R22,BYTE3(-2*@0-(@1))
	.ENDM

	.MACRO __ADDD1N
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	SBCI R22,BYTE3(-@0)
	SBCI R23,BYTE4(-@0)
	.ENDM

	.MACRO __ADDD2N
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	SBCI R24,BYTE3(-@0)
	SBCI R25,BYTE4(-@0)
	.ENDM

	.MACRO __SUBD1N
	SUBI R30,LOW(@0)
	SBCI R31,HIGH(@0)
	SBCI R22,BYTE3(@0)
	SBCI R23,BYTE4(@0)
	.ENDM

	.MACRO __SUBD2N
	SUBI R26,LOW(@0)
	SBCI R27,HIGH(@0)
	SBCI R24,BYTE3(@0)
	SBCI R25,BYTE4(@0)
	.ENDM

	.MACRO __ANDBMNN
	LDS  R30,@0+@1
	ANDI R30,LOW(@2)
	STS  @0+@1,R30
	.ENDM

	.MACRO __ANDWMNN
	LDS  R30,@0+@1
	ANDI R30,LOW(@2)
	STS  @0+@1,R30
	LDS  R30,@0+@1+1
	ANDI R30,HIGH(@2)
	STS  @0+@1+1,R30
	.ENDM

	.MACRO __ANDD1N
	ANDI R30,LOW(@0)
	ANDI R31,HIGH(@0)
	ANDI R22,BYTE3(@0)
	ANDI R23,BYTE4(@0)
	.ENDM

	.MACRO __ANDD2N
	ANDI R26,LOW(@0)
	ANDI R27,HIGH(@0)
	ANDI R24,BYTE3(@0)
	ANDI R25,BYTE4(@0)
	.ENDM

	.MACRO __ORBMNN
	LDS  R30,@0+@1
	ORI  R30,LOW(@2)
	STS  @0+@1,R30
	.ENDM

	.MACRO __ORWMNN
	LDS  R30,@0+@1
	ORI  R30,LOW(@2)
	STS  @0+@1,R30
	LDS  R30,@0+@1+1
	ORI  R30,HIGH(@2)
	STS  @0+@1+1,R30
	.ENDM

	.MACRO __ORD1N
	ORI  R30,LOW(@0)
	ORI  R31,HIGH(@0)
	ORI  R22,BYTE3(@0)
	ORI  R23,BYTE4(@0)
	.ENDM

	.MACRO __ORD2N
	ORI  R26,LOW(@0)
	ORI  R27,HIGH(@0)
	ORI  R24,BYTE3(@0)
	ORI  R25,BYTE4(@0)
	.ENDM

	.MACRO __DELAY_USB
	LDI  R24,LOW(@0)
__DELAY_USB_LOOP:
	DEC  R24
	BRNE __DELAY_USB_LOOP
	.ENDM

	.MACRO __DELAY_USW
	LDI  R24,LOW(@0)
	LDI  R25,HIGH(@0)
__DELAY_USW_LOOP:
	SBIW R24,1
	BRNE __DELAY_USW_LOOP
	.ENDM

	.MACRO __CLRD1S
	LDI  R30,0
	STD  Y+@0,R30
	STD  Y+@0+1,R30
	STD  Y+@0+2,R30
	STD  Y+@0+3,R30
	.ENDM

	.MACRO __GETD1S
	LDD  R30,Y+@0
	LDD  R31,Y+@0+1
	LDD  R22,Y+@0+2
	LDD  R23,Y+@0+3
	.ENDM

	.MACRO __PUTD1S
	STD  Y+@0,R30
	STD  Y+@0+1,R31
	STD  Y+@0+2,R22
	STD  Y+@0+3,R23
	.ENDM

	.MACRO __PUTD2S
	STD  Y+@0,R26
	STD  Y+@0+1,R27
	STD  Y+@0+2,R24
	STD  Y+@0+3,R25
	.ENDM

	.MACRO __POINTB1MN
	LDI  R30,LOW(@0+@1)
	.ENDM

	.MACRO __POINTW1MN
	LDI  R30,LOW(@0+@1)
	LDI  R31,HIGH(@0+@1)
	.ENDM

	.MACRO __POINTD1M
	LDI  R30,LOW(@0)
	LDI  R31,HIGH(@0)
	LDI  R22,BYTE3(@0)
	LDI  R23,BYTE4(@0)
	.ENDM

	.MACRO __POINTW1FN
	LDI  R30,LOW(2*@0+@1)
	LDI  R31,HIGH(2*@0+@1)
	.ENDM

	.MACRO __POINTD1FN
	LDI  R30,LOW(2*@0+@1)
	LDI  R31,HIGH(2*@0+@1)
	LDI  R22,BYTE3(2*@0+@1)
	LDI  R23,BYTE4(2*@0+@1)
	.ENDM

	.MACRO __POINTB2MN
	LDI  R26,LOW(@0+@1)
	.ENDM

	.MACRO __POINTW2MN
	LDI  R26,LOW(@0+@1)
	LDI  R27,HIGH(@0+@1)
	.ENDM

	.MACRO __POINTBRM
	LDI  R@0,LOW(@1)
	.ENDM

	.MACRO __POINTWRM
	LDI  R@0,LOW(@2)
	LDI  R@1,HIGH(@2)
	.ENDM

	.MACRO __POINTBRMN
	LDI  R@0,LOW(@1+@2)
	.ENDM

	.MACRO __POINTWRMN
	LDI  R@0,LOW(@2+@3)
	LDI  R@1,HIGH(@2+@3)
	.ENDM

	.MACRO __POINTWRFN
	LDI  R@0,LOW(@2*2+@3)
	LDI  R@1,HIGH(@2*2+@3)
	.ENDM

	.MACRO __GETD1N
	LDI  R30,LOW(@0)
	LDI  R31,HIGH(@0)
	LDI  R22,BYTE3(@0)
	LDI  R23,BYTE4(@0)
	.ENDM

	.MACRO __GETD2N
	LDI  R26,LOW(@0)
	LDI  R27,HIGH(@0)
	LDI  R24,BYTE3(@0)
	LDI  R25,BYTE4(@0)
	.ENDM

	.MACRO __GETD2S
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	LDD  R24,Y+@0+2
	LDD  R25,Y+@0+3
	.ENDM

	.MACRO __GETB1MN
	LDS  R30,@0+@1
	.ENDM

	.MACRO __GETB1HMN
	LDS  R31,@0+@1
	.ENDM

	.MACRO __GETW1MN
	LDS  R30,@0+@1
	LDS  R31,@0+@1+1
	.ENDM

	.MACRO __GETD1MN
	LDS  R30,@0+@1
	LDS  R31,@0+@1+1
	LDS  R22,@0+@1+2
	LDS  R23,@0+@1+3
	.ENDM

	.MACRO __GETBRMN
	LDS  R@0,@1+@2
	.ENDM

	.MACRO __GETWRMN
	LDS  R@0,@2+@3
	LDS  R@1,@2+@3+1
	.ENDM

	.MACRO __GETWRZ
	LDD  R@0,Z+@2
	LDD  R@1,Z+@2+1
	.ENDM

	.MACRO __GETD2Z
	LDD  R26,Z+@0
	LDD  R27,Z+@0+1
	LDD  R24,Z+@0+2
	LDD  R25,Z+@0+3
	.ENDM

	.MACRO __GETB2MN
	LDS  R26,@0+@1
	.ENDM

	.MACRO __GETW2MN
	LDS  R26,@0+@1
	LDS  R27,@0+@1+1
	.ENDM

	.MACRO __GETD2MN
	LDS  R26,@0+@1
	LDS  R27,@0+@1+1
	LDS  R24,@0+@1+2
	LDS  R25,@0+@1+3
	.ENDM

	.MACRO __PUTB1MN
	STS  @0+@1,R30
	.ENDM

	.MACRO __PUTW1MN
	STS  @0+@1,R30
	STS  @0+@1+1,R31
	.ENDM

	.MACRO __PUTD1MN
	STS  @0+@1,R30
	STS  @0+@1+1,R31
	STS  @0+@1+2,R22
	STS  @0+@1+3,R23
	.ENDM

	.MACRO __PUTB1EN
	LDI  R26,LOW(@0+@1)
	LDI  R27,HIGH(@0+@1)
	CALL __EEPROMWRB
	.ENDM

	.MACRO __PUTW1EN
	LDI  R26,LOW(@0+@1)
	LDI  R27,HIGH(@0+@1)
	CALL __EEPROMWRW
	.ENDM

	.MACRO __PUTD1EN
	LDI  R26,LOW(@0+@1)
	LDI  R27,HIGH(@0+@1)
	CALL __EEPROMWRD
	.ENDM

	.MACRO __PUTBR0MN
	STS  @0+@1,R0
	.ENDM

	.MACRO __PUTDZ2
	STD  Z+@0,R26
	STD  Z+@0+1,R27
	STD  Z+@0+2,R24
	STD  Z+@0+3,R25
	.ENDM

	.MACRO __PUTBMRN
	STS  @0+@1,R@2
	.ENDM

	.MACRO __PUTWMRN
	STS  @0+@1,R@2
	STS  @0+@1+1,R@3
	.ENDM

	.MACRO __PUTBZR
	STD  Z+@1,R@0
	.ENDM

	.MACRO __PUTWZR
	STD  Z+@2,R@0
	STD  Z+@2+1,R@1
	.ENDM

	.MACRO __GETW1R
	MOV  R30,R@0
	MOV  R31,R@1
	.ENDM

	.MACRO __GETW2R
	MOV  R26,R@0
	MOV  R27,R@1
	.ENDM

	.MACRO __GETWRN
	LDI  R@0,LOW(@2)
	LDI  R@1,HIGH(@2)
	.ENDM

	.MACRO __PUTW1R
	MOV  R@0,R30
	MOV  R@1,R31
	.ENDM

	.MACRO __PUTW2R
	MOV  R@0,R26
	MOV  R@1,R27
	.ENDM

	.MACRO __ADDWRN
	SUBI R@0,LOW(-@2)
	SBCI R@1,HIGH(-@2)
	.ENDM

	.MACRO __ADDWRR
	ADD  R@0,R@2
	ADC  R@1,R@3
	.ENDM

	.MACRO __SUBWRN
	SUBI R@0,LOW(@2)
	SBCI R@1,HIGH(@2)
	.ENDM

	.MACRO __SUBWRR
	SUB  R@0,R@2
	SBC  R@1,R@3
	.ENDM

	.MACRO __ANDWRN
	ANDI R@0,LOW(@2)
	ANDI R@1,HIGH(@2)
	.ENDM

	.MACRO __ANDWRR
	AND  R@0,R@2
	AND  R@1,R@3
	.ENDM

	.MACRO __ORWRN
	ORI  R@0,LOW(@2)
	ORI  R@1,HIGH(@2)
	.ENDM

	.MACRO __ORWRR
	OR   R@0,R@2
	OR   R@1,R@3
	.ENDM

	.MACRO __EORWRR
	EOR  R@0,R@2
	EOR  R@1,R@3
	.ENDM

	.MACRO __GETWRS
	LDD  R@0,Y+@2
	LDD  R@1,Y+@2+1
	.ENDM

	.MACRO __PUTWSR
	STD  Y+@2,R@0
	STD  Y+@2+1,R@1
	.ENDM

	.MACRO __MOVEWRR
	MOV  R@0,R@2
	MOV  R@1,R@3
	.ENDM

	.MACRO __INWR
	IN   R@0,@2
	IN   R@1,@2+1
	.ENDM

	.MACRO __OUTWR
	OUT  @2+1,R@1
	OUT  @2,R@0
	.ENDM

	.MACRO __CALL1MN
	LDS  R30,@0+@1
	LDS  R31,@0+@1+1
	ICALL
	.ENDM

	.MACRO __CALL1FN
	LDI  R30,LOW(2*@0+@1)
	LDI  R31,HIGH(2*@0+@1)
	CALL __GETW1PF
	ICALL
	.ENDM

	.MACRO __CALL2EN
	LDI  R26,LOW(@0+@1)
	LDI  R27,HIGH(@0+@1)
	CALL __EEPROMRDW
	ICALL
	.ENDM

	.MACRO __GETW1STACK
	IN   R26,SPL
	IN   R27,SPH
	ADIW R26,@0+1
	LD   R30,X+
	LD   R31,X
	.ENDM

	.MACRO __NBST
	BST  R@0,@1
	IN   R30,SREG
	LDI  R31,0x40
	EOR  R30,R31
	OUT  SREG,R30
	.ENDM


	.MACRO __PUTB1SN
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1SN
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1SN
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1SNS
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	ADIW R26,@1
	ST   X,R30
	.ENDM

	.MACRO __PUTW1SNS
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	ADIW R26,@1
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1SNS
	LDD  R26,Y+@0
	LDD  R27,Y+@0+1
	ADIW R26,@1
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1PMN
	LDS  R26,@0
	LDS  R27,@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1PMN
	LDS  R26,@0
	LDS  R27,@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1PMN
	LDS  R26,@0
	LDS  R27,@0+1
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1PMNS
	LDS  R26,@0
	LDS  R27,@0+1
	ADIW R26,@1
	ST   X,R30
	.ENDM

	.MACRO __PUTW1PMNS
	LDS  R26,@0
	LDS  R27,@0+1
	ADIW R26,@1
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1PMNS
	LDS  R26,@0
	LDS  R27,@0+1
	ADIW R26,@1
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1RN
	MOVW R26,R@0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1RN
	MOVW R26,R@0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1RN
	MOVW R26,R@0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1RNS
	MOVW R26,R@0
	ADIW R26,@1
	ST   X,R30
	.ENDM

	.MACRO __PUTW1RNS
	MOVW R26,R@0
	ADIW R26,@1
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1RNS
	MOVW R26,R@0
	ADIW R26,@1
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1RON
	MOV  R26,R@0
	MOV  R27,R@1
	SUBI R26,LOW(-@2)
	SBCI R27,HIGH(-@2)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1RON
	MOV  R26,R@0
	MOV  R27,R@1
	SUBI R26,LOW(-@2)
	SBCI R27,HIGH(-@2)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1RON
	MOV  R26,R@0
	MOV  R27,R@1
	SUBI R26,LOW(-@2)
	SBCI R27,HIGH(-@2)
	CALL __PUTDP1
	.ENDM

	.MACRO __PUTB1RONS
	MOV  R26,R@0
	MOV  R27,R@1
	ADIW R26,@2
	ST   X,R30
	.ENDM

	.MACRO __PUTW1RONS
	MOV  R26,R@0
	MOV  R27,R@1
	ADIW R26,@2
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1RONS
	MOV  R26,R@0
	MOV  R27,R@1
	ADIW R26,@2
	CALL __PUTDP1
	.ENDM


	.MACRO __GETB1SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	LD   R30,Z
	.ENDM

	.MACRO __GETB1HSX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	LD   R31,Z
	.ENDM

	.MACRO __GETW1SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	LD   R0,Z+
	LD   R31,Z
	MOV  R30,R0
	.ENDM

	.MACRO __GETD1SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	LD   R0,Z+
	LD   R1,Z+
	LD   R22,Z+
	LD   R23,Z
	MOVW R30,R0
	.ENDM

	.MACRO __GETB2SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R26,X
	.ENDM

	.MACRO __GETW2SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R0,X+
	LD   R27,X
	MOV  R26,R0
	.ENDM

	.MACRO __GETD2SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R0,X+
	LD   R1,X+
	LD   R24,X+
	LD   R25,X
	MOVW R26,R0
	.ENDM

	.MACRO __GETBRSX
	MOVW R30,R28
	SUBI R30,LOW(-@1)
	SBCI R31,HIGH(-@1)
	LD   R@0,Z
	.ENDM

	.MACRO __GETWRSX
	MOVW R30,R28
	SUBI R30,LOW(-@2)
	SBCI R31,HIGH(-@2)
	LD   R@0,Z+
	LD   R@1,Z
	.ENDM

	.MACRO __LSLW8SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	LD   R31,Z
	CLR  R30
	.ENDM

	.MACRO __PUTB1SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1SX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	ST   X+,R30
	ST   X+,R31
	ST   X+,R22
	ST   X,R23
	.ENDM

	.MACRO __CLRW1SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	CLR  R0
	ST   Z+,R0
	ST   Z,R0
	.ENDM

	.MACRO __CLRD1SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	CLR  R0
	ST   Z+,R0
	ST   Z+,R0
	ST   Z+,R0
	ST   Z,R0
	.ENDM

	.MACRO __PUTB2SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	ST   Z,R26
	.ENDM

	.MACRO __PUTW2SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	ST   Z+,R26
	ST   Z,R27
	.ENDM

	.MACRO __PUTD2SX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	ST   Z+,R26
	ST   Z+,R27
	ST   Z+,R24
	ST   Z,R25
	.ENDM

	.MACRO __PUTBSRX
	MOVW R30,R28
	SUBI R30,LOW(-@0)
	SBCI R31,HIGH(-@0)
	ST   Z,R@1
	.ENDM

	.MACRO __PUTWSRX
	MOVW R30,R28
	SUBI R30,LOW(-@2)
	SBCI R31,HIGH(-@2)
	ST   Z+,R@0
	ST   Z,R@1
	.ENDM

	.MACRO __PUTB1SNX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R0,X+
	LD   R27,X
	MOV  R26,R0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X,R30
	.ENDM

	.MACRO __PUTW1SNX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R0,X+
	LD   R27,X
	MOV  R26,R0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X+,R30
	ST   X,R31
	.ENDM

	.MACRO __PUTD1SNX
	MOVW R26,R28
	SUBI R26,LOW(-@0)
	SBCI R27,HIGH(-@0)
	LD   R0,X+
	LD   R27,X
	MOV  R26,R0
	SUBI R26,LOW(-@1)
	SBCI R27,HIGH(-@1)
	ST   X+,R30
	ST   X+,R31
	ST   X+,R22
	ST   X,R23
	.ENDM

	.MACRO __MULBRR
	MULS R@0,R@1
	MOVW R30,R0
	.ENDM

	.MACRO __MULBRRU
	MUL  R@0,R@1
	MOVW R30,R0
	.ENDM

	.MACRO __MULBRR0
	MULS R@0,R@1
	.ENDM

	.MACRO __MULBRRU0
	MUL  R@0,R@1
	.ENDM

	.MACRO __MULBNWRU
	LDI  R26,@2
	MUL  R26,R@0
	MOVW R30,R0
	MUL  R26,R@1
	ADD  R31,R0
	.ENDM

;NAME DEFINITIONS FOR GLOBAL VARIABLES ALLOCATED TO REGISTERS
	.DEF _GPS_index=R4
	.DEF _Timer_0=R6
	.DEF _status=R8
	.DEF _c_counter=R10
	.DEF _rx_wr_index1=R12

	.CSEG
	.ORG 0x00

;INTERRUPT VECTORS
	JMP  __RESET
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  _timer0_ovf_isr
	JMP  0x00
	JMP  _usart0_rx_isr
	JMP  0x00
	JMP  _usart0_tx_isr
	JMP  _adc_isr
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  _usart1_rx_isr
	JMP  0x00
	JMP  0x00
	JMP  0x00
	JMP  0x00

_tbl10_G100:
	.DB  0x10,0x27,0xE8,0x3,0x64,0x0,0xA,0x0
	.DB  0x1,0x0
_tbl16_G100:
	.DB  0x0,0x10,0x0,0x1,0x10,0x0,0x1,0x0

__RESET:
	CLI
	CLR  R30
	OUT  EECR,R30

;INTERRUPT VECTORS ARE PLACED
;AT THE START OF FLASH
	LDI  R31,1
	OUT  MCUCR,R31
	OUT  MCUCR,R30
	STS  XMCRB,R30

;DISABLE WATCHDOG
	LDI  R31,0x18
	OUT  WDTCR,R31
	OUT  WDTCR,R30

;CLEAR R2-R14
	LDI  R24,(14-2)+1
	LDI  R26,2
	CLR  R27
__CLEAR_REG:
	ST   X+,R30
	DEC  R24
	BRNE __CLEAR_REG

;CLEAR SRAM
	LDI  R24,LOW(0x1000)
	LDI  R25,HIGH(0x1000)
	LDI  R26,LOW(0x100)
	LDI  R27,HIGH(0x100)
__CLEAR_SRAM:
	ST   X+,R30
	SBIW R24,1
	BRNE __CLEAR_SRAM

;STACK POINTER INITIALIZATION
	LDI  R30,LOW(0x10FF)
	OUT  SPL,R30
	LDI  R30,HIGH(0x10FF)
	OUT  SPH,R30

;DATA STACK POINTER INITIALIZATION
	LDI  R28,LOW(0x500)
	LDI  R29,HIGH(0x500)

	JMP  _main

	.ESEG
	.ORG 0

	.DSEG
	.ORG 0x500

	.CSEG
;/*****************************************************
;This program was produced by the
;CodeWizardAVR V1.24.8d Professional
;Automatic Program Generator
;© Copyright 1998-2006 Pavel Haiduc, HP InfoTech s.r.l.
;http://www.hpinfotech.com
;
;Project : ROBOT_KAAR
;Version : 1.0.0.3
;Date    : 6.12.2007
;Author  : Stanislav Spisak
;Company : TUKE SJF KAAR
;Comments:
;
;
;Chip type           : ATmega128L
;Program type        : Application
;Clock frequency     : 7,372800 MHz
;Memory model        : Small
;External SRAM size  : 0
;Data Stack size     : 1024
;*****************************************************/
;
;#include <mega128.h>
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
;
;#include <stdio.h>
;#include <delay.h>
;
;#define RXB8 1
;#define TXB8 0
;#define UPE 2
;#define OVR 3
;#define FE 4
;#define UDRE 5
;#define RXC 7
;
;#define FRAMING_ERROR (1<<FE)
;#define PARITY_ERROR (1<<UPE)
;#define DATA_OVERRUN (1<<OVR)
;#define DATA_REGISTER_EMPTY (1<<UDRE)
;#define RX_COMPLETE (1<<RXC)
;
;#define CAMERA PORTC.7         ///uprava kamil PG2
;#define H_V_CAM_SERVO PORTC.5  ///uprava kamil P??
;#define FOREWARD PORTD.5
;#define BACKWARD PORTD.6
;#define SERVOS_PWM 9216  //50 Hz
;#define DC_MOTOR_PWM 9216 //50 Hz
;
;#define FIRST_ADC_INPUT 0
;#define LAST_ADC_INPUT 0
;
;#define RX_BUFFER_SIZE1 128
;#define GPS_BUFFER 128
;#define TX_BUFFER_SIZE0 128
;#define RX_BUFFER_SIZE0 20
;#define DATA_BYTES0 20
;
;#define ADC_VREF_TYPE 0x20
;
;
;char tx_buffer0[TX_BUFFER_SIZE0];
;char rx_buffer1[RX_BUFFER_SIZE1];
;char rx_buffer0[RX_BUFFER_SIZE0];
;char GPS_data[GPS_BUFFER];
;
;char data_bytes[DATA_BYTES0];
;char data_0[DATA_BYTES0];
;
;unsigned char adc_data[1];
;
;bit rx_buffer_overflow1;
;bit rx_buffer_overflow0;
;
;unsigned int GPS_index;
;unsigned int Timer_0;
;unsigned int status;
;unsigned int c_counter;
;unsigned int rx_wr_index1,rx_rd_index1,rx_counter1;
;unsigned int rx_wr_index0, rx_rd_index0, rx_counter0, counter;
;unsigned int tx_rd_index0,tx_counter0,tx_wr_index0;
;
;//************************************************************* ADC ******************************************************************
;
;// ADC interrupt service routine
;// with auto input scanning
;interrupt [ADC_INT] void adc_isr(void)
; 0000 0058         {

	.CSEG
_adc_isr:
	ST   -Y,R26
	ST   -Y,R27
	ST   -Y,R30
	IN   R30,SREG
	ST   -Y,R30
; 0000 0059         register unsigned char input_index=0;
; 0000 005A 
; 0000 005B         // Read the 8 most significant bits
; 0000 005C         // of the AD conversion result
; 0000 005D         adc_data[input_index]=ADCH;
	ST   -Y,R17
;	input_index -> R17
	LDI  R17,0
	MOV  R26,R17
	LDI  R27,0
	SUBI R26,LOW(-_adc_data)
	SBCI R27,HIGH(-_adc_data)
	IN   R30,0x5
	ST   X,R30
; 0000 005E         // Select next ADC input
; 0000 005F         if (++input_index > (LAST_ADC_INPUT-FIRST_ADC_INPUT))
	SUBI R17,-LOW(1)
	CPI  R17,1
	BRLO _0x3
; 0000 0060                 input_index=0;
	LDI  R17,LOW(0)
; 0000 0061         ADMUX=(FIRST_ADC_INPUT|ADC_VREF_TYPE)+input_index;
_0x3:
	MOV  R30,R17
	SUBI R30,-LOW(32)
	OUT  0x7,R30
; 0000 0062         // Start the AD conversion
; 0000 0063         ADCSRA|=0x40;
	SBI  0x6,6
; 0000 0064         }
	LD   R17,Y+
	LD   R30,Y+
	OUT  SREG,R30
	LD   R30,Y+
	LD   R27,Y+
	LD   R26,Y+
	RETI
;
;//**************************************************** RX INTERRUPT USART0 ***********************************************************
;
;// USART0 Receiver interrupt service routine
;interrupt [USART0_RXC] void usart0_rx_isr(void)
; 0000 006A         {
_usart0_rx_isr:
	CALL SUBOPT_0x0
; 0000 006B         char status,data;
; 0000 006C         status=UCSR0A;
	ST   -Y,R17
	ST   -Y,R16
;	status -> R17
;	data -> R16
	IN   R17,11
; 0000 006D         data=UDR0;
	IN   R16,12
; 0000 006E         if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN))==0)
	MOV  R30,R17
	ANDI R30,LOW(0x1C)
	BRNE _0x4
; 0000 006F                 {
; 0000 0070                 rx_buffer0[rx_wr_index0]=data;
	LDS  R30,_rx_wr_index0
	LDS  R31,_rx_wr_index0+1
	SUBI R30,LOW(-_rx_buffer0)
	SBCI R31,HIGH(-_rx_buffer0)
	ST   Z,R16
; 0000 0071                 if (++rx_wr_index0 == RX_BUFFER_SIZE0)
	LDI  R26,LOW(_rx_wr_index0)
	LDI  R27,HIGH(_rx_wr_index0)
	CALL SUBOPT_0x1
	BRNE _0x5
; 0000 0072                         rx_wr_index0=0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _rx_wr_index0,R30
	STS  _rx_wr_index0+1,R31
; 0000 0073                 if (++rx_counter0 == RX_BUFFER_SIZE0)
_0x5:
	LDI  R26,LOW(_rx_counter0)
	LDI  R27,HIGH(_rx_counter0)
	CALL SUBOPT_0x1
	BRNE _0x6
; 0000 0074                         {
; 0000 0075                         rx_counter0=0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _rx_counter0,R30
	STS  _rx_counter0+1,R31
; 0000 0076                         rx_buffer_overflow0=1;
	SET
	BLD  R2,1
; 0000 0077                         };
_0x6:
; 0000 0078                 };
_0x4:
; 0000 0079         }
	RJMP _0x6A
;
;//**************************************************** TX INTERRUPT USART0 ************************************************************
;
;// USART0 Transmitter interrupt service routine
;interrupt [USART0_TXC] void usart0_tx_isr(void)
; 0000 007F {
_usart0_tx_isr:
	CALL SUBOPT_0x0
; 0000 0080 if (tx_counter0)
	LDS  R30,_tx_counter0
	LDS  R31,_tx_counter0+1
	SBIW R30,0
	BREQ _0x7
; 0000 0081    {
; 0000 0082    --tx_counter0;
	LDI  R26,LOW(_tx_counter0)
	LDI  R27,HIGH(_tx_counter0)
	CALL SUBOPT_0x2
; 0000 0083    UDR0=tx_buffer0[tx_rd_index0];
	LDS  R30,_tx_rd_index0
	LDS  R31,_tx_rd_index0+1
	SUBI R30,LOW(-_tx_buffer0)
	SBCI R31,HIGH(-_tx_buffer0)
	LD   R30,Z
	OUT  0xC,R30
; 0000 0084    if (++tx_rd_index0 == TX_BUFFER_SIZE0)
	LDI  R26,LOW(_tx_rd_index0)
	LDI  R27,HIGH(_tx_rd_index0)
	CALL SUBOPT_0x3
	CPI  R30,LOW(0x80)
	LDI  R26,HIGH(0x80)
	CPC  R31,R26
	BRNE _0x8
; 0000 0085         tx_rd_index0=0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _tx_rd_index0,R30
	STS  _tx_rd_index0+1,R31
; 0000 0086    };
_0x8:
_0x7:
; 0000 0087 }
	RJMP _0x69
;
;//***************************************************** RD_USART0 *****************************************************************
;
;void RD_USART0(void)
; 0000 008C          {
_RD_USART0:
; 0000 008D          while(rx_counter0 != 0)
_0x9:
	LDS  R30,_rx_counter0
	LDS  R31,_rx_counter0+1
	SBIW R30,0
	BRNE PC+3
	JMP _0xB
; 0000 008E                   {
; 0000 008F                   #asm("cli")
	cli
; 0000 0090                   data_0[counter] = rx_buffer0[rx_rd_index0];
	LDS  R26,_counter
	LDS  R27,_counter+1
	SUBI R26,LOW(-_data_0)
	SBCI R27,HIGH(-_data_0)
	LDS  R30,_rx_rd_index0
	LDS  R31,_rx_rd_index0+1
	SUBI R30,LOW(-_rx_buffer0)
	SBCI R31,HIGH(-_rx_buffer0)
	LD   R30,Z
	ST   X,R30
; 0000 0091 
; 0000 0092                   if(data_0[counter - 7] == '$' && data_0[counter - 6] == 'A' && data_0[counter - 5] == '7')
	CALL SUBOPT_0x4
	SBIW R30,7
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	CPI  R30,LOW(0x24)
	BRNE _0xD
	CALL SUBOPT_0x4
	SBIW R30,6
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	CPI  R30,LOW(0x41)
	BRNE _0xD
	CALL SUBOPT_0x4
	SBIW R30,5
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	CPI  R30,LOW(0x37)
	BREQ _0xE
_0xD:
	RJMP _0xC
_0xE:
; 0000 0093                         {
; 0000 0094                         if(data_0[counter] == 10/*<LF>*/)
	CALL SUBOPT_0x4
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	CPI  R30,LOW(0xA)
	BRNE _0xF
; 0000 0095                                 {
; 0000 0096                                 data_bytes[2] = data_0[counter - 4];
	CALL SUBOPT_0x4
	SBIW R30,4
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	__PUTB1MN _data_bytes,2
; 0000 0097                                 data_bytes[1] = data_0[counter - 3];
	CALL SUBOPT_0x4
	SBIW R30,3
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	__PUTB1MN _data_bytes,1
; 0000 0098                                 data_bytes[0] = data_0[counter - 2];
	CALL SUBOPT_0x4
	SBIW R30,2
	SUBI R30,LOW(-_data_0)
	SBCI R31,HIGH(-_data_0)
	LD   R30,Z
	STS  _data_bytes,R30
; 0000 0099                                 counter = 0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _counter,R30
	STS  _counter+1,R31
; 0000 009A                                 goto here1;
	RJMP _0x10
; 0000 009B                                 }
; 0000 009C                         }
_0xF:
; 0000 009D                    counter++;
_0xC:
	LDI  R26,LOW(_counter)
	LDI  R27,HIGH(_counter)
	CALL SUBOPT_0x3
; 0000 009E                    here1:
_0x10:
; 0000 009F                    if(++rx_rd_index0 == RX_BUFFER_SIZE0)
	LDI  R26,LOW(_rx_rd_index0)
	LDI  R27,HIGH(_rx_rd_index0)
	CALL SUBOPT_0x1
	BRNE _0x11
; 0000 00A0                         rx_rd_index0 =0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _rx_rd_index0,R30
	STS  _rx_rd_index0+1,R31
; 0000 00A1                    --rx_counter0;
_0x11:
	LDI  R26,LOW(_rx_counter0)
	LDI  R27,HIGH(_rx_counter0)
	CALL SUBOPT_0x2
; 0000 00A2                   #asm("sei")
	sei
; 0000 00A3                   }
	RJMP _0x9
_0xB:
; 0000 00A4          }
	RET
;
;char RD_DATA_BUFFER(int znak)
; 0000 00A7         {
_RD_DATA_BUFFER:
; 0000 00A8         return data_bytes[znak];
;	znak -> Y+0
	LD   R30,Y
	LDD  R31,Y+1
	SUBI R30,LOW(-_data_bytes)
	SBCI R31,HIGH(-_data_bytes)
	LD   R30,Z
	RJMP _0x2060001
; 0000 00A9         }
;
;
;//************************************************** RX INTERRUPT USART1 *************************************************************
;// USART1 Receiver interrupt service routine
;interrupt [USART1_RXC] void usart1_rx_isr(void)
; 0000 00AF {
_usart1_rx_isr:
	CALL SUBOPT_0x0
; 0000 00B0 char status,data;
; 0000 00B1 status = UCSR1A;
	ST   -Y,R17
	ST   -Y,R16
;	status -> R17
;	data -> R16
	LDS  R17,155
; 0000 00B2 data = UDR1;
	LDS  R16,156
; 0000 00B3 
; 0000 00B4 if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN)) == 0)
	MOV  R30,R17
	ANDI R30,LOW(0x1C)
	BRNE _0x12
; 0000 00B5    {
; 0000 00B6    rx_buffer1[rx_wr_index1] = data;
	MOVW R30,R12
	SUBI R30,LOW(-_rx_buffer1)
	SBCI R31,HIGH(-_rx_buffer1)
	ST   Z,R16
; 0000 00B7 
; 0000 00B8    if(++rx_wr_index1 == RX_BUFFER_SIZE1)
	MOVW R30,R12
	ADIW R30,1
	MOVW R12,R30
	CPI  R30,LOW(0x80)
	LDI  R26,HIGH(0x80)
	CPC  R31,R26
	BRNE _0x13
; 0000 00B9           rx_wr_index1=0;
	CLR  R12
	CLR  R13
; 0000 00BA 
; 0000 00BB    if(++rx_counter1 == RX_BUFFER_SIZE1)
_0x13:
	LDI  R26,LOW(_rx_counter1)
	LDI  R27,HIGH(_rx_counter1)
	CALL SUBOPT_0x3
	CPI  R30,LOW(0x80)
	LDI  R26,HIGH(0x80)
	CPC  R31,R26
	BRNE _0x14
; 0000 00BC         {
; 0000 00BD         rx_counter1=0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _rx_counter1,R30
	STS  _rx_counter1+1,R31
; 0000 00BE         rx_buffer_overflow1=1;
	SET
	BLD  R2,0
; 0000 00BF         };
_0x14:
; 0000 00C0 
; 0000 00C1    };
_0x12:
; 0000 00C2 }
_0x6A:
	LD   R16,Y+
	LD   R17,Y+
_0x69:
	LD   R30,Y+
	OUT  SREG,R30
	LD   R31,Y+
	LD   R30,Y+
	LD   R27,Y+
	LD   R26,Y+
	RETI
;//*********************************************************************************************************************************
;void WR_USART0(char data_for_send)
; 0000 00C5 {
_WR_USART0:
; 0000 00C6          while (tx_counter0 == TX_BUFFER_SIZE0);
;	data_for_send -> Y+0
_0x15:
	LDS  R26,_tx_counter0
	LDS  R27,_tx_counter0+1
	CPI  R26,LOW(0x80)
	LDI  R30,HIGH(0x80)
	CPC  R27,R30
	BREQ _0x15
; 0000 00C7          #asm("cli")
	cli
; 0000 00C8          if (tx_counter0 || ((UCSR0A & DATA_REGISTER_EMPTY)==0))
	LDS  R30,_tx_counter0
	LDS  R31,_tx_counter0+1
	SBIW R30,0
	BRNE _0x19
	SBIC 0xB,5
	RJMP _0x18
_0x19:
; 0000 00C9                   {
; 0000 00CA                   tx_buffer0[tx_wr_index0] = data_for_send;
	LDS  R30,_tx_wr_index0
	LDS  R31,_tx_wr_index0+1
	SUBI R30,LOW(-_tx_buffer0)
	SBCI R31,HIGH(-_tx_buffer0)
	LD   R26,Y
	STD  Z+0,R26
; 0000 00CB                   if (++tx_wr_index0 == TX_BUFFER_SIZE0)
	LDI  R26,LOW(_tx_wr_index0)
	LDI  R27,HIGH(_tx_wr_index0)
	CALL SUBOPT_0x3
	CPI  R30,LOW(0x80)
	LDI  R26,HIGH(0x80)
	CPC  R31,R26
	BRNE _0x1B
; 0000 00CC                           tx_wr_index0=0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _tx_wr_index0,R30
	STS  _tx_wr_index0+1,R31
; 0000 00CD                   ++tx_counter0;
_0x1B:
	LDI  R26,LOW(_tx_counter0)
	LDI  R27,HIGH(_tx_counter0)
	CALL SUBOPT_0x3
; 0000 00CE                   }
; 0000 00CF          else
	RJMP _0x1C
_0x18:
; 0000 00D0                   UDR0=data_for_send;
	LD   R30,Y
	OUT  0xC,R30
; 0000 00D1          #asm("sei")
_0x1C:
	sei
; 0000 00D2 }
	ADIW R28,1
	RET
;
;void RD_USART1(void)
; 0000 00D5         {
_RD_USART1:
; 0000 00D6         while(rx_counter1 != 0)
_0x1D:
	LDS  R30,_rx_counter1
	LDS  R31,_rx_counter1+1
	SBIW R30,0
	BRNE PC+3
	JMP _0x1F
; 0000 00D7                 {
; 0000 00D8                 GPS_data[GPS_index] = rx_buffer1[rx_rd_index1];
	MOVW R26,R4
	SUBI R26,LOW(-_GPS_data)
	SBCI R27,HIGH(-_GPS_data)
	LDS  R30,_rx_rd_index1
	LDS  R31,_rx_rd_index1+1
	SUBI R30,LOW(-_rx_buffer1)
	SBCI R31,HIGH(-_rx_buffer1)
	LD   R30,Z
	ST   X,R30
; 0000 00D9 
; 0000 00DA                 #asm("cli")
	cli
; 0000 00DB                 if(GPS_data[GPS_index] == 10)
	LDI  R26,LOW(_GPS_data)
	LDI  R27,HIGH(_GPS_data)
	ADD  R26,R4
	ADC  R27,R5
	LD   R26,X
	CPI  R26,LOW(0xA)
	BRNE _0x20
; 0000 00DC                         {
; 0000 00DD                         for(c_counter = 0;c_counter <= GPS_index; c_counter++)
	CLR  R10
	CLR  R11
_0x22:
	__CPWRR 4,5,10,11
	BRLO _0x23
; 0000 00DE                                 {
; 0000 00DF                                 WR_USART0(GPS_data[c_counter]);
	LDI  R26,LOW(_GPS_data)
	LDI  R27,HIGH(_GPS_data)
	ADD  R26,R10
	ADC  R27,R11
	LD   R30,X
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 00E0                                 }
	MOVW R30,R10
	ADIW R30,1
	MOVW R10,R30
	RJMP _0x22
_0x23:
; 0000 00E1                         GPS_index = 0;
	CLR  R4
	CLR  R5
; 0000 00E2                         goto here;
	RJMP _0x24
; 0000 00E3                         };
_0x20:
; 0000 00E4 
; 0000 00E5                 GPS_index++;
	MOVW R30,R4
	ADIW R30,1
	MOVW R4,R30
; 0000 00E6                 here:
_0x24:
; 0000 00E7                 if(++rx_rd_index1 == RX_BUFFER_SIZE1)
	LDI  R26,LOW(_rx_rd_index1)
	LDI  R27,HIGH(_rx_rd_index1)
	CALL SUBOPT_0x3
	CPI  R30,LOW(0x80)
	LDI  R26,HIGH(0x80)
	CPC  R31,R26
	BRNE _0x25
; 0000 00E8                         rx_rd_index1 = 0;
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	STS  _rx_rd_index1,R30
	STS  _rx_rd_index1+1,R31
; 0000 00E9 
; 0000 00EA                 --rx_counter1;
_0x25:
	LDI  R26,LOW(_rx_counter1)
	LDI  R27,HIGH(_rx_counter1)
	CALL SUBOPT_0x2
; 0000 00EB                 #asm("sei")
	sei
; 0000 00EC                 };
	RJMP _0x1D
_0x1F:
; 0000 00ED         }
	RET
;
;//**************************************************** DEVICES ****************************************************************
;void SERVO1( int servo1 )                       //Direction servo
; 0000 00F1         {
_SERVO1:
; 0000 00F2         OCR3CH = (servo1 >> 8) & 0xFF;
;	servo1 -> Y+0
	LDD  R30,Y+1
	STS  131,R30
; 0000 00F3         OCR3CL = servo1 & 0xFF;
	LD   R30,Y
	LDD  R31,Y+1
	STS  130,R30
; 0000 00F4         }
	RJMP _0x2060001
;
;void SERVO3( int servo3 )                       //Camera servo
; 0000 00F7         {
_SERVO3:
; 0000 00F8         OCR3AH = (servo3 >> 8) & 0xFF;
;	servo3 -> Y+0
	LDD  R30,Y+1
	STS  135,R30
; 0000 00F9         OCR3AL = servo3 & 0xFF;
	LD   R30,Y
	LDD  R31,Y+1
	STS  134,R30
; 0000 00FA         }
	RJMP _0x2060001
;
;void SERVO2( int servo2 )
; 0000 00FD         {
_SERVO2:
; 0000 00FE         OCR3BH = (servo2 >> 8) & 0xFF;
;	servo2 -> Y+0
	LDD  R30,Y+1
	STS  133,R30
; 0000 00FF         OCR3BL = servo2 & 0xFF;
	LD   R30,Y
	LDD  R31,Y+1
	STS  132,R30
; 0000 0100         }
	RJMP _0x2060001
;
;void DC_MOTOR ( int dc_motor )
; 0000 0103         {
_DC_MOTOR:
; 0000 0104         OCR1BH = (dc_motor >> 8) & 0xFF;
;	dc_motor -> Y+0
	LDD  R30,Y+1
	ANDI R31,HIGH(0x0)
	OUT  0x29,R30
; 0000 0105         OCR1BL = dc_motor & 0xFF;
	LD   R30,Y
	LDD  R31,Y+1
	ANDI R31,HIGH(0xFF)
	OUT  0x28,R30
; 0000 0106         }
_0x2060001:
	ADIW R28,2
	RET
;
;void BATTERY_STATUS(void)
; 0000 0109         {
_BATTERY_STATUS:
; 0000 010A         if(status == 1)
	LDI  R30,LOW(1)
	LDI  R31,HIGH(1)
	CP   R30,R8
	CPC  R31,R9
	BRNE _0x26
; 0000 010B                 {
; 0000 010C                 WR_USART0('$');
	LDI  R30,LOW(36)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 010D                 WR_USART0('M');
	LDI  R30,LOW(77)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 010E                 WR_USART0('S');
	LDI  R30,LOW(83)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 010F                 WR_USART0('G');
	LDI  R30,LOW(71)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0110                 WR_USART0('1');
	LDI  R30,LOW(49)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0111                 WR_USART0(',');
	LDI  R30,LOW(44)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0112                 WR_USART0(adc_data[0]);
	LDS  R30,_adc_data
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0113                 WR_USART0(13);             //<CR>
	LDI  R30,LOW(13)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0114                 WR_USART0(10);             //<LF>
	LDI  R30,LOW(10)
	ST   -Y,R30
	RCALL _WR_USART0
; 0000 0115 
; 0000 0116                 status = 0;
	CLR  R8
	CLR  R9
; 0000 0117                 }
; 0000 0118         }
_0x26:
	RET
;//**************************************************** TIMER_0 ***************************************************************
; // Timer 0 overflow interrupt service routine
;interrupt [TIM0_OVF] void timer0_ovf_isr(void)
; 0000 011C {
_timer0_ovf_isr:
	ST   -Y,R30
	ST   -Y,R31
	IN   R30,SREG
	ST   -Y,R30
; 0000 011D TCNT0=0x01;
	LDI  R30,LOW(1)
	OUT  0x32,R30
; 0000 011E Timer_0++;
	MOVW R30,R6
	ADIW R30,1
	MOVW R6,R30
; 0000 011F if(Timer_0 == 28)
	LDI  R30,LOW(28)
	LDI  R31,HIGH(28)
	CP   R30,R6
	CPC  R31,R7
	BRNE _0x27
; 0000 0120         {
; 0000 0121         status = 1;
	LDI  R30,LOW(1)
	LDI  R31,HIGH(1)
	MOVW R8,R30
; 0000 0122         Timer_0 = 0;
	CLR  R6
	CLR  R7
; 0000 0123         }
; 0000 0124 }
_0x27:
	LD   R30,Y+
	OUT  SREG,R30
	LD   R31,Y+
	LD   R30,Y+
	RETI
;
;//****************************************************************************************************************************
;//***************************************************** MAIN *****************************************************************
;
;void main(void)
; 0000 012A {
_main:
; 0000 012B 
; 0000 012C int servo_start_position = 280;
; 0000 012D //int dc_motor_start_position = 0;
; 0000 012E int korekcia_1 = 146;
; 0000 012F int i;
; 0000 0130 char data[DATA_BYTES0];
; 0000 0131 
; 0000 0132 //****************************************** PORT A
; 0000 0133 // Input/Output Ports initialization
; 0000 0134 // Port A initialization
; 0000 0135 PORTA=0x00;
	SBIW R28,20
;	servo_start_position -> R16,R17
;	korekcia_1 -> R18,R19
;	i -> R20,R21
;	data -> Y+0
	__GETWRN 16,17,280
	__GETWRN 18,19,146
	LDI  R30,LOW(0)
	OUT  0x1B,R30
; 0000 0136 DDRA=0x00;
	OUT  0x1A,R30
; 0000 0137 //****************************************** PORT B
; 0000 0138 // Port B initialization
; 0000 0139 // DDRB.0 = 1;  PORTB.0 = 1; SERVO1 - power supply transistor - (normally ON) //remove
; 0000 013A // DDRB.6 = PWM OC3A   PORTE.3 = 0
; 0000 013B PORTB=0x00;    ///uprava kamil 0x01
	OUT  0x18,R30
; 0000 013C DDRB=0x40;     ///uprava kamil 0x41
	LDI  R30,LOW(64)
	OUT  0x17,R30
; 0000 013D //****************************************** PORT C
; 0000 013E //DDRC.7 = 1; PORTC.7 = 1; CAMERA ON/OFF (normally ON)
; 0000 013F PORTC=0xFF; //uprava 0xB0
	LDI  R30,LOW(255)
	OUT  0x15,R30
; 0000 0140 DDRC=0xFF;  //uprava 0xB0
	OUT  0x14,R30
; 0000 0141 //****************************************** PORT D
; 0000 0142 // Port D initialization
; 0000 0143 // DDRD.5 = 1; PORTD.5 = 0; DC_MOTOR_Direction 1/1 (normally OFF)
; 0000 0144 // DDRD.6 = 1; PORTD.6 = 0; DC_MOTOR_Direction 1/2 (normally OFF)
; 0000 0145 PORTD=0x00;
	LDI  R30,LOW(0)
	OUT  0x12,R30
; 0000 0146 DDRD=0x60;
	LDI  R30,LOW(96)
	OUT  0x11,R30
; 0000 0147 //****************************************** PORT E
; 0000 0148 // Port E initialization
; 0000 0149 // DDRE.3 = PWM OC3A   PORTE.3 = 0; SERVO#2
; 0000 014A // DDRE.4 = PWM OC3B   PORTE.4 = 0; SERVO#1
; 0000 014B // DDRE.5 = PWM OC3C   PORTE.5 = 0; SERVO#3
; 0000 014C // DDRE.6 = 1;  PORTB.6 = 1; SERVO2 - power supply transistor - (normally ON)  //remove
; 0000 014D // DDRE.7 = 1;  PORTB.7 = 1; SERVO3 - power supply transistor - (normally ON)  //remove
; 0000 014E PORTE=0x00;  ///uprava kamil 0xC0
	LDI  R30,LOW(0)
	OUT  0x3,R30
; 0000 014F DDRE=0x38;   ///uprava kamil 0xF8
	LDI  R30,LOW(56)
	OUT  0x2,R30
; 0000 0150 //****************************************** PORT F
; 0000 0151 // Port F initialization
; 0000 0152 PORTF=0x00;
	LDI  R30,LOW(0)
	STS  98,R30
; 0000 0153 DDRF=0x00;
	STS  97,R30
; 0000 0154 //****************************************** PORT G
; 0000 0155 // Port G initialization
; 0000 0156 PORTG=0x00;
	STS  101,R30
; 0000 0157 DDRG=0x00;
	STS  100,R30
; 0000 0158 //****************************************** TIMER 0
; 0000 0159 // Timer/Counter 0 initialization
; 0000 015A // Clock source: 7200Hz System Clock
; 0000 015B // Clock value: Timer 0 Stopped
; 0000 015C // Mode: Normal top=FFh
; 0000 015D // OC0 output: Disconnected
; 0000 015E ASSR=0x00;
	OUT  0x30,R30
; 0000 015F TCCR0=0x07;
	LDI  R30,LOW(7)
	OUT  0x33,R30
; 0000 0160 TCNT0=0x01;
	LDI  R30,LOW(1)
	OUT  0x32,R30
; 0000 0161 OCR0=0x00;
	LDI  R30,LOW(0)
	OUT  0x31,R30
; 0000 0162 //****************************************** TIMER 1
; 0000 0163 // Timer/Counter 1 initialization
; 0000 0164 // Clock source: System Clock
; 0000 0165 // Clock value: 7372,800 kHz
; 0000 0166 // Mode: Ph. & fr. cor. PWM top=ICR1
; 0000 0167 // OC1A output: Discon.
; 0000 0168 // OC1B output: Non-Inv.
; 0000 0169 // OC1C output: Discon.
; 0000 016A // Noise Canceler: Off
; 0000 016B // Input Capture on Falling Edge
; 0000 016C // Timer 1 Overflow Interrupt: Off
; 0000 016D // Input Capture Interrupt: Off
; 0000 016E // Compare A Match Interrupt: Off
; 0000 016F // Compare B Match Interrupt: Off
; 0000 0170 // Compare C Match Interrupt: Off
; 0000 0171 TCCR1A=0x20;
	LDI  R30,LOW(32)
	OUT  0x2F,R30
; 0000 0172 TCCR1B=0x12;
	LDI  R30,LOW(18)
	OUT  0x2E,R30
; 0000 0173 TCNT1H=0x00;
	LDI  R30,LOW(0)
	OUT  0x2D,R30
; 0000 0174 TCNT1L=0x00;
	OUT  0x2C,R30
; 0000 0175 
; 0000 0176 ICR1H =(DC_MOTOR_PWM >> 8) & 0xFF;
	LDI  R30,LOW(36)
	OUT  0x27,R30
; 0000 0177 ICR1L = DC_MOTOR_PWM & 0xFF;
	LDI  R30,LOW(0)
	OUT  0x26,R30
; 0000 0178 
; 0000 0179 DC_MOTOR(0);  //DC motor null
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	ST   -Y,R31
	ST   -Y,R30
	RCALL _DC_MOTOR
; 0000 017A 
; 0000 017B //***************************************** TIMER 2
; 0000 017C // Timer/Counter 2 initialization
; 0000 017D // Clock source: System Clock
; 0000 017E // Clock value: Timer 2 Stopped
; 0000 017F // Mode: Normal top=FFh
; 0000 0180 // OC2 output: Disconnected
; 0000 0181 TCCR2=0x00;
	LDI  R30,LOW(0)
	OUT  0x25,R30
; 0000 0182 TCNT2=0x00;
	OUT  0x24,R30
; 0000 0183 OCR2=0x00;
	OUT  0x23,R30
; 0000 0184 //***************************************** TIMER 3
; 0000 0185 // Timer/Counter 3 initialization
; 0000 0186 // Clock source: System Clock
; 0000 0187 // Clock value: 7372,800 kHz
; 0000 0188 // Mode: Ph. & fr. cor. PWM top=ICR3
; 0000 0189 // Noise Canceler: Off
; 0000 018A // Input Capture on Falling Edge
; 0000 018B // OC3A output: Discon.
; 0000 018C // OC3B output: Non-Inv.
; 0000 018D // OC3C output: Discon.
; 0000 018E // Timer 3 Overflow Interrupt: Off
; 0000 018F // Input Capture Interrupt: Off
; 0000 0190 // Compare A Match Interrupt: Off
; 0000 0191 // Compare B Match Interrupt: Off
; 0000 0192 // Compare C Match Interrupt: Off
; 0000 0193 TCCR3A=0xA8;
	LDI  R30,LOW(168)
	STS  139,R30
; 0000 0194 TCCR3B=0x12;
	LDI  R30,LOW(18)
	STS  138,R30
; 0000 0195 TCNT3H=0x00;
	LDI  R30,LOW(0)
	STS  137,R30
; 0000 0196 TCNT3L=0x00;
	STS  136,R30
; 0000 0197 
; 0000 0198 ICR3H =(SERVOS_PWM >> 8) & 0xFF;
	LDI  R30,LOW(36)
	STS  129,R30
; 0000 0199 ICR3L = SERVOS_PWM & 0xFF;
	LDI  R30,LOW(0)
	STS  128,R30
; 0000 019A 
; 0000 019B SERVO1(servo_start_position);
	ST   -Y,R17
	ST   -Y,R16
	RCALL _SERVO1
; 0000 019C SERVO2(servo_start_position);
	ST   -Y,R17
	ST   -Y,R16
	RCALL _SERVO2
; 0000 019D SERVO3(servo_start_position);
	ST   -Y,R17
	ST   -Y,R16
	RCALL _SERVO3
; 0000 019E //**************************************************
; 0000 019F 
; 0000 01A0 // External Interrupt(s) initialization
; 0000 01A1 // INT0: Off
; 0000 01A2 // INT1: Off
; 0000 01A3 // INT2: Off
; 0000 01A4 // INT3: Off
; 0000 01A5 // INT4: Off
; 0000 01A6 // INT5: Off
; 0000 01A7 // INT6: Off
; 0000 01A8 // INT7: Off
; 0000 01A9 EICRA=0x00;
	LDI  R30,LOW(0)
	STS  106,R30
; 0000 01AA EICRB=0x00;
	OUT  0x3A,R30
; 0000 01AB EIMSK=0x00;
	OUT  0x39,R30
; 0000 01AC //**************************************************
; 0000 01AD // Timer(s)/Counter(s) Interrupt(s) initialization
; 0000 01AE TIMSK=0x01;
	LDI  R30,LOW(1)
	OUT  0x37,R30
; 0000 01AF ETIMSK=0x00;
	LDI  R30,LOW(0)
	STS  125,R30
; 0000 01B0 //**************************************************
; 0000 01B1 // USART0 initialization
; 0000 01B2 // Communication Parameters: 8 Data, 1 Stop, No Parity
; 0000 01B3 // USART0 Receiver: On
; 0000 01B4 // USART0 Transmitter: On
; 0000 01B5 // USART0 Mode: Asynchronous
; 0000 01B6 // USART0 Baud rate: 19200
; 0000 01B7 UCSR0A=0x00;
	OUT  0xB,R30
; 0000 01B8 UCSR0B=0xD8;
	LDI  R30,LOW(216)
	OUT  0xA,R30
; 0000 01B9 UCSR0C=0x06;
	LDI  R30,LOW(6)
	STS  149,R30
; 0000 01BA UBRR0H=0x00;
	LDI  R30,LOW(0)
	STS  144,R30
; 0000 01BB UBRR0L=0x17;
	LDI  R30,LOW(23)
	OUT  0x9,R30
; 0000 01BC //**************************************************
; 0000 01BD // USART1 initialization
; 0000 01BE // Communication Parameters: 8 Data, 1 Stop, No Parity
; 0000 01BF // USART1 Receiver: On
; 0000 01C0 // USART1 Transmitter: On
; 0000 01C1 // USART1 Mode: Asynchronous
; 0000 01C2 // USART1 Baud rate: 4800
; 0000 01C3 UCSR1A=0x00;
	LDI  R30,LOW(0)
	STS  155,R30
; 0000 01C4 UCSR1B=0xD8;
	LDI  R30,LOW(216)
	STS  154,R30
; 0000 01C5 UCSR1C=0x06;
	LDI  R30,LOW(6)
	STS  157,R30
; 0000 01C6 UBRR1H=0x00;
	LDI  R30,LOW(0)
	STS  152,R30
; 0000 01C7 UBRR1L=0x5F;
	LDI  R30,LOW(95)
	STS  153,R30
; 0000 01C8 //**************************************************
; 0000 01C9 // Analog Comparator initialization
; 0000 01CA // Analog Comparator: Off
; 0000 01CB // Analog Comparator Input Capture by Timer/Counter 1: Off
; 0000 01CC ACSR=0x80;
	LDI  R30,LOW(128)
	OUT  0x8,R30
; 0000 01CD SFIOR=0x00;
	LDI  R30,LOW(0)
	OUT  0x20,R30
; 0000 01CE 
; 0000 01CF // ADC initialization
; 0000 01D0 // ADC Clock frequency: 900,600 kHz
; 0000 01D1 // ADC Voltage Reference: AREF pin
; 0000 01D2 // Only the 8 most significant bits of
; 0000 01D3 // the AD conversion result are used
; 0000 01D4 ADMUX=FIRST_ADC_INPUT|ADC_VREF_TYPE;
	LDI  R30,LOW(32)
	OUT  0x7,R30
; 0000 01D5 ADCSRA=0xCD;
	LDI  R30,LOW(205)
	OUT  0x6,R30
; 0000 01D6 //***************************************************
; 0000 01D7 // Global enable interrupts
; 0000 01D8 #asm("sei")
	sei
; 0000 01D9 
; 0000 01DA while (1)
_0x28:
; 0000 01DB       {
; 0000 01DC        RD_USART1();             //Read GPS data from USART1
	RCALL _RD_USART1
; 0000 01DD        BATTERY_STATUS();        //Send msg "battery status"
	RCALL _BATTERY_STATUS
; 0000 01DE        RD_USART0();             //Read incoming msg
	RCALL _RD_USART0
; 0000 01DF 
; 0000 01E0        data[2] = RD_DATA_BUFFER(2);
	LDI  R30,LOW(2)
	LDI  R31,HIGH(2)
	CALL SUBOPT_0x5
	STD  Y+2,R30
; 0000 01E1        data[1] = RD_DATA_BUFFER(1);
	LDI  R30,LOW(1)
	LDI  R31,HIGH(1)
	CALL SUBOPT_0x5
	STD  Y+1,R30
; 0000 01E2        data[0] = RD_DATA_BUFFER(0);
	LDI  R30,LOW(0)
	LDI  R31,HIGH(0)
	CALL SUBOPT_0x5
	ST   Y,R30
; 0000 01E3 
; 0000 01E4        switch(data[2])
	LDD  R30,Y+2
; 0000 01E5              {
; 0000 01E6              case 'M':  ///////////////////////////// MOTION //////////////////////////////
	CPI  R30,LOW(0x4D)
	BRNE _0x2E
; 0000 01E7 
; 0000 01E8                 if (data[1] == '!')                  // FOREWARD
	LDD  R26,Y+1
	CPI  R26,LOW(0x21)
	BRNE _0x2F
; 0000 01E9                         {
; 0000 01EA                         FOREWARD = 0;
	CBI  0x12,5
; 0000 01EB                         BACKWARD = 1;
	SBI  0x12,6
; 0000 01EC                         i = (int)data[0];
	CALL SUBOPT_0x6
; 0000 01ED                         DC_MOTOR ( korekcia_1 * i);
	RJMP _0x68
; 0000 01EE                         }
; 0000 01EF 
; 0000 01F0                 else if(data[1] == '%')              // BACKWARD
_0x2F:
	LDD  R26,Y+1
	CPI  R26,LOW(0x25)
	BRNE _0x35
; 0000 01F1                         {
; 0000 01F2                         FOREWARD = 1;
	SBI  0x12,5
; 0000 01F3                         BACKWARD = 0;
	CBI  0x12,6
; 0000 01F4                         i = (int)data[0];
	CALL SUBOPT_0x6
; 0000 01F5                         DC_MOTOR ( korekcia_1 * i);
	RJMP _0x68
; 0000 01F6                         }
; 0000 01F7 
; 0000 01F8                 else if(data[1] == ')')              // STOP
_0x35:
	LDD  R26,Y+1
	CPI  R26,LOW(0x29)
	BRNE _0x3B
; 0000 01F9                         {
; 0000 01FA                         FOREWARD = 1;
	SBI  0x12,5
; 0000 01FB                         BACKWARD = 1;
	SBI  0x12,6
; 0000 01FC                         DC_MOTOR (63);
	LDI  R30,LOW(63)
	LDI  R31,HIGH(63)
_0x68:
	ST   -Y,R31
	ST   -Y,R30
	RCALL _DC_MOTOR
; 0000 01FD                         }
; 0000 01FE                 break;
_0x3B:
	RJMP _0x2D
; 0000 01FF 
; 0000 0200              case 'D':   ////////////////////////// DIRECTION /////////////////////////////
_0x2E:
	CPI  R30,LOW(0x44)
	BRNE _0x40
; 0000 0201 
; 0000 0202                 if(data[1] == '1')
	LDD  R26,Y+1
	CPI  R26,LOW(0x31)
	BRNE _0x41
; 0000 0203                         {
; 0000 0204                         PORTC.4 = 1;                 //Direction Servo#1 enabled  ///kamil uprava
	SBI  0x15,4
; 0000 0205                         }
; 0000 0206                 else if(data[1] == '0')
	RJMP _0x44
_0x41:
	LDD  R26,Y+1
	CPI  R26,LOW(0x30)
	BRNE _0x45
; 0000 0207                         {
; 0000 0208                         PORTC.4 = 0;                 //Direction Servo#1 disabled ///kamil uprava
	CBI  0x15,4
; 0000 0209                         }
; 0000 020A                 i = (int)data[0];
_0x45:
_0x44:
	CALL SUBOPT_0x7
; 0000 020B                 SERVO1(servo_start_position + 5*i);
	RCALL _SERVO1
; 0000 020C                 i = 0;
	__GETWRN 20,21,0
; 0000 020D 
; 0000 020E                 break;
	RJMP _0x2D
; 0000 020F 
; 0000 0210              case 'G':   /////////////////////////////GPS /////////////////////////////////
_0x40:
	CPI  R30,LOW(0x47)
	BRNE _0x48
; 0000 0211 
; 0000 0212                 if(data[1] == '1')
	LDD  R26,Y+1
	CPI  R26,LOW(0x31)
	BRNE _0x49
; 0000 0213                         {
; 0000 0214                         PORTC.0 = 1;                 //enabled  ///uprava kamil PD1
	SBI  0x15,0
; 0000 0215                         }
; 0000 0216                 else if(data[1] == '0')
	RJMP _0x4C
_0x49:
	LDD  R26,Y+1
	CPI  R26,LOW(0x30)
	BRNE _0x4D
; 0000 0217                         {
; 0000 0218                         PORTC.0 = 0;                 //disabled ///uprava kamil PD1
	CBI  0x15,0
; 0000 0219                         }
; 0000 021A                 break;
_0x4D:
_0x4C:
	RJMP _0x2D
; 0000 021B 
; 0000 021C              case 'C':   /////////////////////////// CAMERA ///////////////////////////////
_0x48:
	CPI  R30,LOW(0x43)
	BRNE _0x66
; 0000 021D 
; 0000 021E                 if(data[1] == '1')                   //camera servo#2
	LDD  R26,Y+1
	CPI  R26,LOW(0x31)
	BRNE _0x51
; 0000 021F                         {
; 0000 0220                         i = (int)data[0];
	CALL SUBOPT_0x7
; 0000 0221                         SERVO2(servo_start_position + 5*i);
	RCALL _SERVO2
; 0000 0222                         i = 0;
	__GETWRN 20,21,0
; 0000 0223                         }
; 0000 0224                 else if(data[1] == '2')              //camera servo#3
	RJMP _0x52
_0x51:
	LDD  R26,Y+1
	CPI  R26,LOW(0x32)
	BRNE _0x53
; 0000 0225                         {
; 0000 0226                         i = (int)data[0];
	CALL SUBOPT_0x7
; 0000 0227                         SERVO3(servo_start_position + 5*i);
	RCALL _SERVO3
; 0000 0228                         i = 0;
	__GETWRN 20,21,0
; 0000 0229                         }
; 0000 022A                 else if(data[1] == '3')
	RJMP _0x54
_0x53:
	LDD  R26,Y+1
	CPI  R26,LOW(0x33)
	BRNE _0x55
; 0000 022B                         {
; 0000 022C                         if(data[0] == '0')
	LD   R26,Y
	CPI  R26,LOW(0x30)
	BRNE _0x56
; 0000 022D                                 H_V_CAM_SERVO = 0;     //servo#2,3 disabled
	CBI  0x15,5
; 0000 022E                         else if(data[0] == '1')
	RJMP _0x59
_0x56:
	LD   R26,Y
	CPI  R26,LOW(0x31)
	BRNE _0x5A
; 0000 022F                                 H_V_CAM_SERVO = 1;     //servo#2,3 enabled
	SBI  0x15,5
; 0000 0230                         }
_0x5A:
_0x59:
; 0000 0231                 else if(data[1] == '5')
	RJMP _0x5D
_0x55:
	LDD  R26,Y+1
	CPI  R26,LOW(0x35)
	BRNE _0x5E
; 0000 0232                         {
; 0000 0233                         if(data[0] == '0')
	LD   R26,Y
	CPI  R26,LOW(0x30)
	BRNE _0x5F
; 0000 0234                                 CAMERA = 0;     //servo#2,3 disabled
	CBI  0x15,7
; 0000 0235                         else if(data[0] == '1')
	RJMP _0x62
_0x5F:
	LD   R26,Y
	CPI  R26,LOW(0x31)
	BRNE _0x63
; 0000 0236                                 CAMERA = 1;     //servo#2,3 enabled
	SBI  0x15,7
; 0000 0237                         }
_0x63:
_0x62:
; 0000 0238                 break;
_0x5E:
_0x5D:
_0x54:
_0x52:
; 0000 0239 
; 0000 023A              default:
_0x66:
; 0000 023B                 break;
; 0000 023C              }
_0x2D:
; 0000 023D 
; 0000 023E       };
	RJMP _0x28
; 0000 023F }
_0x67:
	RJMP _0x67
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

	.CSEG

	.CSEG

	.CSEG

	.DSEG
_tx_buffer0:
	.BYTE 0x80
_rx_buffer1:
	.BYTE 0x80
_rx_buffer0:
	.BYTE 0x14
_GPS_data:
	.BYTE 0x80
_data_bytes:
	.BYTE 0x14
_data_0:
	.BYTE 0x14
_adc_data:
	.BYTE 0x1
_rx_rd_index1:
	.BYTE 0x2
_rx_counter1:
	.BYTE 0x2
_rx_wr_index0:
	.BYTE 0x2
_rx_rd_index0:
	.BYTE 0x2
_rx_counter0:
	.BYTE 0x2
_counter:
	.BYTE 0x2
_tx_rd_index0:
	.BYTE 0x2
_tx_counter0:
	.BYTE 0x2
_tx_wr_index0:
	.BYTE 0x2
_p_S1020024:
	.BYTE 0x2

	.CSEG
;OPTIMIZER ADDED SUBROUTINE, CALLED 3 TIMES, CODE SIZE REDUCTION:5 WORDS
SUBOPT_0x0:
	ST   -Y,R26
	ST   -Y,R27
	ST   -Y,R30
	ST   -Y,R31
	IN   R30,SREG
	ST   -Y,R30
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 3 TIMES, CODE SIZE REDUCTION:5 WORDS
SUBOPT_0x1:
	LD   R30,X+
	LD   R31,X+
	ADIW R30,1
	ST   -X,R31
	ST   -X,R30
	SBIW R30,20
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 3 TIMES, CODE SIZE REDUCTION:3 WORDS
SUBOPT_0x2:
	LD   R30,X+
	LD   R31,X+
	SBIW R30,1
	ST   -X,R31
	ST   -X,R30
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 6 TIMES, CODE SIZE REDUCTION:12 WORDS
SUBOPT_0x3:
	LD   R30,X+
	LD   R31,X+
	ADIW R30,1
	ST   -X,R31
	ST   -X,R30
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 7 TIMES, CODE SIZE REDUCTION:9 WORDS
SUBOPT_0x4:
	LDS  R30,_counter
	LDS  R31,_counter+1
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 3 TIMES, CODE SIZE REDUCTION:1 WORDS
SUBOPT_0x5:
	ST   -Y,R31
	ST   -Y,R30
	JMP  _RD_DATA_BUFFER

;OPTIMIZER ADDED SUBROUTINE, CALLED 2 TIMES, CODE SIZE REDUCTION:1 WORDS
SUBOPT_0x6:
	LDD  R20,Y+0
	CLR  R21
	MOVW R30,R20
	MOVW R26,R18
	CALL __MULW12
	RET

;OPTIMIZER ADDED SUBROUTINE, CALLED 3 TIMES, CODE SIZE REDUCTION:15 WORDS
SUBOPT_0x7:
	LDD  R20,Y+0
	CLR  R21
	MOVW R30,R20
	LDI  R26,LOW(5)
	LDI  R27,HIGH(5)
	CALL __MULW12
	ADD  R30,R16
	ADC  R31,R17
	ST   -Y,R31
	ST   -Y,R30
	RET


	.CSEG
__ANEGW1:
	NEG  R31
	NEG  R30
	SBCI R31,0
	RET

__MULW12U:
	MUL  R31,R26
	MOV  R31,R0
	MUL  R30,R27
	ADD  R31,R0
	MUL  R30,R26
	MOV  R30,R0
	ADD  R31,R1
	RET

__MULW12:
	RCALL __CHKSIGNW
	RCALL __MULW12U
	BRTC __MULW121
	RCALL __ANEGW1
__MULW121:
	RET

__CHKSIGNW:
	CLT
	SBRS R31,7
	RJMP __CHKSW1
	RCALL __ANEGW1
	SET
__CHKSW1:
	SBRS R27,7
	RJMP __CHKSW2
	COM  R26
	COM  R27
	ADIW R26,1
	BLD  R0,0
	INC  R0
	BST  R0,0
__CHKSW2:
	RET

;END OF CODE MARKER
__END_OF_CODE:
