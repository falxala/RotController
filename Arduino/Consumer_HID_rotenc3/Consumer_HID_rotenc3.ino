/*---------------------------------------------*/
//デバイス識別番号
//シリアルで文字 "R" を受信したらこの数字を返す

#define ACK 0x00 //肯定応答
/*---------------------------------------------*/

//↓このスケッチの説明↓
/*----------------------------------------------
[シリアルで以下に示す文字を受信すると行う動作]
'C' EEPROMに保存されているキーコードを出力
'D' DUMP，EEPROMの中身を出力する
'M' EEPROMに仮想キーコードを記録する(ハードウェアモード)
    例えば,右回転のときは音量UP[0xE9]
    左回転のときは音量DOWN[0xEA]
    としたい場合は "M0xE9,0xEA" と送信する
'H' ハードウェアモードに変更する
    EEPROMに保存されている仮想キーコードを実行する
'S' ソフトウェアモードに変更する
    本機から右回転時は 'R' ,左回転時は 'L' を
    送信するので受信した端末で解釈して使用する
'R' 上記に記したACKの文字を送信する
    端末でデバイスを識別したい時に使用する

[Arduinoの動作]
・起動時とモード変更時にLEDが虹色に光る
・ハードウェアモードでは回転に合わせてLEDの色が変わる
・ソフトウェアモードでは白色固定
・LEDは最終操作から tick_value で設定した時間後に消灯する
・ロータリエンコーダで　右左右左右　または　左右左右左　を
操作するとモード(ハード・ソフト)が切り替わる

-----------------------------------------------*/

#include "HID-Project.h"
#include <EEPROM.h>
#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h>
#endif
#define PIN 11
#define SigA 3
#define SigB 2
#define LED_A 10
#define LED_B 9
#define LED_State 14
#define SW 12
#define Mode_SW 7

int rot = 0;
int flag = 0;
int Mode_sw = 0;
int tick_value = 1500;
int counter = 0;
static const int iRotPtn[] = 
{0, 1, -1, 0, -1, 0, 0, 1, 1, 0, 0, -1, 0, -1, 1, 0, 0};

//エンコーダー状態を記憶
volatile byte pos;
volatile int  enc_count;

char buff[12] = {'\0'};
byte C_pos = 0;
byte old_C_pos = 0;
byte rec = 0;
uint16_t Vkey1 = 0x00;
uint16_t Vkey2 = 0x00;
unsigned long time;
unsigned long time_sub = 0;
unsigned long memory = 0;

//WS2812 本機では1つ
Adafruit_NeoPixel strip = 
Adafruit_NeoPixel(1, PIN, NEO_GRB + NEO_KHZ800);

//EEPROM用構造体
typedef struct _VKSTRUCT {
  uint16_t Vkey1;
  uint16_t Vkey2;
} VKSTRUCT, *PVKSTRUCT;
VKSTRUCT vs;  //C.W. Vkey1
VKSTRUCT vs2; //C.C.W. Vkey2

void setup() {
  //EEPROMから読み込み

  //HID
  Consumer.begin();
  Keyboard.begin();

  //ピン設定
  pinMode(SigA, INPUT_PULLUP);
  pinMode(SigB, INPUT_PULLUP);
  pinMode(SW, INPUT_PULLUP);
  pinMode(Mode_SW, INPUT_PULLUP);

  // This is for Trinket 5V 16MHz, you can remove these three lines if you are not using a Trinket
#if defined (__AVR_ATtiny85__)
  if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
#endif
  // End of trinket special code

  strip.begin();
  strip.show(); // Initialize all pixels to 'off'

  //割り込み
  attachInterrupt(0, interrupt_enc, CHANGE);
  attachInterrupt(1, interrupt_enc, CHANGE);
  //起動LED
  rainbowCycle(5);
  //シリアル通信開始
  Serial.begin(115200);
  //EEPROMから読み込み
  eeprom_read_write('r', "", "");
}

void loop() {
  Switch_function("");
  Coloer_LED();
}

