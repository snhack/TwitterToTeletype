#define F_CPU 1000000UL
#define BAUD 9600
#define TTYBAUDRATE 110
#define DELAY_CYCLES 7
#define _tx_delay  (F_CPU/TTYBAUDRATE/DELAY_CYCLES)
#define DEBUG 1
#include <util/delay.h>
#include <avr/io.h>
#include <stdio.h>
#include <util/setbaud.h>

// Some macros that make the code more readable
#define output_low(port,pin) port &= ~(1<<pin)
#define output_high(port,pin) port |= (1<<pin)
//#define set_input(portdir,pin) portdir &= ~(1<<pin)
#define set_output(portdir,pin) portdir |= (1<<pin)

void tunedDelay(uint16_t delay) {
	uint8_t tmp=0;
	asm volatile("sbiw    %0, 0x01 \n\t"
	"ldi %1, 0xFF \n\t"
	"cpi %A0, 0xFF \n\t"
	"cpc %B0, %1 \n\t"
	"brne .-10 \n\t"
	: "+w" (delay), "+a" (tmp)
	: "0" (delay)
	);
}
inline void uart_init(void) {
	UBRRH = UBRRH_VALUE;
	UBRRL = UBRRL_VALUE;
	#if USE_2X
	UCSRA |= _BV(U2X);
	#else
	UCSRA &= ~(_BV(U2X));
	#endif
	UCSRC = _BV(UCSZ1) | _BV(UCSZ0); /* 8-bit data */
	_delay_ms(100);
	UCSRB = _BV(RXEN) | _BV(TXEN);   /* Enable RX and TX */
	_delay_ms(100);
}
static const char *POWER_ON = "<<";
static const char *POWER_OFF = ">>";

#ifdef DEBUG
static const char *START_MESSAGE = "Ready.";
#endif

static const char NEWLINE = '\n';
static const char CR = '\r';
static const char NULLCHAR = '\0';

#ifdef DEBUG
int uart_puts(const char *str) {
	while(*str) {
		char c = *str++;
		if (c == NEWLINE) {
			c = CR;
		}
		loop_until_bit_is_set(UCSRA, UDRE);
		UDR = c;
	}
	return 0;
}
#endif
/************************************************************************/
/* Buffer to hold the input command.                                    */
/************************************************************************/
char buffer[]= "This is a string of 51 characters - 50 data + null.";;
/************************************************************************/
/* Set bit 7 if there are an odd number of 1's in bits 0 to 6           */
/************************************************************************/
inline uint8_t evenParity( uint8_t b )
{
	b &= 0x7F;// clear the high bit
	for(uint8_t mask = 1;mask < 0x80;mask <<= 1) {
		if(b & mask) {
			b ^=0x80; // XOR the MSB for each 1 in the byte
		}
	}
	return b;
}
/************************************************************************/
/* Hacked from SoftwareSerial                                           */
/************************************************************************/
size_t write(uint8_t b)
{
	//
	// Set bit 7 if there are an odd number of 1's in bits 0 to 6
	//
	//b = evenParity(b);
	// Write the start bit
	output_high(PORTD, PD3);
	tunedDelay(_tx_delay);
	// Write each of the 8 bits
	for (uint8_t mask = 0x01; mask; mask <<= 1)
	{
		if (b & mask) // choose bit
		output_low(PORTD, PD3);
		else
		output_high(PORTD, PD3);
		tunedDelay(_tx_delay);
	}
	// two stop bits
	output_low(PORTD, PD3);
	tunedDelay(_tx_delay*4);// *4 gives 2 stop bits plus 2 extra bit periods for the TTY to 'recover'
	return 1;
}
/************************************************************************/
/* Function to send the current buffer contents to the teletype         */
/* (expects a null terminated string)                                   */
/************************************************************************/
inline void sendDataToTTY() {
	char *nextChar = buffer;
	while(*nextChar) {
		write(*nextChar++);
	}
}
/************************************************************************/
/* Slightly shorter than strncmp                                        */
/************************************************************************/
int same(const char *first, char* second, uint8_t len) {
	while(len >0 && *first++ == *second++) {
		len--;
	}
	return len;
}
/*****************************************************************************/
/* Interpret the command and turn the TTY on or OFF, or send data to the TTY */
/*****************************************************************************/
void interpretCommand() {
	//
	// Echo the command back to the client.
	//
	#ifdef DEBUG
	uart_puts(buffer);
	#endif
	if (same(POWER_ON, buffer, 2) == 0)
	{
		output_high(PORTD, PD2);
	}
	else if (same(POWER_OFF, buffer, 2) == 0)
	{
		output_low(PORTD, PD2);
	}
	else {
		sendDataToTTY();
	}
}
/************************************************************************/
/* Program Entry Point                                                  */
/************************************************************************/
int main(void)
{
	//
	// Set up port pins
	//
	set_output(DDRD, PD5);
	set_output(DDRD, PD2);
	set_output(DDRD, PD3);// TTY send pin
	output_low(PORTB, PD5);// LED pin - flashes to indicate reset
	output_low(PORTD, PD2);
	output_low(PORTD, PD3);
//
// Toggle PD5 to indicate a reset has taken place
//
	for(uint8_t i = 0;i<5;i++){
		output_high(PIND, PD5);// Writing to the input port toggles the output pin
		_delay_ms(300);
	}

	uart_init();
	char *bufferPtr = buffer;
	uint8_t count = 0;
	
	#ifdef DEBUG
	uart_puts(START_MESSAGE);
	#endif
	
	while(1) {
		loop_until_bit_is_set(UCSRA, RXC); /* Wait until data exists. */
		char input = UDR;//getchar();
		*bufferPtr++ = input;
		count++;
		if(input == NEWLINE || count == 50) {
			//
			// Command entered so try to interpret it
			//
			*bufferPtr = NULLCHAR;// NULL terminate the buffer
			interpretCommand();
			bufferPtr = buffer;   // reset the buffer
			count = 0;            // reset the counter
		}
	}
	return 0;
}