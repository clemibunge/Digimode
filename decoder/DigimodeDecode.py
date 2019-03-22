import binascii
binout = "";
time = False;
filepath = 'Output.txt'  
with open(filepath) as fp:  
   line = fp.readline()
   cnt = 1
   while line:
       #print("Line {}: {}".format(cnt, line.strip()))
       line = fp.readline()
       cnt += 1
       try:
           line_float = round(float(line),1);
       except:
           line_float = round(float("0"),1);
       #print(line_float);
       try:
           line_int = int(line_float);
       except:
           line_int = int("0");
       #print(line_int);

       if 1080 <= line_int <= 2020:
#           print ("timing")
           time = True;

#       if 290 <= line_int <= 310:
#           print ("START")

#       if 340 <= line_int <= 360:
#           print ("STOP")

       if 690 <= line_int <= 710:
#           print ("0")
           if (time):
               binout += "0";
               time = False;

       if 790 <= line_int <= 810:
#           print ("1")
           if (time):
               binout += "1";
               time = False;




def text_to_bits(text, encoding='utf-8', errors='surrogatepass'):
    bits = bin(int(binascii.hexlify(text.encode(encoding, errors)), 16))[2:]
    return bits.zfill(8 * ((len(bits) + 7) // 8))

def text_from_bits(bits, encoding='utf-8', errors='surrogatepass'):
    n = int(bits, 2)
    return int2bytes(n).decode(encoding, errors)

def int2bytes(i):
    hex_string = '%x' % i
    n = len(hex_string)
    return binascii.unhexlify(hex_string.zfill(n + (n & 1)))
print("Received Binary Numbers:");
print(binout);
try:
    outstr = text_from_bits(binout);
    print(outstr);
except:
    print("Error at decoding binary back to ASCII");
