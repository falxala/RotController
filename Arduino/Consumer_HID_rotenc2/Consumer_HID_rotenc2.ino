#include <boarddefs.h>
#include <IRremote.h>
#include <IRremoteInt.h>
#include <ir_Lego_PF_BitStreamEncoder.h>
#include "HID-Project.h"
//#include "Keyboard.h"

/*
  Copyright (c) 2014-2015 NicoHood
  See the readme for credit to other people.

  Consumer example
  Press a button to play/pause music player

  You may also use SingleConsumer to use a single report.

  See HID Project documentation for more Consumer keys.
  https://github.com/NicoHood/HID/wiki/Consumer-API
*/
/*
  // Media key definitions, see official USB docs for more
  #define MEDIA_FAST_FORWARD  0xB3
  #define MEDIA_REWIND  0xB4
  #define MEDIA_NEXT  0xB5
  #define MEDIA_PREVIOUS  0xB6
  #define MEDIA_STOP  0xB7
  #define MEDIA_PLAY_PAUSE  0xCD

  #define MEDIA_VOLUME_MUTE 0xE2
  #define MEDIA_VOLUME_UP 0xE9
  #define MEDIA_VOLUME_DOWN 0xEA

  #define CONSUMER_EMAIL_READER 0x18A
  #define CONSUMER_CALCULATOR 0x192
  #define CONSUMER_EXPLORER 0x194

  #define CONSUMER_BROWSER_HOME 0x223
  #define CONSUMER_BROWSER_BACK 0x224
  #define CONSUMER_BROWSER_FORWARD  0x225
  #define CONSUMER_BROWSER_REFRESH  0x227
  #define CONSUMER_BROWSER_BOOKMARKS  0x22A
*/
/*
  LED D13
  MI (MISO, SPI) D14 PB3
  MO  (MOSI, SPI) D16 PB2
  SCK (SPI)   D15 PB1
  RES       RESET
  GND       GND
  5V        VCC
  A2      D20 PF5
  A1      D19 PF6
  A0      D18 PF7
  D9  (PWM)   A9  PB5
  D10 (PWM)   A10 PB6
  D11 (PWM)     PB7
  TX  (serial)  D1  PD3
  RX  (serial)  D0  PD2
  SDA (I2C)   D2  PD1
  SCL (I2C,PWM) D3  PD0
*/

#define SigA 3
#define SigB 2
#define LED_A 10
#define LED_B 9
#define LED_State 13
#define SW 14
#define Mode_SW 8

static const int iRotPtn[] = {0, 1, -1, 0, -1, 0, 0, 1, 1, 0, 0, -1, 0, -1, 1, 0, 0};
//エンコーダー状態を記憶
volatile byte pos;
volatile int  enc_count;

int recvPin = 15;
IRrecv irrecv(recvPin);

void setup() {
  //シリアル初期化
  Serial.begin(115200);

  irrecv.enableIRIn();  // Start the receiver

  //ピン設定
  pinMode(SigA, INPUT_PULLUP);
  pinMode(SigB, INPUT_PULLUP);
  pinMode(SW, INPUT_PULLUP);
  pinMode(Mode_SW, INPUT_PULLUP);
  pinMode(LED_A, OUTPUT);
  pinMode(LED_B, OUTPUT);
  pinMode(LED_State, OUTPUT);

  //割り込み
  attachInterrupt(0, interrupt_enc, CHANGE);
  attachInterrupt(1, interrupt_enc, CHANGE);

  // Sends a clean report to the host. This is important on any Arduino type.
  Consumer.begin();
  Keyboard.begin();
}

int rot = 0;
int flag = 0;
int Mode_sw = 0;
int tick_value = 1000;
bool t_flag = false;
unsigned long time;
unsigned long time_sub = 0;
unsigned long memory = 0;
short led_value_A = 255;
short led_value_B = 255;
void loop() {
  Switch_function();
  decode_results  results;
  if (irrecv.decode(&results)) {  // Grab an IR code
    Code(&results);
    Serial.println("");           // Blank line between entries
    irrecv.resume();              // Prepare for the next value
    }
  LED_off_slowly();
}

void LED_off_slowly(){
    if(led_value_A >= 0 || led_value_B >= 0){
    if(led_value_A != 0)
      led_value_A--;
    if(led_value_B != 0)
      led_value_B--;
    delay(2);
    analogWrite(LED_A, led_value_A);
    analogWrite(LED_B, led_value_B);
  }
}