//EEPROM読み書き
void eeprom_read_write(char rw, uint16_t v1, uint16_t v2) {
  vs.Vkey1 = v1;
  vs.Vkey2 = v2;
  //書き込み
  if (rw == 'w') {
    byte* p = (byte*) &vs;

    for (int j = 0; j < sizeof(VKSTRUCT); j++) {
      EEPROM.write(j, *p);
      p++;
    }
  }
  //読み込み
  if (rw == 'r') {
    byte* p2 = (byte*) &vs2;

    for (int j = 0; j < sizeof(VKSTRUCT); j++) {
      byte b = EEPROM.read(j);
      *p2 = b;
      p2++;
    }
    Vkey1 = vs2.Vkey1;
    Vkey2 = vs2.Vkey2;
    Serial.println("[ Read data from EEPROM ]");
    Serial.print("C.W.    = ");
    Serial.println( Vkey1 , HEX);
    Serial.print("C.C.W. = ");
    Serial.println( Vkey2 , HEX);
  }
}

//操作を行ったら点灯　回転に合わせてLEDの色を変える
void Coloer_LED() {
  if (C_pos != old_C_pos) {
    old_C_pos = C_pos;
    strip.setPixelColor(0, Wheel(C_pos));
    strip.setBrightness(255);
    strip.show();
    time_sub = millis();
  }
  else {
    LED_off();
  }
}

//操作から一定時間後消灯
void LED_off() {
  time = millis();
  if (time - time_sub >=  tick_value) {
    time_sub = time;
    strip.setBrightness(0);
    strip.show();

    rec = (rec & B11111000) >> 4;
    //やっつけ
    if (rec == 2 || rec == 5 || rec == 10) {
      if (Mode_sw == 0) {
        Switch_function('S');
      }
      else if (Mode_sw == 1) {
        Switch_function('H');
      }
    }
    rec = 0;
  }
}

//5文字のHEX文字列をIntに
uint16_t hexToInt(String str) {
  const char hex[5] = {str[0], str[1], str[2], str[3], str[4]};
  return strtol( hex, NULL, 16);
}

//シリアル入力があればモード切り替え等
void Switch_function(int input) {
  // シリアルポートより1文字読み込む
  if (input == "")
    input = Serial.read();

  switch (input) {
    case 'C':
      //EEPROMから読み込み
      eeprom_read_write('r', "", "");
      break;

    case 'D':
      DUMP();
      break;

    case 'S':
      Mode_sw = 1;
      Serial.println("\r\nMode changed : Software");
      strip.setBrightness(255);
      rainbow(2);
      break;

    case'H':
      Mode_sw = 0;
      Serial.println("\r\nMode changed : Hardware");
      strip.setBrightness(255);
      rainbow(2);
      break;

    case 'R':
      Mode_sw = 0;
      Serial.print(ACK);//ACK
      delay(200);
      Serial.println("\r\nCurrent Mode : Hardware");
      break;

    case 'M':
      Serial.println("[ Set Vkey ]");
      char data = "";
      String s = "";
      byte comma = 6;//commaの位置を記憶
      while (counter <= 11) {
        data = Serial.read();
        buff[counter] = data;
        s = buff;
        if (data == ',') {
          comma = counter + 1;
          Vkey1 = hexToInt(s.substring(0, 5));
          Serial.print("[C.W.]   Vkey1 = ");
          Serial.println(Vkey1, HEX);
        }
        if (counter >= 11) {
          Vkey2 = hexToInt(s.substring(comma, 11));
          Serial.print("[C.C.W.] Vkey2 = ");
          Serial.println(Vkey2, HEX);
          counter++;
        }
        else {
          counter++;
        }
      }
      counter = 0;

      //EEPROM書き込み
      eeprom_read_write('w', Vkey1, Vkey2);

      break;
  }
  key_write();
}