void  Code (decode_results *results)
{
  // Now dump "known" codes
  if (results->decode_type != UNKNOWN) {
    // Some protocols have an address
    if (results->decode_type == PANASONIC) {
      Serial.println(results->address, HEX);
    }
    int s = (results->value);
    Serial.println(results->value);
    switch (s)
    {
      case 16718055:
        Serial.print("R");
        Consumer.write(MEDIA_VOLUME_UP);
        memory = 16718055;
        break;

      case 16730805:
        Serial.print("L");
        Consumer.write(MEDIA_VOLUME_DOWN);
        memory = 16730805;
        break;

      case 4294967295:
        if (memory == 16718055)
          Consumer.write(MEDIA_VOLUME_UP);
        else if (memory == 16730805)
          Consumer.write(MEDIA_VOLUME_DOWN);
        break;
    }
  }
}

void Switch_function() {
  time = millis();
  if (time - time_sub >=  tick_value) {
    time_sub = time;
    if (t_flag == false) {
      digitalWrite(LED_State, HIGH);
      t_flag = true;
    }
    else {
      t_flag = false;
    }
  }

  int input;
  // シリアルポートより1文字読み込む
  input = Serial.read();

  if (digitalRead(Mode_SW) == LOW) {
    if (Mode_sw == 0) {
      input = 'S';
    }
    else if (Mode_sw == 1) {
      input = 'H';
    }
    while (digitalRead(Mode_SW) == LOW) {
      delay(10);
    }
  }
  switch (input) {
    case 'S':
      Mode_sw = 1;
      Serial.println("\r\nMode changed : Software");
      tick_value = 500;
      break;

    case'H':
      Mode_sw = 0;
      Serial.println("\r\nMode changed : Hardware");
      tick_value = 1500;
      break;

    case 'R':
      Mode_sw = 0;
      Serial.print(0x06);//ACK
      delay(200);
      Serial.println("\r\nCurrent Mode : Hardware");
      break;
  }

  rot = 0;
  rot = ENC_COUNT(rot);

  if (Mode_sw == 0) {
    if (rot > 0) {
      Serial.println("Right");
      Consumer.write(MEDIA_VOLUME_UP);
      //Keyboard.write('\\');
      led_value_A = 255;
    }
    else if (rot < 0) {
      Serial.println("Left");
      Consumer.write(MEDIA_VOLUME_DOWN);
      //Keyboard.write(']');
      led_value_B = 255;
    }

    if (digitalRead(SW) == LOW) {
      led_value_B = 255;
      led_value_A = 255;
      while (digitalRead(SW) == LOW) {
        rot = 0;
        rot = ENC_COUNT(rot);
        if (rot > 0) {
          Consumer.write(MEDIA_NEXT);
          flag = 1;
        }
        else if (rot < 0) {
          Consumer.write(MEDIA_PREVIOUS);
          flag = 1;
        }
      };
      if (flag == 0) {
        Consumer.write(MEDIA_PLAY_PAUSE);
        delay(100);
      }
      flag = 0;
    }
  }
  else if (Mode_sw == 1) {
    if (rot > 0) {
      Serial.print("R");
      led_value_A = 255;
    }
    else if (rot < 0) {
      Serial.print("L");
      led_value_B = 255;
    }
    if (digitalRead(SW) == LOW) {
      Serial.print("C");
      led_value_B = 255;
      led_value_A = 255;
      while (digitalRead(SW) == LOW) {
        delay(10);
      }
    }
  }
}

int ENC_COUNT(int incoming) {
  static int enc_old = enc_count;
  int val_change = enc_count - enc_old;

  if (val_change != 0)
  {
    incoming += val_change;
    enc_old   = enc_count;
  }
  return incoming;
}

//https://jumbleat.com/2016/12/17/encoder_1/
void interrupt_enc(void) {
  byte cur = (!digitalRead(SigB) << 1) + !digitalRead(SigA);
  byte old = pos & B00000011;
  byte dir = (pos & B00110000) >> 4;

  if (cur == 3) cur = 2;
  else if (cur == 2) cur = 3;

  if (cur != old)
  {
    if (dir == 0)
    {
      if (cur == 1 || cur == 3) dir = cur;
    } else {
      if (cur == 0)
      {
        if (dir == 1 && old == 3) enc_count++;
        else if (dir == 3 && old == 1) enc_count--;
        dir = 0;
      }
    }

    bool rote = 0;
    if (cur == 3 && old == 0) rote = 0;
    else if (cur == 0 && old == 3) rote = 1;
    else if (cur > old) rote = 1;

    pos = (dir << 4) + (old << 2) + cur;
  }
}