//エンコーダに変化があればキー入力を実行
void key_write() {

  rot = 0;
  rot = ENC_COUNT(rot);

  if (Mode_sw == 0) {
    if (rot > 0) {
      Serial.println("Right");
      Consumer.write(Vkey1);
      Keyboard.write(Vkey1);
      strip.setBrightness(255);
      C_pos += 5;
      rec += 2;
      rec = rec << 1;
    }
    else if (rot < 0) {
      Serial.println("Left");
      Consumer.write(Vkey2);
      Keyboard.write(Vkey2);
      strip.setBrightness(255);
      C_pos -= 5;
      rec = rec << 1;
    }
  }
  else if (Mode_sw == 1) {
    if (rot > 0) {
      Serial.print("R");
      strip.setBrightness(255);
      strip.setPixelColor(0, 255, 255, 255);
      strip.show();
      rec += 2;
      rec = rec << 1;
      time_sub = millis();
    }
    else if (rot < 0) {
      Serial.print("L");
      strip.setBrightness(255);
      strip.setPixelColor(0, 255, 255, 255);
      strip.show();
      rec = rec << 1;
      time_sub = millis();
    }
    if (digitalRead(SW) == LOW) {
      Serial.print("C");
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
//割り込みで変化を読み取る
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


// Fill the dots one after the other with a color
void colorWipe(uint32_t c, uint8_t wait) {
  for (uint16_t i = 0; i < strip.numPixels(); i++) {
    strip.setPixelColor(i, c);
    strip.show();
    delay(wait);
  }
}

void rainbow(uint8_t wait) {
  uint16_t i, j;

  for (j = 0; j < 256; j++) {
    for (i = 0; i < strip.numPixels(); i++) {
      strip.setPixelColor(i, Wheel((i + j) & 255));
    }
    strip.show();
    delay(wait);
  }
}

// Slightly different, this makes the rainbow equally distributed throughout
void rainbowCycle(uint8_t wait) {
  uint16_t i, j;

  for (j = 0; j < 256 * 1; j++) { // 5 cycles of all colors on wheel
    for (i = 0; i < strip.numPixels(); i++) {
      strip.setPixelColor(i, Wheel(((i * 256 / strip.numPixels()) + j) & 255));
    }
    strip.show();
    delay(wait);
  }
}

// Input a value 0 to 255 to get a color value.
// The colours are a transition r - g - b - back to r.
uint32_t Wheel(byte WheelPos) {
  WheelPos = 255 - WheelPos;
  if (WheelPos < 85) {
    return strip.Color(255 - WheelPos * 3, 0, WheelPos * 3);
  }
  if (WheelPos < 170) {
    WheelPos -= 85;
    return strip.Color(0, WheelPos * 3, 255 - WheelPos * 3);
  }
  WheelPos -= 170;
  return strip.Color(WheelPos * 3, 255 - WheelPos * 3, 0);
}

//https://ht-deko.com/arduino/eeprom.html
//EEPROMの中身を出力
void DUMP() {
  uint16_t Address = 0;
  byte CheckSum;
  byte Sums[17];
  char buf[10];
  Serial.println("[ DUMP ]");
  do {
    // Header
    Serial.println("Add  +0 +1 +2 +3 +4 +5 +6 +7 +8 +9 +A +B +C +D +E +F Sum");
    memset(Sums, 0, sizeof(Sums));

    for (int i = 0; i < 16; i++) {
      // Address
      sprintf(buf, "%04X ", Address);
      Serial.print(buf);

      // Data
      CheckSum = 0;
      for (int l = 0; l < 16; l++) {
        byte Value = EEPROM.read(Address);
        sprintf(buf, "%02X ", Value);
        Serial.print(buf);
        CheckSum += Value;
        Sums[l] += Value;
        Address++;
      }
      Sums[16] += CheckSum;

      // CheckSum (Horizontal)
      sprintf(buf, ":%02X", CheckSum);
      Serial.println(buf);
    }

    // Footer / CheckSum (Vertical)
    Serial.println("--------------------------------------------------------");
    Serial.print("Sum: ");
    for (int i = 0; i < 16; i++) {
      sprintf(buf, "%02X ", Sums[i]);
      Serial.print(buf);
    }
    sprintf(buf, ":%02X", Sums[16]);
    Serial.println(buf);
    Serial.println("");
  } while (Address < EEPROM.length());
}

